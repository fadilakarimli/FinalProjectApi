using AutoMapper;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.NewLetter;
using Service.Services.Interfaces;

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
                throw new ArgumentException("email is not true format.");

            await _newletterRepo.AddEmailAsync(email);
        }

        public async Task DeleteAsync(int id)
        {
            var newsletter = await _newletterRepo.GetWithExpressionAsync(x=>x.Id == id);
            if (newsletter == null)
                throw new Exception("Notfound email.");
            await _newletterRepo.DeleteAsync(newsletter);
        }
        public async Task<IEnumerable<NewLetterDto>> GetAllAsync()
        {
            var newsletters = await _newletterRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<NewLetterDto>>(newsletters);
        }
    }
}

