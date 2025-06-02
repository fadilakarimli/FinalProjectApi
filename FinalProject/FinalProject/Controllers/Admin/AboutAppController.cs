using Microsoft.AspNetCore.Mvc;
using Service.DTOs.AboutApp;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class AboutAppController : BaseController
    {
        private readonly IAboutAppService _aboutAppService;
        public AboutAppController(IAboutAppService aboutAppService)
        {
            _aboutAppService = aboutAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var aboutApps = await _aboutAppService.GetAllAsync();
            return Ok(aboutApps);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var aboutApp = await _aboutAppService.GetByIdAsync(id);
            return Ok(aboutApp);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AboutAppCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _aboutAppService.CreateAsync(request);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] AboutAppEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _aboutAppService.EditAsync(id, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _aboutAppService.DeleteAsync(id);
            return NoContent();
        }
    }
}
