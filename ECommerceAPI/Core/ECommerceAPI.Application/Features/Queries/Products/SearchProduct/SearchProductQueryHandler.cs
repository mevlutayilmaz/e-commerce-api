using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Products.SearchProduct
{
    public class SearchProductQueryHandler : IRequestHandler<SearchProductQueryRequest, SearchProductQueryResponse>
    {
        readonly IReadRepository<Product> _readRepository;

        public SearchProductQueryHandler(IReadRepository<Product> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<SearchProductQueryResponse> Handle(SearchProductQueryRequest request, CancellationToken cancellationToken)
        {
            SearchProductQueryResponse response = new();
            Expression<Func<Product, bool>>? method = p => p.Name.Contains(request.Query) || p.Brand.Contains(request.Query);
            response.Products = _readRepository.GetAllByPaging(
                method: method,
                pageCount: request.pageCount,
                itemCount: request.itemCount);
            response.totalCount = await _readRepository.CountAsync(method: method);
            return response;
        }
    }
}
