using Microsoft.AspNetCore.Mvc;
using Service.DTOs.AboutAgency;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class AboutAgencyController : BaseController
    {
        private readonly IAboutAgencyService _aboutAgencyService;
        public AboutAgencyController(IAboutAgencyService aboutAgencyService)
        {
            _aboutAgencyService = aboutAgencyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var aboutAgencies = await _aboutAgencyService.GetAllAsync();
            return Ok(aboutAgencies);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var aboutAgency = await _aboutAgencyService.GetByIdAsync(id);
            return Ok(aboutAgency);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AboutAgencyCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _aboutAgencyService.CreateAsync(request);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] AboutAgencyEditDto request)
        {
            if (request.Image == null)
            {
                ModelState.Remove(nameof(request.Image));
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _aboutAgencyService.EditAsync(id, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _aboutAgencyService.DeleteAsync(id);
            return NoContent();
        }
    }
}
