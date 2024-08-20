using ECommerceAPI.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.AuthorizationEndpoints.GetRolesToEnpoint
{
    public class GetRolesToEnpointQueryHandler : IRequestHandler<GetRolesToEnpointQueryRequest, GetRolesToEnpointQueryResponse>
    {
        readonly IAuthorizationEndpointService _authorizationEndpointService;

        public GetRolesToEnpointQueryHandler(IAuthorizationEndpointService authorizationEndpointService)
        {
            _authorizationEndpointService = authorizationEndpointService;
        }

        public async Task<GetRolesToEnpointQueryResponse> Handle(GetRolesToEnpointQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = await _authorizationEndpointService.GetRolesToEndpointAsync(request.Code, request.Menu);
            return new() { Roles = datas };
        }
    }
}
