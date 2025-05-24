using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Brand;
using Service.DTOs.Instagram;
using Service.Services;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class InstagramController : BaseController
    {
        private readonly IInstagramService _instagramService;
        public InstagramController(IInstagramService  instagramService)
        {
            _instagramService = instagramService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InstagramCreateDto request)
        {
            await _instagramService.CreateAsync(request);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var instagrams = await _instagramService.GetAllAsync();
            return Ok(instagrams);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var insta = await _instagramService.GetByIdAsync(id);
                return Ok(insta);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] InstagramEditDto request)
        {
            try
            {
                await _instagramService.EditAsync(id, request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]    
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                await _instagramService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
