using AutoMapper;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.Slider;
using Service.DTOs.Tour;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _repo;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly INewsLetterService _newsletterService;
        public TourService(ITourRepository repo, IMapper mapper, IFileService fileService, IEmailService emailService, INewsLetterService newsletterService)
        {
            _mapper = mapper;
            _repo = repo;
            _fileService = fileService;
            _emailService = emailService;
            _newsletterService = newsletterService;
        }
        public async Task CreateAsync(TourCreateDto model)
        {
            string fileName = await _fileService.UploadFilesAsync(model.ImageFile, "UploadFiles");

            var tour = _mapper.Map<Tour>(model);
            tour.Image = fileName;

            tour.TourActivities = model.ActivityIds.Select(id => new TourActivity
            {
                ActivityId = id
            }).ToList();

            tour.TourAmenities = model.AmenityIds.Select(id => new TourAmenity
            {
                AmenityId = id
            }).ToList();

            await _repo.CreateAsync(tour);
            var emails = await _newsletterService.GetAllAsync();
            foreach (var item in emails)
            {
                 _emailService.SendEmail(item.Email , "Travil Website" , tour.Name);
            }
        }
        public async Task DeleteAsync(int id)
        {
            var tour = await _repo.GetByIdAsync(id);
            if (tour is null) throw new Exception("Tour tapılmadı");
            _fileService.Delete(tour.Image, "UploadFiles");
            await _repo.DeleteAsync(tour);
        }

        public async Task EditAsync(int id, TourEditDto model)
        {
            var existTour = await _repo.GetByIdAsync(id);
            if (existTour is null) throw new Exception("Slider tapılmadı");

            if (model.ImageFile != null)
            {
                _fileService.Delete(existTour.Image, "UploadFiles");
                string newFileName = await _fileService.UploadFilesAsync(model.ImageFile, "UploadFiles");
                existTour.Image = newFileName;
            }
            _mapper.Map(model, existTour);
            await _repo.EditAsync(existTour);
        }

        public async Task<IEnumerable<TourDto>> GetAllAsync()
        {
            var tours = await _repo.GetAllTourWithActivityAsync();
            return _mapper.Map<List<TourDto>>(tours);
        }
        public async Task<TourDto> GetByIdAsync(int id)
        {
            var tour = await _repo.GetByIdAsync(id);
            if (tour == null) return null;
            return _mapper.Map<TourDto>(tour);
        }
    }
}
