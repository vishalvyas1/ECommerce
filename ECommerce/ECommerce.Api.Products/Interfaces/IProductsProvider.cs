using ECommerce.Api.Products.Models;
using ECommerce.Api.Products.RPCModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<ProductsResponse> GetProductsAsync();

        Task<ProductResponse> GetProductAsync(int id);
    }
}
