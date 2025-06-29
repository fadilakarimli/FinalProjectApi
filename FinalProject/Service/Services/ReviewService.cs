using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Review;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(ReviewCreateDto dto)
        {
            var review = _mapper.Map<Review>(dto);
            await _reviewRepository.CreateAsync(review);
        }

        public async Task<IEnumerable<ReviewListDto>> GetByTourIdAsync(int tourId)
        {
            var reviews = await _reviewRepository.GetByTourIdAsync(tourId);
            return _mapper.Map<IEnumerable<ReviewListDto>>(reviews);
        }


        public async Task<IEnumerable<ReviewListDto>> GetAllAsync()
        {
            var reviews = await _reviewRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReviewListDto>>(reviews); 
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review != null)
            {
                await _reviewRepository.DeleteAsync(review);
            }
        }

        public async Task<ReviewListDto> GetByIdAsync(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null) return null;

            return _mapper.Map<ReviewListDto>(review);
        }


    }
}
