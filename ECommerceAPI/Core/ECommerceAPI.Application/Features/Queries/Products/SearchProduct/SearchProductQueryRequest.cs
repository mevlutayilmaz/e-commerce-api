using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Products.SearchProduct
{
    public class SearchProductQueryRequest : IRequest<SearchProductQueryResponse>
    {
        public int pageCount { get; set; } = 1;
        public int itemCount { get; set; } = 5;
        public string Query { get; set; }
    }
}
