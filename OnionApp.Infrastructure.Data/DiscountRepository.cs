using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionApp.Domain.Core;
using OnionApp.Domain.Interfaces;

namespace OnionApp.Infrastructure.Data
{
    public class DiscountRepository : IRepository<Discount>
    {
        private TaxiContext db;

        public DiscountRepository(TaxiContext context)
        {
            this.db = context;
        }
        public void Create(Discount discount)
        {
            db.Discounts.Add(discount);
        }

        public void Delete(int id)
        {
            Discount discount = db.Discounts.Find(id);
            if (discount != null)
                db.Discounts.Remove(discount);
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
        public Discount Get(int id)
        {
            return db.Discounts.Find(id);
        }

        public IEnumerable<Discount> GetList()
        {
            return db.Discounts;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Discount discount)
        {
            db.Entry(discount).State = EntityState.Modified;
        }
    }
}
