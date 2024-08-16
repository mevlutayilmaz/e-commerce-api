using ECommerceAPI.Application.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Categories.GetSubCategories
{
    public class GetSubCategoriesQueryResponse
    {
        public IQueryable<GetSubCategoriesResponseDTO>? SubCategories { get; set; }
    }
}
