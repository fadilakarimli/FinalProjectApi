using Microsoft.AspNetCore.Mvc;
using Service.DTOs.AboutBanner;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class AboutBannerController : BaseController
    {
        private readonly IAboutBannerService _aboutBannerService;

        public AboutBannerController(IAboutBannerService aboutBannerService)
        {
            _aboutBannerService = aboutBannerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var aboutBanners = await _aboutBannerService.GetAllAsync();
            return Ok(aboutBanners);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var aboutBanner = await _aboutBannerService.GetByIdAsync(id);
            return Ok(aboutBanner);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AboutBannerCreateDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _aboutBannerService.CreateAsync(request);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] AboutBannerEditDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _aboutBannerService.EditAsync(id, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _aboutBannerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
