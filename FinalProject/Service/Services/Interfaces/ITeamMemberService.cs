using Service.DTOs.Slider;
using Service.DTOs.TeamMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ITeamMemberService
    {
        Task CreateAsync(TeamMemberCreateDto model);
        Task<IEnumerable<TeamMemberDto>> GetAllAsync();
        Task<TeamMemberDto> GetByIdAsync(int id);
        Task EditAsync(int id, TeamMemberEditDto model);
        Task DeleteAsync(int id);
    }
}
