using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.AppUsers.GetAllUsers
{
    public class GetAllUsersQueryRequest : IRequest<GetAllUsersQueryReponse>
    {
        public int PageCount { get; set; }
        public int ItemCount { get; set; }
    }
}
