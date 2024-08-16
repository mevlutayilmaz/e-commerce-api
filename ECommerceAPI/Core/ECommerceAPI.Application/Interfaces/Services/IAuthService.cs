using ECommerceAPI.Application.DTOs;

namespace ECommerceAPI.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<TokenDTO> LoginAsync(string UserNameOrEmail, string Password, int accessTokenLifeTime);
        Task<TokenDTO> RefreshTokenLoginAsync(string refreshToken);
    }
}
