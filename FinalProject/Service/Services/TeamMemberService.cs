using AutoMapper;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.Slider;
using Service.DTOs.SliderInfo;
using Service.DTOs.TeamMember;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly ITeamMemberRepository _teamMemberRepo;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly ICloudinaryManager _cloudinaryManager;
        public TeamMemberService(ITeamMemberRepository teamMemberRepo
                                ,IMapper mapper
                                ,IFileService fileService , ICloudinaryManager cloudinaryManager)
        {
            _mapper = mapper;
            _teamMemberRepo = teamMemberRepo;
            _fileService = fileService;
            _cloudinaryManager  = cloudinaryManager;
        }
        public async Task CreateAsync(TeamMemberCreateDto model)
        {
            var imageUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
            var member = _mapper.Map<TeamMember>(model);
            member.Image = imageUrl;
            await _teamMemberRepo.CreateAsync(member);
        }

        public async Task DeleteAsync(int id)
        {
            var member = await _teamMemberRepo.GetWithExpressionAsync(x => x.Id == id);
            if (member == null) throw new Exception("Team member not found");

            if (!string.IsNullOrEmpty(member.Image))
                await _cloudinaryManager.FileDeleteAsync(member.Image);

            await _teamMemberRepo.DeleteAsync(member);
        }


        public async Task EditAsync(int id, TeamMemberEditDto model)
        {
            var member = await _teamMemberRepo.GetByIdWithIncludesAsync(id);
            if (member == null)
                throw new Exception($"Team member with ID {id} not found");

            if (model.Image != null)
            {
                if (!string.IsNullOrEmpty(member.Image))
                    await _cloudinaryManager.FileDeleteAsync(member.Image);

                var imageUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
                member.Image = imageUrl;
            }
            _mapper.Map(model, member);

            await _teamMemberRepo.EditAsync(member);
        }
        public async Task<IEnumerable<TeamMemberDto>> GetAllAsync()
        {
            var members = await _teamMemberRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<TeamMemberDto>>(members);
        }

        public async Task<TeamMemberDto> GetByIdAsync(int id)
        {
            var entity = await _teamMemberRepo.GetByIdAsync(id);
            if (entity == null) throw new Exception("Team member not found");
            return _mapper.Map<TeamMemberDto>(entity);
        }
    }
}
