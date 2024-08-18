using ECommerceAPI.Application.DTOs.Users;
using ECommerceAPI.Application.Helpers;
using ECommerceAPI.Application.Interfaces.Services;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO user)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = user.NameSurname,
                Email = user.Email,
                UserName = user.UserName
            }, user.Password);

            if (result.Succeeded)
                return new()
                {
                    Succeeded = result.Succeeded,
                    Message = "User created successfully."
                };
            return new()
            {
                Succeeded = result.Succeeded,
                Message = result.Errors.First().Description
            };
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new Exception("User not found!");
        }

        public async Task<UserListDTO> GetAllUsersAsync(int pageCount, int itemCount)
        {
            var query = _userManager.Users;

            return new()
            {
                TotalCount = await query.CountAsync(),
                Users = await query.Select(u => new
                {
                    Id = u.Id,
                    NameSurname = u.NameSurname,
                    UserName = u.UserName,
                    Email = u.Email,
                }).ToListAsync()
            };
        }

        public async Task RemoveUserAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if(user != null)
            {
                IdentityResult result =  await _userManager.DeleteAsync(user);
                if(!result.Succeeded) throw new Exception("Silme işlemi başarısız!");
            }
            else
                throw new Exception("User not found!");
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded) await _userManager.UpdateSecurityStampAsync(user);
                else throw new Exception("Şifre değiştirilirken bir hata oluştu!");
            }
        }
    }
}
