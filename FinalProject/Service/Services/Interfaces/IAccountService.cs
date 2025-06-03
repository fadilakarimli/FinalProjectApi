using Domain.Entities;
using Microsoft.AspNetCore.Identity;
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
        Task ForgetPassword(string usernameOrEmail);
        Task ConfirmForgetPassword(string token, string userId, string newPassword);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<IEnumerable<RoleDto>> GetAllRolesByUserIdAsync(string userId);
        Task<IEnumerable<RoleDto>> GetAllRoles();
        Task CreateRole(string roleName);
        Task DeleteRole(string roleName);
        Task AddRoleToUser(string roleName, string userId);
        Task DeleteRoleToUser(string roleName, string userId);

    }
}
