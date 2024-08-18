using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.AppUsers.GetAllUsers
{
    public class GetAllUsersQueryReponse
    {
        public int TotalCount { get; set; }
        public object Users { get; set; }
    }
}
