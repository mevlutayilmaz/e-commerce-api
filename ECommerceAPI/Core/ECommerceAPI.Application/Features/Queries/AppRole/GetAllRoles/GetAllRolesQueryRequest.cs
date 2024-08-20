using MediatR;

namespace ECommerceAPI.Application.Features.Queries.AppRole.GetAllRoles
{
    public class GetAllRolesQueryRequest : IRequest<GetAllRolesQueryResponse>
    {
        public int PageCount { get; set; }
        public int ItemCount { get; set; }
    }
}