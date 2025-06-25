using AutoMapper;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.AboutTeamMember;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AboutTeamMemberService : IAboutTeamMemberService
    {
        private readonly IAboutTeamMemberRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryManager _cloudinaryManager;
        public AboutTeamMemberService(IAboutTeamMemberRepository repository , IMapper mapper , ICloudinaryManager cloudinaryManager)
        {
            _cloudinaryManager = cloudinaryManager;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateAsync(AboutTeamMemberCreateDto model)
        {
            string fileUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
            var teamMember = _mapper.Map<AboutTeamMember>(model);
            teamMember.Image = fileUrl;

            await _repository.CreateAsync(teamMember);
        }

        public async Task DeleteAsync(int id)
        {
            var teamMember = await _repository.GetWithExpressionAsync(x => x.Id == id);
            if (teamMember == null)
                throw new Exception("AboutTeamMember tapılmadı");

            await _cloudinaryManager.FileDeleteAsync(teamMember.Image);
            await _repository.DeleteAsync(teamMember);
        }

        public async Task EditAsync(int id, AboutTeamMemberEditDto model)
        {
            var existingMember = await _repository.GetByIdAsync(id);
            if (existingMember == null)
                throw new Exception("AboutTeamMember tapılmadı");

            if (model.Image != null)
            {
                await _cloudinaryManager.FileDeleteAsync(existingMember.Image);
                string newFileUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
                existingMember.Image = newFileUrl;
            }
            _mapper.Map(model, existingMember);

            await _repository.EditAsync(existingMember);
        }

        public async Task<IEnumerable<AboutTeamMemberDto>> GetAllAsync()
        {
            var teamMembers = await _repository.GetAllAsync();
            return _mapper.Map<List<AboutTeamMemberDto>>(teamMembers);
        }

        public async Task<AboutTeamMemberDto> GetByIdAsync(int id)
        {
            var teamMember = await _repository.GetByIdAsync(id);
            if (teamMember == null)
                return null;

            return _mapper.Map<AboutTeamMemberDto>(teamMember);
        }
    }
}
