using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Controllers.Admin
{
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;
        private readonly IEmailConfirmationService _emailService;
        public ContactController(IContactService contactService, IEmailConfirmationService emailService)
        {
            _contactService = contactService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _contactService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _contactService.GetByIdAsync(id));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery][Required] int id)
        {
            await _contactService.DeleteAsync(id);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Reply([FromQuery][Required] int contactId, [FromQuery][Required] string replyMessage)
        {
            await _contactService.ReplyAsync(contactId, replyMessage);
            return Ok(new { message = "Reply sent successfully." });
        }

    }
}
