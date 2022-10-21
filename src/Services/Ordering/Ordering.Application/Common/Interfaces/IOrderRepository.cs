﻿using Contracts.Common.Interfaces;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Common.Interfaces
{
    public interface IOrderRepository : IRepositoryBaseAsync<Order, long>
    {
        Task<IEnumerable<Order>> GetOrdersByUserNameAsync(string userName);
        //Task<Order> CreateOrderAsync(Order order);
        void CreateOrder(Order order);
        Task<Order> UpdateOrderAsync(Order order);
        void DeleteOrder(Order order);

    }
}
