using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Setting;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class SettingController : BaseController
    {
        private readonly ISettingService _service;
        public SettingController(ISettingService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SettingCreateDto request)
        {
            var result = await _service.CreateAsync(request);

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _service.DeleteAsync(id);

            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SettingEditDto request)
        {
            var result = await _service.EditAsync(id, request);

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _service.GetByIdAsync(id);

            return Ok(result);
        }
    }
}
