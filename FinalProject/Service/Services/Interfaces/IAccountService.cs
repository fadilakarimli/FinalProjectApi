using Domain.Entities;
using Service.DTOs.Account;
using Service.Helpers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task CreateRolesAsync();
        Task<RegisterResponse> RegisterAsync(RegisterDto model);
        Task<LoginResponse> LoginAsync(LoginDto model);
        Task<string> VerifyEmail(string VerifyEmail, string token);
        string CreateToken(AppUser user, IList<string> roles);
    }
}
