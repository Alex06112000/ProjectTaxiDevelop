using OnionApp.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionApp.Infrastructure.Data;

namespace ProjectTaxi.Repositories
{
    public class UnitOfWork: IDisposable
    {
        private TaxiContext db = new TaxiContext();
        private CarRepository carRepository;
        private DiscountRepository discountRepository;
        private OrderRepository orderRepository;
        private TaxistRepository taxiRepository;
        private UserRepository userRepository;
        public CarRepository Cars
        {
            get
            {
                if(carRepository==null)
                {
                    carRepository = new CarRepository(db);
                    return carRepository;
                }else
                return carRepository;
            }

        }
       public DiscountRepository Discounts
        {
            get
            {
                if (discountRepository == null)
                {
                    discountRepository = new DiscountRepository(db);
                    return discountRepository;
                }
                else
                    return discountRepository;
            }
        }
        public OrderRepository Orders
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new OrderRepository(db);
                    return orderRepository;
                }
                else
                    return orderRepository;
            }
        }
        public TaxistRepository Taxists
        {
            get
            {
                if (taxiRepository == null)
                {
                    taxiRepository = new TaxistRepository(db);
                    return taxiRepository;
                }
                else
                    return taxiRepository;
            }
        }
        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(db);
                    return userRepository;
                }
                else
                    return userRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
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
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
