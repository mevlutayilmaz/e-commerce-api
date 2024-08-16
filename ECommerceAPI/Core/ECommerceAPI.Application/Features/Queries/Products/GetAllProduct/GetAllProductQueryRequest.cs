using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Products.GetAllProduct
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        public int pageCount { get; set; } = 1;
        public int itemCount { get; set; } = 5;
        public Guid? CategoryId { get; set; }
        public string SortBy { get; set; } = "Created Date";
        public bool IsAscending { get; set; } = true;
    }
}
