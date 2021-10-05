using ECommerce.Api.Customers.RPCModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<CustomersResponse> GetCustomersAsync();

        Task<CustomerResponse> GetCustomerAsync(int id);
    }
}
