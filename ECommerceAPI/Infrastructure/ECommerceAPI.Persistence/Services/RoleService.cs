using ECommerceAPI.Application.Interfaces.Services;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRoleAsync(string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = name });
            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            IdentityResult result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public (object, int) GetAllRoles(int pageCount, int itemCount)
        {
            var query = _roleManager.Roles;
            IQueryable<AppRole> rolesQuery = null;

            if (pageCount != -1 && itemCount != -1)
                rolesQuery = query.Skip(itemCount * (pageCount - 1)).Take(itemCount);
            else
                rolesQuery = query;

            return (rolesQuery.Select(r => new {r.Id, r.Name}), query.Count());
        }

        public async Task<(string id, string name)> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return (role.Id, role.Name);
        }

        public async Task<bool> UpdateRoleAsync(string id, string name)
        {
            var role = await _roleManager.FindByIdAsync(id);
            role.Name = name;
            IdentityResult result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
    }
}
