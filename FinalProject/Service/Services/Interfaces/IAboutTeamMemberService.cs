using Service.DTOs.AboutBanner;
using Service.DTOs.AboutTeamMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAboutTeamMemberService
    {
        Task<IEnumerable<AboutTeamMemberDto>> GetAllAsync();
        Task<AboutTeamMemberDto> GetByIdAsync(int id);
        Task CreateAsync(AboutTeamMemberCreateDto model);
        Task EditAsync(int id, AboutTeamMemberEditDto model);
        Task DeleteAsync(int id);
    }
}
