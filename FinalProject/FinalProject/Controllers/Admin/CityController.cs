using Microsoft.AspNetCore.Mvc;
using Service.DTOs.City;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class CityController : BaseController
    {
        private readonly ICityService _service;

        public CityController(ICityService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityCreateDto request)
        {
            await _service.CreateAsync(request);
            return CreatedAtAction(nameof(Create), "Created Successfully");
        }

        [HttpGet]   
        public async Task<IActionResult> GetAll()
        {
            var datas = await _service.GetAllAsync();
            return Ok(datas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var data = await _service.GetByIdAsync(id);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] CityEditDto request)
        {
            await _service.EditAsync(id, request);
            return Ok();
        }
    }
}
