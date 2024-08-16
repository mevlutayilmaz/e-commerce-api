using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Categories.GetCategory
{
    public class GetCategoryQueryRequest : IRequest<GetCategoryQueryResponse>
    {
        public string Name { get; set; }
    }
}
