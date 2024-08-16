using ECommerceAPI.Application.DTOs.Categories;

namespace ECommerceAPI.Application.Features.Queries.Categories.GetRootCategories
{
    public class GetRootCategoriesQueryResponse
    {
        public IQueryable<GetRootCategoriesResponseDTO> Categories { get; set; }
    }
}
