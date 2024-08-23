using ECommerceAPI.Application.DTOs.Users;
using ECommerceAPI.Application.Helpers;
using ECommerceAPI.Application.Interfaces.Services;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IReadRepository<Domain.Entities.Endpoint> _endpointReadRepository;
        readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<AppUser> userManager, IReadRepository<Domain.Entities.Endpoint> endpointReadRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _endpointReadRepository = endpointReadRepository;
            _httpContextAccessor = httpContextAccessor;
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

            var appUser = await _userManager.FindByNameAsync(user.UserName);

            IdentityResult identityResult = await _userManager.AddToRoleAsync(appUser, "Customer");

            if (result.Succeeded && identityResult.Succeeded)
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
                    TwoFactorEnabled = u.TwoFactorEnabled
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

        public async Task AssignRoleToUserAsync(string userId, string[] roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user != null )
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                await _userManager.AddToRolesAsync(user, roles);
            }
        }

        public async Task<IList<string>> GetRolesToUserAsync(string userIdOrName)
        {
            var user = await _userManager.FindByIdAsync(userIdOrName);
            if(user == null)
                user = await _userManager.FindByNameAsync(userIdOrName);

            if(user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return userRoles;
            }
            return new List<string>();
        }

        public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
        {
            var userRoles = await GetRolesToUserAsync(name);
            if (!userRoles.Any()) return false;

            Domain.Entities.Endpoint?  endpoint = await _endpointReadRepository.Table
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Code == code);

            if (endpoint == null) return false;

            var endpointRoles = endpoint.Roles.Select(r => r.Name);

            return userRoles.Any(userRole => endpointRoles.Contains(userRole));

        }

        public async Task<IList<string>> GetMenusOfUserRolesAsync()
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;

            if (!string.IsNullOrEmpty(username))
            {
                var roles = await GetRolesToUserAsync(username);

                if(roles.Any())
                    return await _endpointReadRepository.Table
                        .Include(e => e.Roles)
                        .Include(e => e.Menu)
                        .Where(e => e.Roles.Any(r => roles.Contains(r.Name)) && e.Definition != "Create Order" && e.Definition != "Get Menus Of User Roles" && e.Menu.Name != "Baskets")
                        .Select(e => e.Menu.Name)
                        .Distinct()
                        .ToListAsync();

                return new List<string>();
            } 
            return new List<string>();
        }
    }
}
