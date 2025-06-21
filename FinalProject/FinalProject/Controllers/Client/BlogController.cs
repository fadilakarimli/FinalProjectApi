using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Client
{
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query boş ola bilməz.");

            try
            {
                var blogs = await _blogService.SearchBlogsAsync(query);
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                // Log buraya əlavə oluna bilər
                return StatusCode(500, new { message = "Serverdə xəta baş verdi.", error = ex.Message });
            }
        }


    }
}
