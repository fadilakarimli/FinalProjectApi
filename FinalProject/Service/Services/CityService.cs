using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.City;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepo;
        private readonly ICountryRepository _countryRepo;
        private readonly IMapper _mapper;

        public CityService(ICityRepository cityRepo, ICountryRepository countryRepo, IMapper mapper)
        {
            _cityRepo = cityRepo;
            _countryRepo = countryRepo;
            _mapper = mapper;
        }

        public async Task CreateAsync(CityCreateDto request)
        {
            var city = _mapper.Map<City>(request);
            await _cityRepo.CreateAsync(city);
        }

        public async Task<IEnumerable<CityDto>> GetAllAsync()
        {
            var cities = await _cityRepo.GetAllWithIcludesAsync(x => x.Country);
            return _mapper.Map<IEnumerable<CityDto>>(cities);
        }

        public async Task<CityDto> GetByIdAsync(int id)
        {
            var city = await _cityRepo.GetByIdWithIncludesAsync(id, x => x.Country);
            if (city is null) throw new NullReferenceException("City not found");
            return _mapper.Map<CityDto>(city);
        }

        public async Task DeleteAsync(int id)
        {
            var city = await _cityRepo.GetByIdAsync(id);
            if (city is null) throw new NullReferenceException("City not found");
            await _cityRepo.DeleteAsync(city);
        }

        public async Task EditAsync(int id, CityEditDto request)
        {
            var city = await _cityRepo.GetByIdAsync(id);
            if (city is null) throw new NullReferenceException("City not found");

            var country = await _countryRepo.GetByIdAsync(request.CountryId);
            if (country is null) throw new NullReferenceException("Country not found");

            _mapper.Map(request, city);
            await _cityRepo.EditAsync(city);
        }
    }
}