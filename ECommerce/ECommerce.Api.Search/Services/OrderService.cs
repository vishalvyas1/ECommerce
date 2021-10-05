using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class OrderService : IOrderService
    {

        private readonly IHttpClientFactory httpClientFactory;

        private readonly ILogger<OrderService> logger;

        public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<OrdersResponse> GetOrdersByIdAsync(int customerId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("OrdersService");
                var response = await client.GetAsync($"api/orders/{customerId}"); 

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);

                    return new OrdersResponse()
                    {
                        IsSuccess = true,
                        Orders = result,
                        ErrorMessage = null
                    };
                }

                return new OrdersResponse()
                {
                    IsSuccess = false,
                    Orders = null,
                    ErrorMessage = response.ReasonPhrase
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new OrdersResponse()
                {
                    IsSuccess = false,
                    Orders = null,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
