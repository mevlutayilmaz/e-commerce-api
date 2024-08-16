using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceAPI.Application.DTOs.Categories;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Features.Queries.Categories.GetSubCategories
{
    public class GetSubCategoriesQueryHandler : IRequestHandler<GetSubCategoriesQueryRequest, GetSubCategoriesQueryResponse>
    {
        readonly IReadRepository<Category> _readRepository;
        readonly IMapper _mapper;

        public GetSubCategoriesQueryHandler(IReadRepository<Category> readRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<GetSubCategoriesQueryResponse> Handle(GetSubCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            GetSubCategoriesQueryResponse response = new();
            var subCategories = _readRepository
                                    .GetAll(c => c.ParentCategoryId == Guid.Parse(request.ParentId))
                                    .Include(sc => sc.SubCategories);

            response.SubCategories = subCategories.ProjectTo<GetSubCategoriesResponseDTO>(_mapper.ConfigurationProvider);
            return response;
        }
    }
}
