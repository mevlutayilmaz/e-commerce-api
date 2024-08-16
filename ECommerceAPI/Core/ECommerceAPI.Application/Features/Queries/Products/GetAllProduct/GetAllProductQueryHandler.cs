using ECommerceAPI.Application.DTOs.Prodcuts;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ECommerceAPI.Application.Features.Queries.Products.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IReadRepository<Product> _readRepository;

        public GetAllProductQueryHandler(IReadRepository<Product> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            GetAllProductQueryResponse response = new();
            Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null;
            Expression<Func<Product, bool>>? method = null;

            switch (request.SortBy.ToLower())
            {
                case "price":
                    orderBy = request.IsAscending
                        ? q => q.OrderBy(p => p.Price)
                        : q => q.OrderByDescending(p => p.Price);
                    break;
                case "created date":
                    orderBy = request.IsAscending
                        ? q => q.OrderBy(p => p.CreatedDate)
                        : q => q.OrderByDescending(p => p.CreatedDate);
                        break;
                default:
                    break;
            }
            if (request.CategoryId is not null) method = p => p.CategoryId == request.CategoryId;

            response.Products = _readRepository.GetAllByPaging(
                    method: method,
                    include: q => q.Include(p => p.Category),
                    orderBy: orderBy,
                    pageCount: request.pageCount,
                    itemCount: request.itemCount)
                .Select(p => new GetProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Brand = p.Brand,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    ImageUrl = p.ImageUrl,
                    ModelNo = p.ModelNo,
                    Price = p.Price,
                    Rating = p.Rating,
                    RatingCount = p.RatingCount
                });
            response.totalCount = await _readRepository.CountAsync(method: method);

            return response;
        }
    }
}
