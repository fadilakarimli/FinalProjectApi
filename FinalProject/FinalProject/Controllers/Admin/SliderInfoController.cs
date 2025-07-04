﻿using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.SliderInfo;
using Service.Services;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class SliderInfoController :  BaseController
    {
        private readonly ISliderInfoService _sliderInfoService;

        public SliderInfoController(ISliderInfoService sliderInfoService)
        {
            _sliderInfoService = sliderInfoService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SliderInfoCreateDto request)
        {
            await _sliderInfoService.CreateAsync(request);
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _sliderInfoService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            try
            {
                var data = await _sliderInfoService.GetByIdAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
          
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] SliderInfoEditDto request)
        {
            try
            {
                await _sliderInfoService.EditAsync(id, request);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            try
            {
                await _sliderInfoService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }

    }
}
