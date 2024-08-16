using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceAPI.Application.DTOs.Categories;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Categories.GetRootCategories
{
    public class GetRootCategoriesQueryHandler : IRequestHandler<GetRootCategoriesQueryRequest, GetRootCategoriesQueryResponse>
    {
        readonly IReadRepository<Category> _readRepository;
        readonly IMapper _mapper;

        public GetRootCategoriesQueryHandler(IReadRepository<Category> readRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<GetRootCategoriesQueryResponse> Handle(GetRootCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            GetRootCategoriesQueryResponse response = new();
            var categories = _readRepository.GetAll(c => c.ParentCategoryId == null);
            response.Categories = categories.ProjectTo<GetRootCategoriesResponseDTO>(_mapper.ConfigurationProvider);
            return response;
        }
    }
}
