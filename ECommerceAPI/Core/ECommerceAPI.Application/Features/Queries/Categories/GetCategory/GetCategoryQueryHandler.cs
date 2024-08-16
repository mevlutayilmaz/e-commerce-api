using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Categories.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQueryRequest, GetCategoryQueryResponse>
    {
        readonly IReadRepository<Category> _readRepository;

        public GetCategoryQueryHandler(IReadRepository<Category> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<GetCategoryQueryResponse> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var category = await _readRepository.GetSingleAsync(c => c.Name == request.Name);
            if (category == null)
                return new();
            return new()
            {
                Id = category.Id,
            };
        }
    }
}
