using MediatR;

namespace ECommerceAPI.Application.Features.Queries.AuthorizationEndpoints.GetRolesToEnpoint
{
    public class GetRolesToEnpointQueryRequest : IRequest<GetRolesToEnpointQueryResponse>
    {
        public string Code { get; set; }
        public string Menu { get; set; }
    }
}