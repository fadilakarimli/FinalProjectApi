using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Brand;
using Service.DTOs.DestinationFeature;
using Service.Services;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class DestinationFeatureController : BaseController
    {
        private readonly IDestinationFeatureService _destinationService;
        public DestinationFeatureController(IDestinationFeatureService destinationService)
        {
            _destinationService = destinationService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DestinationFeatureCreateDto dto)
        {
            await _destinationService.CreateAsync(dto);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dest = await _destinationService.GetAllAsync();
            return Ok(dest);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var dest = await _destinationService.GetByIdAsync(id);
                return Ok(dest);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                await _destinationService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
