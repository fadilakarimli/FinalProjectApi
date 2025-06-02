using Microsoft.AspNetCore.Mvc;
using Service.DTOs.AboutTeamMember;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class AboutTeamMemberController : BaseController
    {
        private readonly IAboutTeamMemberService _aboutTeamMemberService;

        public AboutTeamMemberController(IAboutTeamMemberService aboutTeamMemberService)
        {
            _aboutTeamMemberService = aboutTeamMemberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teamMembers = await _aboutTeamMemberService.GetAllAsync();
            return Ok(teamMembers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var teamMember = await _aboutTeamMemberService.GetByIdAsync(id);
            return Ok(teamMember);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AboutTeamMemberCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _aboutTeamMemberService.CreateAsync(request);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] AboutTeamMemberEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _aboutTeamMemberService.EditAsync(id, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _aboutTeamMemberService.DeleteAsync(id);
            return NoContent();
        }

    }
}
