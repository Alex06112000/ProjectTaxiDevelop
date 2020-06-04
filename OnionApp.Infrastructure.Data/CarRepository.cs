using Microsoft.EntityFrameworkCore;
using System;
using OnionApp.Domain.Core;
using OnionApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnionApp.Infrastructure.Data
{
    public class CarRepository : IRepository<Car>
    {
        private TaxiContext db;

        public CarRepository(TaxiContext context)
        {
            this.db = context;
        }
        public void Create(Car car)
        {
            db.Cars.Add(car);
        }

        public void Delete(int id)
        {
            Car car = db.Cars.Find(id);
            if (car != null)
                db.Cars.Remove(car);
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public Car Get(int id)
        {
            return db.Cars.Find(id);
        }

        public IEnumerable<Car> GetList()
        {
            return db.Cars;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Car car)
        {
            db.Entry(car).State = EntityState.Modified;
        }
    }
}
