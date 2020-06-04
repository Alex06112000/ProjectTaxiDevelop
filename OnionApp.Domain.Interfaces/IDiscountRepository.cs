using System;
using System.Collections.Generic;
using OnionApp.Domain.Core;

namespace OnionApp.Domain.Interfaces
{
    public interface IDiscountRepository 
    {
        IEnumerable<Discount> GetAll();
        Discount Get(int id);
        void Create(Discount discount);
    }
}