using AutoMapper;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.NewLetter;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class NewsLetterService : INewsLetterService
    {
        private readonly INewsletterRepository _newletterRepo;
        private readonly IMapper _mapper;
        public NewsLetterService(INewsletterRepository newletterRepo , IMapper mapper)
        {
            _mapper = mapper;
            _newletterRepo = newletterRepo; 
        }
        public async Task AddEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Email düzgün formatda deyil.");

            await _newletterRepo.AddEmailAsync(email);
        }


        public async Task DeleteAsync(int id)
        {
            var newsletter = await _newletterRepo.GetByIdAsync(id);
            if (newsletter == null)
                throw new Exception("Belə bir e-poçt tapılmadı.");
            await _newletterRepo.DeleteAsync(newsletter);
        }
        public async Task<IEnumerable<NewLetterDto>> GetAllAsync()
        {
            var newsletters = await _newletterRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<NewLetterDto>>(newsletters);
        }
    }
}

