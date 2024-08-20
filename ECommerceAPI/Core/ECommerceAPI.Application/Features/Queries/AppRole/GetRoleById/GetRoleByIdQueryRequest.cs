using MediatR;

namespace ECommerceAPI.Application.Features.Queries.AppRole.GetRoleById
{
    public class GetRoleByIdQueryRequest : IRequest<GetRoleByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}