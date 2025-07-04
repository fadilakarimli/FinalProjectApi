﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Slider;
using Service.Services;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class SliderController : BaseController
    {
        private readonly ISliderService _sliderService;
        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SliderCreateDto request)
        {
            await _sliderService.CreateAsync(request);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var educations = await _sliderService.GetAllAsync();
            return Ok(educations);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                await _sliderService.DeleteAsync(id);
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
            var slider = await _sliderService.GetByIdAsync(id);
            if (slider == null)
                return NotFound(new { message = "Slider tapılmadı" });

            return Ok(slider);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] SliderEditDto model)
        {
            if (model.Image == null)
                ModelState.Remove(nameof(model.Image));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _sliderService.EditAsync(id, model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    



    }
}
