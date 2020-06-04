using System;
using System.Collections.Generic;
using OnionApp.Domain.Core;

namespace OnionApp.Domain.Interfaces
{
    public interface ITaxistRepository : IDisposable
    {
        IEnumerable<Taxist> GetBookList();
        Taxist GetBook(int id);
        void Create(Taxist item);
        void Update(Taxist item);
        void Delete(int id);
        void Save();
    }
}