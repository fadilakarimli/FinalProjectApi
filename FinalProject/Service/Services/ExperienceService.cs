using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Experience;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IMapper _mapper;
        public ExperienceService(IExperienceRepository experienceRepository , IMapper mapper)
        {
            _experienceRepository = experienceRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(ExperienceCreateDto request)
        {
            var entity = _mapper.Map<Experience>(request);
            await _experienceRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _experienceRepository.GetByIdAsync(id);
            if (existing is null) throw new NullReferenceException("Experience not found");
            await _experienceRepository.DeleteAsync(existing);
        }
        public async Task EditAsync(int id, ExperienceEditDto request)
        {
            var existing = await _experienceRepository.GetByIdAsync(id);
            if (existing is null) throw new NullReferenceException("Experience not found");
            _mapper.Map(request, existing);
            await _experienceRepository.EditAsync(existing);
        }
        public async Task<IEnumerable<ExperienceDto>> GetAllAsync()
        {
            var experiences = await _experienceRepository.GetAllWithIcludesAsync(x => x.Tour);
            return _mapper.Map<IEnumerable<ExperienceDto>>(experiences);
        }

        public async Task<ExperienceDto> GetByIdAsync(int id)
        {
            var experience = await _experienceRepository.GetByIdWithIncludesAsync(id, x => x.Tour);
            if (experience is null) throw new Exception("Experience not found");
            return _mapper.Map<ExperienceDto>(experience);
        }

        public async Task<IEnumerable<ExperienceDto>> GetByTourIdAsync(int tourId)
        {
            var experiences = await _experienceRepository.GetAllWithIcludesAsync(x => x.Tour);

            var filtered = experiences.Where(e => e.Tour.Id == tourId);

            return _mapper.Map<IEnumerable<ExperienceDto>>(filtered);
        }

    }
}
