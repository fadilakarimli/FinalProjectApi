using Microsoft.AspNetCore.Mvc;
using Service.DTOs.ChooseUsAbout;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class ChooseUsAboutController : BaseController
    {
        private readonly IChooseUsAboutService _chooseUsAboutService;

        public ChooseUsAboutController(IChooseUsAboutService chooseUsAboutService)
        {
            _chooseUsAboutService = chooseUsAboutService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var chooseUsAbouts = await _chooseUsAboutService.GetAllAsync();
            return Ok(chooseUsAbouts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var chooseUsAbout = await _chooseUsAboutService.GetByIdAsync(id);
            return Ok(chooseUsAbout);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ChooseUsAboutCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _chooseUsAboutService.CreateAsync(request);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ChooseUsAboutEditDto request)
        {
            if (request.Image == null)
                ModelState.Remove(nameof(request.Image));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _chooseUsAboutService.EditAsync(id, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _chooseUsAboutService.DeleteAsync(id);
            return NoContent();
        }
    }
}
