using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.Slider;
using Service.DTOs.Tour;
using Service.Helpers;
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
        private readonly IExperienceRepository _experienceRepository;
        private readonly IEmailService _emailService;
        private readonly INewsLetterService _newsletterService;
        private readonly ICloudinaryManager _cloudinaryManager;
        private readonly IPlanRepository _planRepository;
        private readonly ISettingRepository _settingRepository;


        public TourService(ITourRepository repo, IMapper mapper, IFileService fileService, IEmailService emailService,
                           INewsLetterService newsletterService, ICloudinaryManager cloudinaryManager, IExperienceRepository experienceRepository, IPlanRepository planRepository
                         , ISettingRepository settingRepository)
        {
            _mapper = mapper;
            _repo = repo;
            _fileService = fileService;
            _emailService = emailService;
            _newsletterService = newsletterService;
            _cloudinaryManager = cloudinaryManager;
            _experienceRepository = experienceRepository;
            _planRepository = planRepository;
            _settingRepository = settingRepository;
        }
        public async Task CreateAsync(TourCreateDto model)
        {
            string fileUrl = null;

            if (model.ImageFile != null)
                fileUrl = await _cloudinaryManager.FileCreateAsync(model.ImageFile);

            var tour = _mapper.Map<Tour>(model);

            if (fileUrl != null)
                tour.Image = fileUrl;

            tour.StartDate = model.StartDate;
            tour.EndDate = model.EndDate;

            // TourCities üçün cityId-lərdən TourCity-lər yarat
            if (model.CityIds != null && model.CityIds.Any())
            {
                tour.TourCities = model.CityIds.Select(cityId => new TourCity
                {
                    CityId = cityId
                }).ToList();
            }

            // Əlavə əlaqələr, məsələn, TourActivities və TourAmenities
            tour.TourActivities = model.ActivityIds?.Select(id => new TourActivity
            {
                ActivityId = id
            }).ToList() ?? new List<TourActivity>();

            tour.TourAmenities = model.AmenityIds?.Select(id => new TourAmenity
            {
                AmenityId = id
            }).ToList() ?? new List<TourAmenity>();

            //if (model.ExperienceIds != null && model.ExperienceIds.Any())
            //{
            //    var experiences = await _experienceRepository
            //        .GetAllWithExpressionAsync(e => model.ExperienceIds.Contains(e.Id));
            //    tour.Experiences = experiences.ToList();
            //}

            await _repo.CreateAsync(tour);
        }


        public async Task DeleteAsync(int id)
        {
            var tour = await _repo.GetByIdAsync(id);
            if (tour is null) throw new Exception("Tour tapılmadı");

            await _cloudinaryManager.FileDeleteAsync(tour.Image);
            await _repo.DeleteAsync(tour);
        }

        public async Task EditAsync(int id, TourEditDto model)
        {
            var existTour = await _repo.GetByIdWithIncludesAsync(id);

            if (existTour is null) throw new Exception("Tour tapılmadı");

            if (model.ImageFile != null)
            {
                await _cloudinaryManager.FileDeleteAsync(existTour.Image);
                string newImageUrl = await _cloudinaryManager.FileCreateAsync(model.ImageFile);
                existTour.Image = newImageUrl;
            }

            _mapper.Map(model, existTour);

            // TourActivities
            existTour.TourActivities.Clear();
            existTour.TourActivities = model.ActivityIds.Select(id => new TourActivity
            {
                ActivityId = id,
                TourId = existTour.Id
            }).ToList();

            // TourAmenities
            existTour.TourAmenities.Clear();
            existTour.TourAmenities = model.AmenityIds.Select(id => new TourAmenity
            {
                AmenityId = id,
                TourId = existTour.Id
            }).ToList();

            // Experiences
            if (model.ExperienceIds != null && model.ExperienceIds.Any())
            {
                var experiences = await _experienceRepository
                    .GetAllWithExpressionAsync(e => model.ExperienceIds.Contains(e.Id));
                existTour.Experiences = experiences.ToList();
            }
            else
            {
                existTour.Experiences.Clear();
            }

            // *** Burada TourCities update etməliyik ***

            // 1. Əvvəlki əlaqələri silirik
            existTour.TourCities.Clear();

            // 2. Yeni əlaqələri əlavə edirik
            if (model.CityIds != null && model.CityIds.Any())
            {
                existTour.TourCities = model.CityIds.Select(cityId => new TourCity
                {
                    TourId = existTour.Id,
                    CityId = cityId
                }).ToList();
            }

            await _repo.EditAsync(existTour);
        }


        public async Task<IEnumerable<TourDto>> GetAllAsync()
        {
            var tours = await _repo.GetAllTourWithActivityAsync();

            var tourDtos = new List<TourDto>();

            foreach (var tour in tours)
            {
                var dto = _mapper.Map<TourDto>(tour);

                if (tour.TourCities != null && tour.TourCities.Any())
                {
                    var cityNames = tour.TourCities.Select(tc => tc.City.Name).ToList();
                    dto.CityIds = tour.TourCities.Select(tc => tc.CityId).ToList();
                    dto.CityNames = tour.TourCities.Select(tc => tc.City.Name).ToList();

                }
                else
                {
                    dto.CityNames = new List<string>();
                    dto.CityIds = new List<int>();

                }

                tourDtos.Add(dto);
            }

            return tourDtos;
        }

        public async Task<TourDto> GetByIdAsync(int id)
        {
            var tour = await _repo.GetByIdWithIncludesAsync(id); 

            if (tour == null) return null;

            return _mapper.Map<TourDto>(tour);
        }

        public async Task<Paginate<TourDto>> GetPaginatedAsync(int page, int take)
        {
            var tours = await _repo.GetPaginatedDatasAsync(page, take);
            var totalCount = await _repo.GetCountAsync();
            var tourDtos = _mapper.Map<IEnumerable<TourDto>>(tours);

            var pageCount = (int)System.Math.Ceiling((decimal)totalCount / take);

            return new Paginate<TourDto>(tourDtos, pageCount, page);
        }

        public async Task<IEnumerable<TourDto>> SearchAsync(TourSearchDto request)
        {
            var tours = await _repo.SearchAsync(request.Cities,request.Activities,request.Date,request.GuestCount);
            return _mapper.Map<IEnumerable<TourDto>>(tours);
        }


        public async Task<IEnumerable<TourDto>> SearchByNameAsync(string search)
        {
            IEnumerable<Tour> tours;

            if (string.IsNullOrWhiteSpace(search))
            {
                tours = await _repo.GetAllWithIcludesAsync(t => t.TourCities);
            }
            else
            {
                tours = await _repo.GetAllWithIncludesAndExpressionAsync(
                    t => t.Name.Contains(search) ||
                         t.TourCities.Any(tc => tc.City.Name.Contains(search)),
                    t => t.TourCities
                );
            }

            return _mapper.Map<IEnumerable<TourDto>>(tours);
        }


    }
}
