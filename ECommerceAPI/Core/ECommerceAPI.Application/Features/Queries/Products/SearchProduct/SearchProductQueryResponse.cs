using ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Features.Queries.Products.SearchProduct
{
    public class SearchProductQueryResponse
    {
        public IQueryable<Product>? Products { get; set; }
        public int totalCount { get; set; }
    }
}
