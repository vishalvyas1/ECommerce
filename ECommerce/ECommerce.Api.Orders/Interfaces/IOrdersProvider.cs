﻿using ECommerce.Api.Orders.RPCModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        public Task<OrdersResponse> GetOrdersByIdAsync(int customerId);
    }
}
