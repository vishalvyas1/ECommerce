using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Models
{
    public class CustomersResponse
    {
        public bool IsSuccess { get; set; }

        public IEnumerable<Customer> Customers { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class CustomerResponse
    {
        public bool IsSuccess { get; set; }

        public Customer Customer { get; set; }

        public string ErrorMessage { get; set; }
    }
}
