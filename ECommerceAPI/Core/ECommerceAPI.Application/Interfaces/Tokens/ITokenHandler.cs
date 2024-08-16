using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Interfaces.Tokens
{
    public interface ITokenHandler
    {
        TokenDTO CreateAccessToken(int second, AppUser user);
        string CreateRefreshToken();
    }
}
