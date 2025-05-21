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
        public TeamMemberService(ITeamMemberRepository teamMemberRepo
                                ,IMapper mapper
                                ,IFileService fileService)
        {
            _mapper = mapper;
            _teamMemberRepo = teamMemberRepo;
            _fileService = fileService;
        }
        public async Task CreateAsync(TeamMemberCreateDto model)
        {
            var teamMember = await _fileService.UploadFilesAsync(model.Image, "UploadFiles");
            var member = _mapper.Map<TeamMember>(model);
            member.Image = teamMember;
            await _teamMemberRepo.CreateAsync(member);
        }

        public async Task DeleteAsync(int id)
        {
            var member = await _teamMemberRepo.GetWithExpressionAsync(x => x.Id == id);
            if (member == null) throw new Exception("Slider not found");
            _fileService.Delete(member.Image, "UploadFiles");
            await _teamMemberRepo.DeleteAsync(member);
        }

        public async Task EditAsync(int id, TeamMemberEditDto model)
        {
            var member = await _teamMemberRepo.GetByIdWithIncludesAsync(id);
            if (member == null)
                throw new Exception($"Slider with ID {id} not found");

            if (model.Image != null)
            {
                if (!string.IsNullOrEmpty(member.Image))
                    _fileService.Delete(member.Image, "UploadFiles");

                var imagePath = await _fileService.UploadFilesAsync(model.Image, "UploadFiles");
                member.Image = imagePath;
            }
            _mapper.Map(model, member);
            await _teamMemberRepo.EditAsync(member);
        }

        public async Task<IEnumerable<TeamMemberDto>> GetAllAsync()
        {
            var member = await _teamMemberRepo.GetAllAsync();
            var memberDtos = _mapper.Map<IEnumerable<TeamMemberDto>>(member);
            return memberDtos;
        }

        public async Task<TeamMemberDto> GetByIdAsync(int id)
        {
            var entity = await _teamMemberRepo.GetByIdAsync(id);
            if (entity == null) throw new Exception("TeamMember tapılmadı");
            return _mapper.Map<TeamMemberDto>(entity);
        }
    }
}
