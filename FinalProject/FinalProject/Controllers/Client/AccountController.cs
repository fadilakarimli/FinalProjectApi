using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Account;
using Service.Helpers.Responses;
using Service.Services.Interfaces;
using System.Security.Claims;

namespace FinalProject.Controllers.Client
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            var response = await _accountService.RegisterAsync(request);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            return Ok(await _accountService.LoginAsync(request));

        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword([FromBody] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("Email not found. Make sure you typed correctly");

            if (email == null) return BadRequest("Email not found. Make sure you typed correctly");
            var scheme = HttpContext.Request.Scheme;
            var host = HttpContext.Request.Host.Value;
            ResponseObject responseObj = await _accountService.ForgetPassword(email, scheme, host);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj.ResponseMessage);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj.ResponseMessage);

            return Ok(responseObj);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordDto userResetPasswordDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObject responseObj = await _accountService.ResetPassword(userResetPasswordDto);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj.ResponseMessage);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj.ResponseMessage);

            return Ok(responseObj);
        }


        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string verifyEmail, string token)
        {
            if (verifyEmail == null || token == null) return BadRequest("Something went wrong");
            var response = await _accountService.VerifyEmail(verifyEmail, token);

            return Redirect("https://localhost:7014/Account/Login");
          
        }


        [HttpGet("profile")]
        [Authorize] 
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("İstifadəçi ID-si tapılmadı.");

                var profile = await _accountService.GetProfile(userId);

                if (profile == null)
                    return NotFound("İstifadəçi profili tapılmadı.");

                return Ok(profile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Xəta baş verdi: " + ex.Message);
            }
        }

        [HttpPut("profile")]
        [Authorize] 
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("İstifadəçi ID-si tapılmadı.");

                var result = await _accountService.UpdateProfile(userId, model);

                return StatusCode(result.StatusCode, new { message = result.ResponseMessage });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Xəta baş verdi: " + ex.Message);
            }
        }

    }
}
