﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Orders.GetOrderById
{
    public class GetOrderByIdQueryResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Address { get; set; }
        public object BasketItems { get; set; }
        public string Description { get; set; }
        public string OrderCode { get; set; }
    }
}
