using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.ChooseUsAbout;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ChooseUsAboutService : IChooseUsAboutService
    {
        private readonly IMapper _mapper;
        private readonly ICloudinaryManager _cloudinaryManager;
        private readonly IChooseUsAboutRepository _chooseUsAboutRepository;
        public ChooseUsAboutService(IMapper mapper , ICloudinaryManager cloudinaryManager , IChooseUsAboutRepository chooseUsAboutRepository )
        {
            _mapper = mapper;
            _cloudinaryManager = cloudinaryManager;
            _chooseUsAboutRepository = chooseUsAboutRepository;
            
        }
        public async Task CreateAsync(ChooseUsAboutCreateDto model)
        {
            string fileUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
            var chooseUsAbout = _mapper.Map<ChooseUsAbout>(model);
            chooseUsAbout.Image = fileUrl;
            await _chooseUsAboutRepository.CreateAsync(chooseUsAbout);
        }

        public async Task DeleteAsync(int id)
        {
            var chooseUsAbout = await _chooseUsAboutRepository.GetByIdAsync(id);
            if (chooseUsAbout == null)
                throw new Exception("ChooseUsAbout tapılmadı");

            await _cloudinaryManager.FileDeleteAsync(chooseUsAbout.Image);
            await _chooseUsAboutRepository.DeleteAsync(chooseUsAbout);
        }

        public async Task EditAsync(int id, ChooseUsAboutEditDto model)
        {
            var existChooseUsAbout = await _chooseUsAboutRepository.GetByIdAsync(id);
            if (existChooseUsAbout == null)
                throw new Exception("ChooseUsAbout tapılmadı");

            if (model.Image != null)
            {
                await _cloudinaryManager.FileDeleteAsync(existChooseUsAbout.Image);
                string newFileUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
                existChooseUsAbout.Image = newFileUrl;
            }

            _mapper.Map(model, existChooseUsAbout);
            await _chooseUsAboutRepository.EditAsync(existChooseUsAbout);
        }

        public async Task<IEnumerable<ChooseUsAboutDto>> GetAllAsync()
        {
            var chooseUsAbouts = await _chooseUsAboutRepository.GetAllAsync();
            return _mapper.Map<List<ChooseUsAboutDto>>(chooseUsAbouts);
        }

        public async Task<ChooseUsAboutDto> GetByIdAsync(int id)
        {
            var chooseUsAbout = await _chooseUsAboutRepository.GetByIdAsync(id);
            if (chooseUsAbout == null) return null;
            return _mapper.Map<ChooseUsAboutDto>(chooseUsAbout);
        }
    }
}
