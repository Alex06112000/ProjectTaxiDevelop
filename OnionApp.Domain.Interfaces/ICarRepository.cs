using System;
using System.Collections.Generic;
using OnionApp.Domain.Core;

namespace OnionApp.Domain.Interfaces
{
    public interface ICarRepository : IDisposable
    {
            IEnumerable<Car> GetAll();
            Car Get(int id);
            void Create(Car car);
    }
}