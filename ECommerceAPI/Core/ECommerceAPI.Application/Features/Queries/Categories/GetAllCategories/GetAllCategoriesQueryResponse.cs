using ECommerceAPI.Application.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Categories.GetAllCategories
{
    public class GetAllCategoriesQueryResponse
    {
        public IQueryable<GetRootCategoriesResponseDTO> Categories { get; set; }
    }
}
