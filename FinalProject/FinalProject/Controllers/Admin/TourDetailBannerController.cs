using Microsoft.AspNetCore.Mvc;
using Service.DTOs.TourDetailBanner;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class TourDetailBannerController : BaseController
    {
        private readonly ITourDetailBannerService _tourDetailBannerService;
        public TourDetailBannerController(ITourDetailBannerService tourDetailBannerService)
        {
            _tourDetailBannerService = tourDetailBannerService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TourDetailBannerCreateDto request)
        {
            await _tourDetailBannerService.CreateAsync(request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _tourDetailBannerService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var data = await _tourDetailBannerService.GetByIdAsync(id);
            if (data == null)
                return NotFound(new { message = "Banner tapılmadı" });
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                await _tourDetailBannerService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] TourDetailBannerEditDto request)
        {
            try
            {
                await _tourDetailBannerService.EditAsync(id, request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
