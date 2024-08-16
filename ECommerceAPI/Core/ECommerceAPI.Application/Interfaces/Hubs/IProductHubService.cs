using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Interfaces.Hubs
{
    public interface IProductHubService
    {
        Task StudentAddedMessageAsync(string message);
    }
}
