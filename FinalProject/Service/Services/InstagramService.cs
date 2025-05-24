using Service.DTOs.Instagram;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class InstagramService : IInstagramService
    {
        public Task CreateAsync(InstagramCreateDto model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(int id, InstagramEditDto request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InstagramDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<InstagramDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
