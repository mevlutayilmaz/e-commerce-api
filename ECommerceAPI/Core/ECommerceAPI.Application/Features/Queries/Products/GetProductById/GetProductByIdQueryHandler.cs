using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Products.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>
    {
        readonly IReadRepository<Product> _readRepository;

        public GetProductByIdQueryHandler(IReadRepository<Product> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            GetProductByIdQueryResponse response = new();
            response.Product = await _readRepository.GetByIdAsync(request.Id);
            return response;
        }
    }
}
