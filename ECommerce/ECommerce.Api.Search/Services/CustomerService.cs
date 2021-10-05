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
    public class CustomerService : ICustomersService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<CustomerService> logger;

        public CustomerService(IHttpClientFactory httpClientFactory, ILogger<CustomerService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<CustomerResponse> GetCustomersAsync(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("CustomersService");

                var response =  await client.GetAsync($"api/customers/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<Customer>(content, options);

                    return new CustomerResponse()
                    {
                        IsSuccess = true,
                        Customer = result,
                        ErrorMessage = null
                    };
                }

                return new CustomerResponse()
                {
                    IsSuccess = false,
                    Customer = null,
                    ErrorMessage = response.ReasonPhrase
                };


            }
            catch (Exception ex)
            {

                logger?.LogError(ex.ToString());

                return new CustomerResponse()
                {
                    IsSuccess = false,
                    Customer =null,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
