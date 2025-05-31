using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.DTOs.Account;
using Service.Helpers;
using Service.Helpers.Enums;
using Service.Helpers.Responses;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly JWTSetting _jwt;
        private readonly IEmailConfirmationService _emailConfirmationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly SymmetricSecurityKey _securityKey;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager,
                                                                IOptions<JWTSetting> options, IHttpContextAccessor httpContextAccessor
                                                                ,IEmailConfirmationService emailConfirmationService, IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _jwt = options.Value;
            _httpContextAccessor = httpContextAccessor;
            _emailConfirmationService = emailConfirmationService;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]));
            _configuration = configuration;
        }

        public async Task CreateRolesAsync()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }

        public async Task<LoginResponse> LoginAsync(LoginDto model)
        {
            AppUser user = await _userManager.FindByNameAsync(model.UserNameOrEmail);
            if (user is null)
                user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);

            if (user is null)
                return new LoginResponse { Success = false, Error = "Login failed", Token = null };

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
                return new LoginResponse { Success = false, Error = "Login failed", Token = null };

            var userRoles = await _userManager.GetRolesAsync(user);
            string token = GenerateJwtToken(user.UserName, userRoles.ToList());

            return new LoginResponse { Success = true, Error = null, Token = token };
        }

        private string GenerateJwtToken(string username, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, username)

            };
            roles.ForEach(role =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwt.ExpireDays));

            var token = new JwtSecurityToken(

                _jwt.Issuer,
                _jwt.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<RegisterResponse> RegisterAsync(RegisterDto model)
        {
            var user = _mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(user, model.UserPassword);
            if (!result.Succeeded)
                return new RegisterResponse { Success = false, Message = result.Errors.Select(m => m.Description).ToArray() };
            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
            //Email confirm
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var request = _httpContextAccessor.HttpContext.Request;
            string baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            string url = $"{baseUrl}/api/Client/Account/VerifyEmail?verifyEmail={HttpUtility.UrlEncode(user.Email)}&token={HttpUtility.UrlEncode(token)}";
            var template = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "confirm", "emailconfrim.html"));
            template = template.Replace("{{link}}", url);
            _emailConfirmationService.Send(user.Email, "Email confirmation", template);

            return new RegisterResponse { Success = true, Message = new List<string>() { token } };

        }
        public async Task<string> VerifyEmail(string verifyEmail, string token)
        {
            var appUser = await _userManager.FindByEmailAsync(verifyEmail);
            if (appUser == null) return "User does not exist.";

            var result = await _userManager.ConfirmEmailAsync(appUser, token);
            if (!result.Succeeded) return string.Join(", ", result.Errors.Select(error => error.Description));

            await _userManager.UpdateSecurityStampAsync(appUser);
            var roles = await _userManager.GetRolesAsync(appUser);

            return CreateToken(appUser, roles);
        }
        public string CreateToken(AppUser user, IList<string> roles)
        {
            List<Claim> claims = new List<Claim>
      {
          new Claim(JwtRegisteredClaimNames.NameId,user.UserName),
          new Claim(JwtRegisteredClaimNames.Email,user.Email),
          new Claim("FullName",user.FullName),
          new Claim(ClaimTypes.NameIdentifier,user.Id)
      };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            SigningCredentials credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"]

            };
            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
            var token = securityTokenHandler.CreateToken(tokenDescriptor);
            return securityTokenHandler.WriteToken(token);
        }
    }
}
