using ECommerceAPI.Application.Interfaces.Services;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        readonly IReadRepository<Menu> _menuReadRepository;
        readonly IWriteRepository<Menu> _menuWriteRepository;
        readonly IReadRepository<Endpoint> _endpointReadRepository;
        readonly IWriteRepository<Endpoint> _endpointsWriteRepository;
        readonly IApplicationService _applicationService;
        readonly RoleManager<AppRole> _roleManager;

        public AuthorizationEndpointService(IReadRepository<Menu> menuReadRepository, IWriteRepository<Menu> menuWriteRepository, IReadRepository<Endpoint> endpointReadRepository, IWriteRepository<Endpoint> endpointsWriteRepository, IApplicationService applicationService, RoleManager<AppRole> roleManager)
        {
            _menuReadRepository = menuReadRepository;
            _menuWriteRepository = menuWriteRepository;
            _endpointReadRepository = endpointReadRepository;
            _endpointsWriteRepository = endpointsWriteRepository;
            _applicationService = applicationService;
            _roleManager = roleManager;
        }

        public async Task AssignRoleEndpointAsync(string[] roles, string menu, string code, Type type)
        {
            Menu _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);

            if(_menu == null)
            {
                _menu = new()
                {
                    Id = Guid.NewGuid(),
                    Name = menu,
                };

                await _menuWriteRepository.AddAsync(_menu);
                await _menuWriteRepository.SaveAsync();
            }

            Endpoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.Menu)
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

            if(endpoint == null)
            {
                var action = _applicationService.GetAuthorizeDefinitionEndpoints(type)
                    .FirstOrDefault(m => m.Name == menu)
                    ?.Actions.FirstOrDefault(a => a.Code == code);

                endpoint = new()
                {
                    Id = Guid.NewGuid(),
                    Code = action.Code,
                    ActionType = action.ActionType,
                    HttpType = action.HttpType,
                    Definition = action.Definition,
                    MenuId = _menu.Id,
                };

                await _endpointsWriteRepository.AddAsync(endpoint);
                try
                {
                    await _endpointsWriteRepository.SaveAsync();

                }
                catch (Exception ex)
                {

                }
            }

            foreach (var role in endpoint.Roles)
                endpoint.Roles.Remove(role);
                
            var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            foreach (var role in appRoles)
                endpoint.Roles.Add(role);

            await _endpointsWriteRepository.SaveAsync();
        }

        public async Task<List<string>> GetRolesToEndpointAsync(string code, string menu)
        {
            Endpoint? endpoint = await _endpointReadRepository.Table.
                Include(e => e.Menu)
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
            if(endpoint != null)
                return endpoint.Roles.Select(r => r.Name).ToList();
            return null;
        }
    }
}
