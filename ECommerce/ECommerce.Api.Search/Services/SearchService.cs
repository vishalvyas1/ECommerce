using ECommerce.Api.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICustomersService customersService;

        public SearchService(IOrderService orderService, IProductService productService, ICustomersService customersService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customersService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await orderService.GetOrdersByIdAsync(customerId);

            var productResults = await productService.GetProductAsync();

            var customerResults = await customersService.GetCustomersAsync(customerId);

            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productResults.IsSuccess ? 
                            productResults.Products.FirstOrDefault(p => p.Id == item.Id)?.Name : "Product Info Unavailable";
                    }
                }

                var result = new
                {
                    Orders = ordersResult.Orders,
                    Customer = customerResults.IsSuccess ? 
                                customerResults.Customer :
                                new Models.Customer { Name = "Customer Data Unavailable"}
                    
                };

                return (true, result);
            }

            return (false, null);

        }
    }
}
