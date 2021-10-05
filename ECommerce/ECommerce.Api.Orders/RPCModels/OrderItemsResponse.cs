using ECommerce.Api.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.RPCModels
{
    public class OrderItemsResponse
    {
        public bool IsSuccess { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class OrderItemResponse
    {
        public bool IsSuccess { get; set; }

        public OrderItem OrderItem { get; set; }

        public string ErrorMessage { get; set; }
    }
}
