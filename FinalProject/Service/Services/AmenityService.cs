using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Activity;
using Service.DTOs.Amenity;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AmenityService : IAmenityService
    {
        private readonly IAmenityRepository _amenityRepo;
        private readonly IMapper _mapper;

        public AmenityService(IAmenityRepository amenityRepo, IMapper mapper)
        {
            _mapper = mapper;
            _amenityRepo = amenityRepo;
        }

        public async Task CreateAsync(AmenityCreateDto model)
        {
            var data = _mapper.Map<Amenity>(model);
            await _amenityRepo.CreateAsync(data);
        }

        public async Task DeleteAsync(int id)
        {
            var amenity = await _amenityRepo.GetByIdAsync(id);
            if (amenity == null) throw new Exception("Amenity not found");

            await _amenityRepo.DeleteAsync(amenity);
        }

        public async Task EditAsync(int id, AmenityEditDto model)
        {
            var amenity = await _amenityRepo.GetByIdAsync(id);
            if (amenity == null) throw new Exception("Amenity not found");
            _mapper.Map(model, amenity);
            await _amenityRepo.EditAsync(amenity);
        }


        public async Task<IEnumerable<AmenityDto>> GetAllAsync()
        {
            var amenity = await _amenityRepo.GetAllAsync();
            return _mapper.Map<List<AmenityDto>>(amenity);
        }

        public async Task<AmenityDto> GetByIdAsync(int id)
        {
            var amenity = await _amenityRepo.GetByIdAsync(id);
            if (amenity == null) throw new Exception("Activity not found");
            return _mapper.Map<AmenityDto>(amenity);
        }
    }
}
