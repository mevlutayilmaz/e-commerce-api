using ECommerceAPI.Application.DTOs.Users;
using ECommerceAPI.Application.Interfaces.Services;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

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
    }
}
