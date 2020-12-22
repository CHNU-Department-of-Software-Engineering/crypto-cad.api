using System;
using System.Collections.Generic;

namespace CryptoCAD.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(Guid id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void AddRange(IList<T> entities);
        void Remove(T entity);
        void Update(T entity);
        void SaveChanges();
    }
}