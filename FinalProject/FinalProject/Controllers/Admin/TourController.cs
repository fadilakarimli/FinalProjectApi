﻿using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Slider;
using Service.DTOs.Tour;
using Service.Services;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class TourController : BaseController
    {
        private readonly ITourService _tourService;
        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TourCreateDto request)
        {
            await _tourService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), "Created Succesfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tours = await _tourService.GetAllAsync();
            return Ok(tours);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _tourService.DeleteAsync(id);
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
            var tour = await _tourService.GetByIdAsync(id);
            if (tour == null)
                return NotFound(new { message = "Tour tapılmadı" });
            return Ok(tour);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] TourEditDto model)
        {
            try
            {
                await _tourService.EditAsync(id, model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated(int page = 1, int take = 5)
        {
            if (page < 1) page = 1;

            var result = await _tourService.GetPaginatedAsync(page, take);

            return Ok(new
            {
                Items = result.Datas,
                PageCount = result.PageCount,
                TotalPages = result.PageCount,
                CurrentPage = result.CurrentPage
            });
        }



        [HttpPost]
        public async Task<IActionResult> Search([FromBody] TourSearchDto request)
        {
            var tours = await _tourService.SearchAsync(request);
            return Ok(tours);
        }
    }
}
