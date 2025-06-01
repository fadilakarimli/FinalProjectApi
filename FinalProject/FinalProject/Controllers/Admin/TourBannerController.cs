using Microsoft.AspNetCore.Mvc;
using Service.DTOs.TourBanner;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class TourBannerController : BaseController
    {
        private readonly ITourBannerService _tourBannerService;

        public TourBannerController(ITourBannerService tourBannerService)
        {
            _tourBannerService = tourBannerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TourBannerCreateDto request)
        {
            await _tourBannerService.CreateAsync(request);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _tourBannerService.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var data = await _tourBannerService.GetByIdAsync(id);
            if (data == null)
                return NotFound(new { message = "Banner tapılmadı" });
            return Ok(data);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                await _tourBannerService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] TourBannerEditDto request)
        {
            try
            {
                await _tourBannerService.EditAsync(id, request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
