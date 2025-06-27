using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Brand;
using Service.DTOs.TeamMember;
using Service.Services;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Admin
{
    public class TeamMemberController : BaseController
    {
        private readonly ITeamMemberService _teamMemberService;
        public TeamMemberController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }
        [HttpPost]
        public async Task <IActionResult> Create([FromForm]TeamMemberCreateDto request)
        {
            await _teamMemberService.CreateAsync(request);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _teamMemberService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetById([FromRoute] int id)
        {
            var data = await _teamMemberService.GetByIdAsync(id);
            if (data == null)
                return NotFound(new { message = "Member tapılmadı" });
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                await _teamMemberService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] TeamMemberEditDto request)
        {
            if (request.Image == null)
                ModelState.Remove(nameof(request.Image));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _teamMemberService.EditAsync(id, request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
