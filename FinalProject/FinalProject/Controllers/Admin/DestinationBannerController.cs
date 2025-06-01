using Microsoft.AspNetCore.Mvc;
using Service.DTOs.DestinationBanner;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class DestinationBannerController : BaseController
    {
        private readonly IDestinationBannerService _destinationBannerService;
        public DestinationBannerController(IDestinationBannerService destinationBannerService)
        {
            _destinationBannerService = destinationBannerService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DestinationBannerCreateDto request)
        {
            await _destinationBannerService.CreateAsync(request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _destinationBannerService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var data = await _destinationBannerService.GetByIdAsync(id);
            if (data == null)
                return NotFound(new { message = "DestinationBanner tapılmadı" });
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                await _destinationBannerService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] DestinationBannerEditDto request)
        {
            try
            {
                await _destinationBannerService.EditAsync(id, request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
