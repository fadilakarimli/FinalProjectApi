using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
