using Microsoft.AspNetCore.Mvc;
using Service.DTOs.AboutDestination;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class AboutDestinationController : BaseController
    {
        private readonly IAboutDestinationService _aboutDestinationService;

        public AboutDestinationController(IAboutDestinationService aboutDestinationService)
        {
            _aboutDestinationService = aboutDestinationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var aboutDestinations = await _aboutDestinationService.GetAllAsync();
            return Ok(aboutDestinations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var aboutDestination = await _aboutDestinationService.GetByIdAsync(id);
            return Ok(aboutDestination);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AboutDestinationCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _aboutDestinationService.CreateAsync(request);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] AboutDestinationEditDto request)
        {
            if (request.Image == null)
                ModelState.Remove(nameof(request.Image)); 

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _aboutDestinationService.EditAsync(id, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _aboutDestinationService.DeleteAsync(id);
            return NoContent();
        }

    }
}
