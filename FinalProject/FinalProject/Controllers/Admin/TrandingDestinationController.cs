using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.TrandingDestination;
using Service.Services;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class TrandingDestinationController : BaseController
    {
        private readonly ITrandingDestinationService _service;
        public TrandingDestinationController(ITrandingDestinationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null) return NotFound("Tapılmadı");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TrandingDestinationCreateDto model)
        {
            try
            {
                await _service.CreateAsync(model);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
           
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] TrandingDestinationEditDto model)
        {
            if (model.Image == null)
            {
                ModelState.Remove(nameof(model.Image));
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.EditAsync(id, model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated(int page = 1, int take = 5)
        {
            var result = await _service.GetPaginatedAsync(page, take);
            return Ok(result);
        }
    }
}
