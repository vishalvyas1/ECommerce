using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.RPCModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomersDbContext dbContext;

        private readonly ILogger<CustomerProvider> logger;

        private readonly IMapper mapper;

        public CustomerProvider(CustomersDbContext dbContext, ILogger<CustomerProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        public async Task<CustomerResponse> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(p => p.Id == id);

                if (customer != null)
                {
                    var result = mapper.Map<Db.Customer, Model.Customer>(customer);
                    return new CustomerResponse()
                    {
                        IsSuccess = true,
                        Customer = result,
                        ErrorMessage = string.Empty

                    };
                }
                return new CustomerResponse()
                {
                    IsSuccess = false,
                    Customer = null,
                    ErrorMessage = "Not Found"

                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message.ToString());
                return new CustomerResponse()
                {
                    IsSuccess = false,
                    Customer = null,
                    ErrorMessage = ex.Message

                };
            }
        }

        public async Task<CustomersResponse> GetCustomersAsync()
        {
            try
            {
                var products = await dbContext.Customers.ToListAsync();

                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Model.Customer>>(products);
                    return new CustomersResponse()
                    {
                        IsSuccess = true,
                        Customers = result,
                        ErrorMessage = string.Empty

                    };
                }
                return new CustomersResponse()
                {
                    IsSuccess = false,
                    Customers = null,
                    ErrorMessage = "Not Found"

                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message.ToString());
                return new CustomersResponse()
                {
                    IsSuccess = false,
                    Customers = null,
                    ErrorMessage = ex.Message

                };
            }
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Add(new Db.Customer() { Id = 1, Name = "John D", Address = "476 ABC" });
                dbContext.Add(new Db.Customer() { Id = 2, Name = "Sam S", Address = "456 ABC" });
                dbContext.Add(new Db.Customer() { Id = 3, Name = "Antony A", Address = "466 ABC" });
                dbContext.Add(new Db.Customer() { Id = 4, Name = "Harry Potter", Address = "486 ABC" });
                dbContext.Add(new Db.Customer() { Id = 5, Name = "Play Hard", Address = "576 ABC" });
                dbContext.Add(new Db.Customer() { Id = 6, Name = "Jaimme Lennister", Address = "976 ABC" });

                dbContext.SaveChanges();
            }
        }
    }
}
