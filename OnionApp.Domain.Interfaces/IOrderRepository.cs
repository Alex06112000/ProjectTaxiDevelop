using System;
using System.Collections.Generic;
using OnionApp.Domain.Core;

namespace OnionApp.Domain.Interfaces
{
    public interface IOrderRepository : IDisposable
    {
        IEnumerable<Order> GetAll();
        Order Get(int id);
        void Create(Order car);
    }
}