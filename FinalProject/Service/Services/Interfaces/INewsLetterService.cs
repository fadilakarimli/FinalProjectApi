using Domain.Entities;
using Service.DTOs.DestinationFeature;
using Service.DTOs.NewLetter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface INewsLetterService
    {
        Task DeleteAsync(int id);
        Task<IEnumerable<NewLetterDto>> GetAllAsync();
        Task AddEmailAsync(string email);
    }
}
