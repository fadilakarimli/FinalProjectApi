using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Tour;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Client
{
    public class TourController : BaseController
    {
        private readonly ITourService _tourService;
        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }
        [HttpGet]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var tours = await _tourService.SearchByNameAsync(name);
            return Ok(tours);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter([FromBody] TourFilterDto filter)
        {
            var tours = await _tourService.FilterAsync(filter);
            return Ok(tours);
        }
    }
}
