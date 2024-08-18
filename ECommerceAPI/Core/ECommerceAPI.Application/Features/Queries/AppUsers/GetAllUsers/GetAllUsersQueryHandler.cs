using ECommerceAPI.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.AppUsers.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryReponse>
    {
        readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetAllUsersQueryReponse> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = await _userService.GetAllUsersAsync(request.PageCount, request.ItemCount);
            return new()
            {
                TotalCount = datas.TotalCount,
                Users = datas.Users
            };
        }
    }
}
