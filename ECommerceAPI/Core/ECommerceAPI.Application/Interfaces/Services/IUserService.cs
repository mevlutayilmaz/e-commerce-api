using ECommerceAPI.Application.DTOs.Users;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO user);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
        Task<UserListDTO> GetAllUsersAsync(int pageCount, int itemCount);
        Task RemoveUserAsync(string Id);
        Task UpdatePasswordAsync(string userId,  string resetToken, string newPassword);
    }
}
