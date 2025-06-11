using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Contact;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Client
{
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;

        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] ContactCreateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _contactService.CreateAsync(request);
            return CreatedAtAction(nameof(CreateMessage), new { response = "Data successfully created" });
        }
    }
}
