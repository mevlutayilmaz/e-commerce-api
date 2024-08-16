using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Products.GetProductById
{
    public class GetProductByIdQueryResponse
    {
        public Product Product { get; set; }
    }
}
