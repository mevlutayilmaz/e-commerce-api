using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs.Users
{
    public class UserListDTO
    {
        public int TotalCount { get; set; }
        public object Users { get; set; }
    }
}
