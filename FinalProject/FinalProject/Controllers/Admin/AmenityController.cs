using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Activity;
using Service.DTOs.Amenity;
using Service.Services;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class AmenityController : BaseController
    {
        private readonly IAmenityService _amenityService;
        public AmenityController(IAmenityService amenityService)
        {
            _amenityService = amenityService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var amenities = await _amenityService.GetAllAsync();
            return Ok(amenities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var amenities = await _amenityService.GetByIdAsync(id);
            return Ok(amenities);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AmenityCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _amenityService.CreateAsync(request);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] AmenityEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _amenityService.EditAsync(id, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _amenityService.DeleteAsync(id);
            return NoContent();
        }

    }
}
