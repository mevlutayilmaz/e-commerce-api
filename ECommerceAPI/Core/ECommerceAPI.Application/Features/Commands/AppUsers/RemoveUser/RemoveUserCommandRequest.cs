using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.AppUsers.RemoveUser
{
    public class RemoveUserCommandRequest : IRequest<RemoveUserCommandResponse>
    {
        public string Id { get; set; }
    }
}
