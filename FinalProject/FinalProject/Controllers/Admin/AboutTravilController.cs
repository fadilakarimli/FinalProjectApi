using Microsoft.AspNetCore.Mvc;
using Service.DTOs.AboutTravil;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class AboutTravilController :BaseController
    {
        private readonly IAboutTravilService _aboutTravilService;

        public AboutTravilController(IAboutTravilService aboutTravilService)
        {
            _aboutTravilService = aboutTravilService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var aboutTravils = await _aboutTravilService.GetAllAsync();
            return Ok(aboutTravils);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var aboutTravil = await _aboutTravilService.GetByIdAsync(id);
            if (aboutTravil == null)
                return NotFound();

            return Ok(aboutTravil);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AboutTravilCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _aboutTravilService.CreateAsync(request);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] AboutTravilEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _aboutTravilService.EditAsync(id, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _aboutTravilService.DeleteAsync(id);
            return NoContent();
        }
    }
}
