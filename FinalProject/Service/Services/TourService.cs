﻿using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly INewsletterRepository _newsletterRepository;



        public TourService(ITourRepository repo, IMapper mapper, IFileService fileService, IEmailService emailService,
                           INewsLetterService newsletterService, ICloudinaryManager cloudinaryManager, IExperienceRepository experienceRepository, IPlanRepository planRepository
                         , ISettingRepository settingRepository, INewsletterRepository newsletterRepository)
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
            _newsletterRepository = newsletterRepository;
        }
        public async Task CreateAsync(TourCreateDto model)
        {

            bool isOverlapping = await _repo.IsDateRangeOverlappingAsync(model.StartDate, model.EndDate);

            if (isOverlapping)
                throw new Exception("Bu tarix aralığında artıq mövcud bir tur var.");

            string fileUrl = null;

            if (model.ImageFile != null)
                fileUrl = await _cloudinaryManager.FileCreateAsync(model.ImageFile);

            var tour = _mapper.Map<Tour>(model);

            if (fileUrl != null)
                tour.Image = fileUrl;

            tour.StartDate = model.StartDate;
            tour.EndDate = model.EndDate;

            if (model.CityIds != null && model.CityIds.Any())
            {
                tour.TourCities = model.CityIds.Select(cityId => new TourCity
                {
                    CityId = cityId
                }).ToList();
            }

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

            var allSubscribers = await _newsletterRepository.GetAllAsync();

            string subject = "Yeni tur əlavə olundu!";

            string body = $@"
            <div style='font-family: Arial, sans-serif; padding: 20px; background-color: #f4f4f4;'>
                <div style='max-width: 600px; margin: auto; background-color: white; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0,0,0,0.1);'>
                    <div style='background-color: #007bff; color: white; padding: 20px; text-align: center;'>
                        <h2>Yeni tur əlavə olundu!</h2>
                    </div>
                    <div style='padding: 20px;'>
                        <p>Salam dəyərli abunəçi,</p>
                        <p><strong>{tour.Name}</strong> adlı yeni tur əlavə olundu!</p>
                        <p><strong>Başlama tarixi:</strong> {tour.StartDate:dd.MM.yyyy}</p>
                        <p><strong>Bitmə tarixi:</strong> {tour.EndDate:dd.MM.yyyy}</p>
                        <p>Daha ətraflı məlumat üçün saytımıza keçid edin:</p>
                        <p style='text-align: center;'>
                            <a href='https://localhost:7014/tour/{tour.Id}' 
                               style='background-color: #28a745; color: white; padding: 10px 20px; 
                                      text-decoration: none; border-radius: 5px; display: inline-block;'>
                                Tura bax
                            </a>
                        </p>
                    </div>
                    <div style='background-color: #f8f9fa; text-align: center; padding: 10px; font-size: 12px; color: #888;'>
                        Bu email sistem tərəfindən avtomatik göndərilmişdir. Cavab yazmayın.
                    </div>
                </div>
            </div>";


            foreach (var subscriber in allSubscribers)
            {
                _emailService.SendEmail(subscriber.Email, subject, body);
            }
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

            if (existTour == null) throw new Exception("Tour tapılmadı");

            if (model.ImageFile != null)
            {
                await _cloudinaryManager.FileDeleteAsync(existTour.Image);
                string newImageUrl = await _cloudinaryManager.FileCreateAsync(model.ImageFile);
                existTour.Image = newImageUrl;
            }
            // else
            // {
            //     existTour.Image = model.ExistingImageUrl;
            // }

            _mapper.Map(model, existTour);

            existTour.TourActivities.Clear();
            existTour.TourActivities = model.ActivityIds.Select(id => new TourActivity
            {
                ActivityId = id,
                TourId = existTour.Id
            }).ToList();

            existTour.TourAmenities.Clear();
            existTour.TourAmenities = model.AmenityIds.Select(id => new TourAmenity
            {
                AmenityId = id,
                TourId = existTour.Id
            }).ToList();

            existTour.TourCities.Clear();

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
                    dto.CityIds = tour.TourCities.Select(tc => tc.CityId).ToList();
                    dto.CityNames = tour.TourCities.Select(tc => tc.City.Name).ToList();

                    dto.CountryIds = tour.TourCities
                        .Where(tc => tc.City != null)
                        .Select(tc => tc.City.CountryId)
                        .Distinct()
                        .ToList();

                    dto.CountryNames = tour.TourCities
                        .Where(tc => tc.City != null && tc.City.Country != null)
                        .Select(tc => tc.City.Country.Name)
                        .Distinct()
                        .ToList();
                }
                else
                {
                    dto.CityIds = new List<int>();
                    dto.CityNames = new List<string>();
                    dto.CountryIds = new List<int>();
                    dto.CountryNames = new List<string>();
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
        public async Task<IEnumerable<TourDto>> FilterAsync(TourFilterDto filterDto)
        {
            var query = (await _repo.GetAllForFilterAsync()).AsQueryable();

            if (filterDto.CityIds != null && filterDto.CityIds.Any())
            {
                query = query.Where(t => t.TourCities.Any(tc =>
                    tc.City != null && filterDto.CityIds.Contains(tc.City.Id)));
            }

            if (filterDto.ActivityIds != null && filterDto.ActivityIds.Any())
            {
                query = query.Where(t => t.TourActivities.Any(ta =>
                    ta.Activity != null && filterDto.ActivityIds.Contains(ta.Activity.Id)));
            }

            if (filterDto.AmenityIds != null && filterDto.AmenityIds.Any())
            {
                query = query.Where(t => t.TourAmenities.Any(ta =>
                    ta.Amenity != null && filterDto.AmenityIds.Contains(ta.Amenity.Id)));
            }

            if (filterDto.DepartureDate != null)
            {
                var dateOnly = filterDto.DepartureDate.Value.Date;
                query = query.Where(t => t.StartDate.Date == dateOnly);
            }

            if (filterDto.GuestCount.HasValue)
            {
                query = query.Where(t => t.Capacity >= filterDto.GuestCount.Value);
            }   

            if (filterDto.MinPrice.HasValue)
            {
                query = query.Where(t => t.Price >= filterDto.MinPrice.Value);
            }

            if (filterDto.MaxPrice.HasValue)
            {
                query = query.Where(t => t.Price <= filterDto.MaxPrice.Value);
            }

            var result = _mapper.Map<List<TourDto>>(query.ToList());
            return result;
        }

    }
}
