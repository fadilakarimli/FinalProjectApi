using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Account;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoles()
        {
            await _accountService.CreateRolesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _accountService.GetAllUsersAsync());
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllRolesbyUserId([FromRoute] string userId)
        {
            return Ok(await _accountService.GetAllRolesByUserIdAsync(userId));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(await _accountService.GetAllRoles());
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto dto)
        {
            var result = await _accountService.AssignRoleAsync(dto);
            if (!result)
                return BadRequest("Rol təyinatı alınmadı.");

            return Ok("Rol təyin edildi.");
        }

    }
}
