using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppRole.CreateRole
{
    public class CreateRoleCommandRequest : IRequest<CreateRoleCommandResponse>
    {
        public string Name { get; set; }
    }
}