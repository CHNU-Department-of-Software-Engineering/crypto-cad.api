using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CryptoCAD.Domain.Repositories;
using CryptoCAD.Data.EntityFramework;

namespace CryptoCAD.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly PostgreSqlContext _dbContext;

        public Repository(PostgreSqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Get(Guid id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void AddRange(IList<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}