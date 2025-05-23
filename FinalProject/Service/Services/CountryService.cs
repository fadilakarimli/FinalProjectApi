using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Country;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CountryCreateDto request)
        {
            var entity = _mapper.Map<Country>(request);
            await _countryRepository.CreateAsync(entity);
        }


        public async Task DeleteAsync(int id)
        {
            var existing = await _countryRepository.GetByIdAsync(id);
            if (existing is null) throw new NullReferenceException("Country not found");
            await _countryRepository.DeleteAsync(existing);
        }

        public async Task EditAsync(int id, CountryEditDto request)
        {
            var existing = await _countryRepository.GetByIdAsync(id);
            if (existing is null) throw new NullReferenceException("Country not found");
            _mapper.Map(request, existing);
            await _countryRepository.EditAsync(existing);
        }

        public async Task<IEnumerable<CountryDto>> GetAllAsync()
        {
            var countries = await _countryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CountryDto>>(countries);
        }
        public async Task<CountryDto> GetByIdAsync(int id)
        {
            var country = await _countryRepository.GetByIdAsync(id);
            if (country is null) throw new NullReferenceException("Country not found");
            return _mapper.Map<CountryDto>(country);
        }
    }
}
