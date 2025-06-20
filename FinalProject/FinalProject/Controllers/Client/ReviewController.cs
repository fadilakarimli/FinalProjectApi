using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Review;
using Service.Services;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Client
{
    public class ReviewController : BaseController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _reviewService.CreateAsync(dto);
            return Ok(new { message = "Review successfully submitted" });
        }

   

        [HttpGet("tour/{tourId}")]
        public async Task<IActionResult> GetByTourId(int tourId)
        {
            var reviews = await _reviewService.GetByTourIdAsync(tourId);
            return Ok(reviews);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewListDto>>> GetAll()
        {
            var reviews = await _reviewService.GetAllAsync();
            return Ok(reviews);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _reviewService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}
