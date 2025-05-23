using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Country;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class CountryController : BaseController
    {
        private readonly ICountryService _service;

        public CountryController(ICountryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CountryCreateDto request)
        {
            await _service.CreateAsync(request);
            return CreatedAtAction(nameof(Create), "Created Successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] CountryEditDto request)
        {
            await _service.EditAsync(id, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
