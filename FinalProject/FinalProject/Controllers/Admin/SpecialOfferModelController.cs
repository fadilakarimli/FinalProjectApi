using Microsoft.AspNetCore.Mvc;
using Service.DTOs.SpecialOffer;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class SpecialOfferModelController : BaseController
    {
        private readonly ISpecialOfferModelService _specialOfferService;
        public SpecialOfferModelController(ISpecialOfferModelService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var offers = await _specialOfferService.GetAllAsync();
            return Ok(offers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var offer = await _specialOfferService.GetByIdAsync(id);
            return Ok(offer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SpecialOfferCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _specialOfferService.CreateAsync(request);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] SpecialOfferEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _specialOfferService.EditAsync(id, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            await _specialOfferService.DeleteAsync(id);
            return NoContent();
        }

    }
}
