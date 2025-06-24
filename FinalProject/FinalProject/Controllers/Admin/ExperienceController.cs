using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Experience;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class ExperienceController : BaseController
    {
        private readonly IExperienceService _experienceService;

        public ExperienceController(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var experiences = await _experienceService.GetAllAsync();
            return Ok(experiences);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var experience = await _experienceService.GetByIdAsync(id);
            return Ok(experience);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExperienceCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _experienceService.CreateAsync(request);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ExperienceEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _experienceService.EditAsync(id, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _experienceService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("GetByTourId/{tourId}")]
        public async Task<IActionResult> GetByTourId(int tourId)
        {
            var experiences = await _experienceService.GetByTourIdAsync(tourId);
            return Ok(experiences);
        }

    }
}
