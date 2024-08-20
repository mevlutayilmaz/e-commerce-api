using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppRole.DeleteRole
{
    public class DeleteRoleCommandRequest : IRequest<DeleteRoleCommandResponse>
    {
        public string Id { get; set; }
    }
}