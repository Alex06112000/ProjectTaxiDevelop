
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionApp.Domain.Core;
using OnionApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OnionApp.Infrastructure.Data
{
    public class TaxistRepository : IRepository<Taxist>
    {
        private TaxiContext db;

        public TaxistRepository(TaxiContext context)
        {
            this.db = context;
        }
        public void Create(Taxist taxist)
        {
            db.Taxists.Add(taxist);
        }

        public void Delete(int id)
        {
            Taxist taxist = db.Taxists.Find(id);
            if (taxist != null)
                db.Taxists.Remove(taxist);
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
        public Taxist Get(int id)
        {
            return db.Taxists.Find(id);
        }

        public IEnumerable<Taxist> GetList()
        {
            return db.Taxists;
        }

        public void Save() { 
           db.SaveChanges();
        }

        public void Update(Taxist taxist)
        {
            db.Entry(taxist).State = EntityState.Modified;
        }
    }
}
