using ECommerceAPI.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.AppUsers.GetMenusOfUserRoles
{
    public class GetMenusOfUserRolesQueryHandler : IRequestHandler<GetMenusOfUserRolesQueryRequest, GetMenusOfUserRolesQueryResponse>
    {
        readonly IUserService _userService;

        public GetMenusOfUserRolesQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetMenusOfUserRolesQueryResponse> Handle(GetMenusOfUserRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var menus = await _userService.GetMenusOfUserRolesAsync();
            return new() { Menus = menus };
        }
    }
}
