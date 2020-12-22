using System;
using System.Linq;
using System.Collections.Generic;
using CryptoCAD.Domain.Repositories;
using CryptoCAD.Domain.Entities.Methods;
using CryptoCAD.Data.Storage.Abstractions;

namespace CryptoCAD.Data.Repositories
{
    public class StandardMethodsRepository : IStandardMethodsRepository
    {
        private readonly IStorageContext Context;

        public StandardMethodsRepository(IStorageContext context)
        {
            Context = context;
        }

        public void Add(StandardMethod entity)
        {
            Context.StandardMethods.Add(entity);
        }

        public void AddRange(IList<StandardMethod> entities)
        {
            foreach (var entity in entities)
            {
                Context.StandardMethods.Add(entity);
            }
        }

        public StandardMethod Get(Guid id)
        {
            return Context.StandardMethods.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<StandardMethod> GetAll()
        {
            return Context.StandardMethods;
        }

        public void Remove(StandardMethod entity)
        {
            Context.StandardMethods.Remove(entity);
        }

        public void Update(StandardMethod entity)
        {
            var toUpdate = Get(entity.Id);
            Remove(toUpdate);
            Add(entity);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}