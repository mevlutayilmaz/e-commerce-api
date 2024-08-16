using ECommerceAPI.Application.DTOs.Prodcuts;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Products.CreateProducts
{
    public class CreateProductsCommandRequest : IRequest<CreateProductsCommandResponse>
    {
        public List<CreateProductDTO> Products { get; set; }
    }
}
