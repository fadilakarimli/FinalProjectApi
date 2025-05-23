using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Activity;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class ActivityController : BaseController
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var activities = await _activityService.GetAllAsync();
            return Ok(activities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var activity = await _activityService.GetByIdAsync(id);
            return Ok(activity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ActivityCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _activityService.CreateAsync(request);
            return StatusCode(201); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ActivityEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _activityService.EditAsync(id, request);
            return Ok(); 
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            await _activityService.DeleteAsync(id);
            return NoContent(); 
        }
    }
}
