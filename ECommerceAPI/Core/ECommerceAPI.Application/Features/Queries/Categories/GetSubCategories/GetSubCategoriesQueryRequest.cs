using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Categories.GetSubCategories
{
    public class GetSubCategoriesQueryRequest : IRequest<GetSubCategoriesQueryResponse>
    {
        public string ParentId { get; set; }
    }
}
