using ECommerceAPI.Application.DTOs.Prodcuts;
using ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Features.Queries.Products.GetAllProduct
{
    public class GetAllProductQueryResponse
    {
        public IQueryable<GetProductDTO> Products { get; set; }
        public int TotalCount { get; set; }
    }
}
