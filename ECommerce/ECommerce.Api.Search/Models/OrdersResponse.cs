using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Models
{
    public class OrdersResponse
    {
        public bool IsSuccess { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class OrderResponse
    {
        public bool IsSuccess { get; set; }

        public Order Order { get; set; }

        public string ErrorMessage { get; set; }
    }
}
