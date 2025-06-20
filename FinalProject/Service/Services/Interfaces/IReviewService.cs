using Service.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IReviewService
    {
        Task CreateAsync(ReviewCreateDto dto);
        Task<IEnumerable<ReviewListDto>> GetByTourIdAsync(int tourId);
        Task<IEnumerable<ReviewListDto>> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
