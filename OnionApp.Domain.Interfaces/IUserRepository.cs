using System;
using System.Collections.Generic;
using OnionApp.Domain.Core;

namespace OnionApp.Domain.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        IEnumerable<User> GetBookList();
        User GetBook(int id);
        void Create(User item);
        void Update(User item);
        void Delete(int id);
        void Save();
    }
}