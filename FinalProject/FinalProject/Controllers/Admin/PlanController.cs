using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Plan;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class PlanController : BaseController
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlanCreateDto request)
        {
            await _planService.CreateAsync(request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plans = await _planService.GetAllAsync();
            return Ok(plans);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                await _planService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var plan = await _planService.GetByIdAsync(id);
            if (plan == null)
                return NotFound(new { message = "Plan tapılmadı" });

            return Ok(plan);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] PlanEditDto model)
        {
            try
            {
                await _planService.EditAsync(id, model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



    }
}
