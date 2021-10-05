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
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory httpClientFactory;

        private readonly ILogger<ProductService> logger;
        public ProductService(IHttpClientFactory httpClientFactory, ILogger<ProductService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<ProductsResponse> GetProductAsync()
        {
            try
            {
                var client = httpClientFactory.CreateClient("ProductsService");

                var response = await client.GetAsync($"api/products");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);

                    return new ProductsResponse()
                    {
                        IsSuccess = true,
                        Products = result,
                        ErrorMessage = null
                    };
                }

                return new ProductsResponse()
                {
                    IsSuccess = false,
                    Products = null,
                    ErrorMessage = response.ReasonPhrase
                };
            }
            catch (Exception ex)
            {

                logger?.LogError(ex.ToString());
                return new ProductsResponse()
                {
                    IsSuccess = false,
                    Products = null,
                    ErrorMessage = ex.Message
                };
            }
        }         
    }
}
    