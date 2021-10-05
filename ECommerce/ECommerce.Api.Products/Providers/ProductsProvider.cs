using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using ECommerce.Api.Products.RPCModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {

        private readonly ProductsDbContext dbContext;

        private readonly ILogger<ProductsProvider> logger;

        private readonly IMapper mapper;
        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product() { Id = 1, Name = "KeyBoard", Inventory = 2, Price = 100 });
                dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Inventory = 2, Price = 200 });
                dbContext.Products.Add(new Db.Product() { Id = 3, Name = "PlayStation", Inventory = 2, Price = 300 });
                dbContext.Products.Add(new Db.Product() { Id = 4, Name = "HeadPhone", Inventory = 2, Price = 400 });
                dbContext.Products.Add(new Db.Product() { Id = 5, Name = "Batter", Inventory = 2, Price = 500 });
                dbContext.Products.Add(new Db.Product() { Id = 6, Name = "Cover", Inventory = 2, Price = 600 });

                dbContext.SaveChanges();
            }
        }

        public async Task<ProductsResponse> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();

                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return new ProductsResponse()
                    {
                        IsSuccess = true,
                        Products = result,
                        ErrorMessage = string.Empty

                    };
                }
                return new ProductsResponse()
                {
                    IsSuccess = false,
                    Products = null,
                    ErrorMessage = "Not Found"

                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message.ToString());
                return new ProductsResponse()
                {
                    IsSuccess = false,
                    Products = null,
                    ErrorMessage = ex.Message

                };
            }
            
        }

        public async Task<ProductResponse> GetProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (product != null)
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);
                    return new ProductResponse()
                    {
                        IsSuccess = true,
                        Product =result,
                        ErrorMessage = null
                    };
                }
                return new ProductResponse()
                {
                    IsSuccess = false,
                    Product = null,
                    ErrorMessage = "Not Found"
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message.ToString());
                return new ProductResponse()
                {
                    IsSuccess = false,
                    Product = null,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
