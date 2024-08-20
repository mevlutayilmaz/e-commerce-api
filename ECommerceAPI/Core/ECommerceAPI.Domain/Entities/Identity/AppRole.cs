using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Domain.Entities.Identity
{
    public class AppRole : IdentityRole<string> 
    {
        public AppRole() => Endpoints = new HashSet<Endpoint>();

        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
