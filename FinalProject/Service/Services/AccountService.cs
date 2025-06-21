using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Data;
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
using Newtonsoft.Json.Linq;

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
        private readonly AppDbContext _context; 
        private readonly IDistributedCache _distributedCache;
        private readonly UrlHelperService _urlHelper;
        private readonly ISendEmailService _sendEmail;




        public AccountService(UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager,
                                                                IOptions<JWTSetting> options, IHttpContextAccessor httpContextAccessor
                                                                ,IEmailConfirmationService emailConfirmationService, IConfiguration configuration
                                                                 , AppDbContext context, IDistributedCache distributedCache
                                                                 , UrlHelperService urlHelper, ISendEmailService sendEmail)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _jwt = options.Value;
            _httpContextAccessor = httpContextAccessor;
            _emailConfirmationService = emailConfirmationService;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]));
            _configuration = configuration;
            _distributedCache = distributedCache;
            _context = context;
            _urlHelper = urlHelper;
            _sendEmail = sendEmail;
        }





        public async Task ConfirmForgetPassword(string token, string userId, string newPassword)
        {
            var findUser = await _userManager.FindByIdAsync(userId);

            await _userManager.ResetPasswordAsync(findUser, token, newPassword);
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesByUserIdAsync(string userId)
        {
            IEnumerable<string> Roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(userId));

            List<IdentityRole> IdentityRoles = new List<IdentityRole>();

            foreach (var role in Roles)
            {
                var findRole = await _roleManager.FindByNameAsync(role);

                IdentityRoles.Add(findRole);
            }
            return _mapper.Map<IEnumerable<RoleDto>>(IdentityRoles);
        }

        public async Task<IEnumerable<RoleDto>> GetAllRoles()
        {
            return _mapper.Map<IEnumerable<RoleDto>>(await _roleManager.Roles.ToListAsync());
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

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var userDto = _mapper.Map<UserDto>(user); // AutoMapper ilə əsas mapping
                var roles = await _userManager.GetRolesAsync(user); // Rolları ayrıca al

                userDto.Roles = roles.ToList(); // Rol siyahısını əlavə et

                userDtos.Add(userDto);
            }

            return userDtos;
        }

        public async Task<bool> RemoveRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            if (!await _roleManager.RoleExistsAsync(roleName))
                return false; // Roll yoxdur, silinməsi lazım deyil

            var isInRole = await _userManager.IsInRoleAsync(user, roleName);
            if (!isInRole) return true; // Əgər istifadəçi artıq o roldan çıxarılıbsa, uğur sayılır

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result.Succeeded;
        }


        public async Task<LoginResponse> LoginAsync(LoginDto model)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            AppUser user = await _userManager.FindByNameAsync(model.UserNameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);

            if (user == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Error = "İstifadəçi tapılmadı.",
                    Token = null
                };
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordCorrect)
            {
                return new LoginResponse
                {
                    Success = false,
                    Error = "Şifrə yanlışdır.",
                    Token = null
                };
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return new LoginResponse
                {
                    Success = false,
                    Error = "Email təsdiqlənməyib.",
                    Token = null
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim("FullName", user.FullName ?? "")
    };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
            };

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            // Token yaradılır burada!
            string token = GenerateJwtToken(user.UserName, roles.ToList());

            return new LoginResponse
            {
                Success = true,
                Error = null,
                Token = token,
                UserName = user.UserName,
                Roles = roles.ToList()  // roles burada var
            };
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
            await _userManager.AddToRoleAsync(user, Roles.SuperAdmin.ToString());
            //Email confirm
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var request = _httpContextAccessor.HttpContext.Request;
            string baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            //string url = $"{baseUrl}/api/Client/Account/VerifyEmail?verifyEmail={HttpUtility.UrlEncode(user.Email)}&token={HttpUtility.UrlEncode(token)}";
            string url = $"https://localhost:7145/api/Account/VerifyEmail?verifyEmail={HttpUtility.UrlEncode(user.Email)}&token={HttpUtility.UrlEncode(token)}";
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

            // ✅ Email təsdiqləndikdən sonra serverdən təsdiq mesajı göndəririk
            return "Email verified successfully!";
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

        public Task CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task AddRoleToUser(string roleName, string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRoleToUser(string roleName, string userId)
        {
            throw new NotImplementedException();
        }
        public async Task<ResponseObject> ForgetPassword(string email, string requestScheme, string requestHost)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null)
            {
                return new ResponseObject
                {
                    ResponseMessage = "User does not exist.",
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);

            // Dinamik link düzəldirik (localhost-u əvəzlədik)
            string link = $"https://localhost:7014/Account/ResetPassword?email={HttpUtility.UrlEncode(appUser.Email)}&token={HttpUtility.UrlEncode(token)}";
            // Email şablonunu oxuyub linki əvəz edirik
            var template = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "confirm", "resetpassword.html"));
            template = template.Replace("{{confirmlink}}", link);

            // Email göndəririk
            _sendEmail.Send("fadilafk@code.edu.az", "Travil", appUser.Email, template, "Reset Password");

            IList<string> roles = await _userManager.GetRolesAsync(appUser);

            return new ResponseObject
            {
                ResponseMessage = token,
                StatusCode = (int)StatusCodes.Status200OK
            };
        }
        public async Task<bool> AssignRoleAsync(AssignRoleDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null) return false;

            // Rol yoxdursa, əlavə et (opsional)
            if (!await _roleManager.RoleExistsAsync(dto.RoleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(dto.RoleName));
            }

            // Əgər user artıq roldadırsa, yenidən əlavə etməyə ehtiyac yoxdur
            var alreadyInRole = await _userManager.IsInRoleAsync(user, dto.RoleName);
            if (alreadyInRole) return true;

            var result = await _userManager.AddToRoleAsync(user, dto.RoleName);
            return result.Succeeded;
        }

        public async Task<ResponseObject> ResetPassword(UserResetPasswordDto userResetPasswordDto)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(userResetPasswordDto.Email);
            if (appUser == null) return new ResponseObject
            {
                ResponseMessage = "User not found",
                StatusCode = (int)StatusCodes.Status404NotFound
            };
            var isSucceeded = await _userManager.VerifyUserTokenAsync(appUser, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", userResetPasswordDto.Token);
            if (!isSucceeded) return new ResponseObject
            {
                StatusCode = (int)StatusCodes.Status400BadRequest,
                ResponseMessage = "TokenIsNotValid"
            };
            IdentityResult resoult = await _userManager.ResetPasswordAsync(appUser, userResetPasswordDto.Token, userResetPasswordDto.Password);
            if (!resoult.Succeeded) return new ResponseObject
            {
                ResponseMessage = string.Join(", ", resoult.Errors.Select(error => error.Description)),
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            await _userManager.UpdateSecurityStampAsync(appUser);
            await _distributedCache.RemoveAsync(appUser.Email);
            return new ResponseObject
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Password successfully reseted"
            };
        }

        //public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        //{
        //    if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
        //        return IdentityResult.Failed(new IdentityError { Description = "İstifadəçi ID və ya token boşdur." });

        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //        return IdentityResult.Failed(new IdentityError { Description = "İstifadəçi tapılmadı." });

        //    var result = await _userManager.ConfirmEmailAsync(user, token);
        //    return result;
        //}
    }
}
