using ECommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.AppUsers.LoginUser
{
    public class LoginUserCommandResponse
    {
        public TokenDTO Token { get; set; }
    }
}
