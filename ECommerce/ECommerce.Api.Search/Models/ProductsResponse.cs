using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Models
{
    public class ProductsResponse
    {
        public bool IsSuccess { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class ProductResponse
    {
        public bool IsSuccess { get; set; }

        public Product Product { get; set; }

        public string ErrorMessage { get; set; }
    }
}
