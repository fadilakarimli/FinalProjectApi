using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class NewsLetterController : BaseController
    {
        private readonly INewsLetterService _newsLetterService;
        public NewsLetterController(INewsLetterService newsLetterService)
        {
            _newsLetterService = newsLetterService; 
        }


        [HttpPost]
        public async Task<IActionResult> AddEmail([FromBody] string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return BadRequest("Email not be empty.");

            try
            {
                await _newsLetterService.AddEmailAsync(email);
                return Ok("Email added success");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _newsLetterService.GetAllAsync();
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            try
            {
                await _newsLetterService.DeleteAsync(id);
                return Ok("Email delete success.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
