using Microsoft.AspNetCore.Mvc;
using Service.DTOs.AboutBlog;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class AboutBlogController : BaseController
    {
        private readonly IAboutBlogService _aboutBlogService;

        public AboutBlogController(IAboutBlogService aboutBlogService)
        {
            _aboutBlogService = aboutBlogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var aboutBlogs = await _aboutBlogService.GetAllAsync();
            return Ok(aboutBlogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var aboutBlog = await _aboutBlogService.GetByIdAsync(id);
            return Ok(aboutBlog);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AboutBlogCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _aboutBlogService.CreateAsync(request);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] AboutBlogEditDto request)
        {
            if (request.Image == null)
                ModelState.Remove(nameof(request.Image));

            await _aboutBlogService.EditAsync(id, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _aboutBlogService.DeleteAsync(id);
            return NoContent();
        }
    }
}
