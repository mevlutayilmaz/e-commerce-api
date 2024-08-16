using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceAPI.Application.DTOs.Categories;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Categories.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, GetAllCategoriesQueryResponse>
    {
        readonly IReadRepository<Category> _readRepository;
        readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(IReadRepository<Category> readRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<GetAllCategoriesQueryResponse> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            GetAllCategoriesQueryResponse response = new();
            var categories = _readRepository.GetAll();
            response.Categories = categories.ProjectTo<GetRootCategoriesResponseDTO>(_mapper.ConfigurationProvider);
            return response;
        }
    }
}
