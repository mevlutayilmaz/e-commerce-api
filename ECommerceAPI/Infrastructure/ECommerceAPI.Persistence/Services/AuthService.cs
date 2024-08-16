using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Interfaces.Services;
using ECommerceAPI.Application.Interfaces.Tokens;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        readonly IUserService _userService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
        }

        public async Task<TokenDTO> LoginAsync(string UserNameOrEmail, string Password, int accessTokenLifeTime)
        {
            AppUser user = await _userManager.FindByNameAsync(UserNameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(UserNameOrEmail);
            if (user == null)
                throw new Exception("User not found!");
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, Password, false);

            if(result.Succeeded)
            {
                TokenDTO token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 1200);

                return new()
                {
                    AccessToken = token.AccessToken,
                    Expiration = token.Expiration,
                    RefreshToken = token.RefreshToken,
                };
            }
            throw new Exception("Authentication error!");
        }

        public async Task<TokenDTO> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = _userManager.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);

            if(user != null && user.RefreshTokenEndDate > DateTime.UtcNow)
            {
                TokenDTO token = _tokenHandler.CreateAccessToken(3600, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 1200);
                return token;
            }
            throw new Exception("User not found!");
        }
    }
}
