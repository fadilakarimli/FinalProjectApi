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
        Task<ResponseObject> ForgetPassword(string email, string requestScheme, string requestHost);
        Task<ResponseObject> ResetPassword(UserResetPasswordDto userResetPasswordDto);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<IEnumerable<RoleDto>> GetAllRolesByUserIdAsync(string userId);
        Task<IEnumerable<RoleDto>> GetAllRoles();
        Task<bool> AssignRoleAsync(AssignRoleDto dto);
        Task<bool> RemoveRoleAsync(string userId, string roleName);

        Task<UserProfileDto> GetProfile(string userId);
        Task<ResponseObject> UpdateProfile(string userId, UpdateProfileDto model);

    }
}
