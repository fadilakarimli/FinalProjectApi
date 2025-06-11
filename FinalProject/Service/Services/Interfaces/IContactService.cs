using Service.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IContactService
    {
        Task CreateAsync(ContactCreateDto model);
        Task DeleteAsync(int? id);
        Task<IEnumerable<ContactDto>> GetAllAsync();
        Task<ContactDto> GetByIdAsync(int? id);
        Task ReplyAsync(int contactId, string replyMessage);
    }
}
