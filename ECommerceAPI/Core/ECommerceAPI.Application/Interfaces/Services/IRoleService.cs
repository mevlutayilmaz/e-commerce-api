using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Interfaces.Services
{
    public interface IRoleService
    {
        (object, int) GetAllRoles(int pageCount, int itemCount);
        Task<(string id, string name)> GetRoleByIdAsync(string id);
        Task<bool> CreateRoleAsync(string name);
        Task<bool> DeleteRoleAsync(string id);
        Task<bool> UpdateRoleAsync(string id, string name);
    }
}
