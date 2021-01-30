using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using CryptoCAD.Domain.Repositories;
using CryptoCAD.Domain.Entities.Methods;

namespace CryptoCAD.Data.Repositories
{
    public class StandartMethodsRepository : IStandardMethodsRepository
    {
        private readonly IMongoCollection<StandardMethod> _standardMethods;

        public StandartMethodsRepository(IMongoClient mongoClient)
        {
            _standardMethods = mongoClient.GetDatabase("crypto_cad").GetCollection<StandardMethod>("standardMethods");
        }

        public StandartMethodsRepository(IMongoCollection<StandardMethod> collection)
        {
            _standardMethods = collection;
        }

        public void Add(StandardMethod entity)
        {
            _standardMethods.InsertOne(entity);
        }

        public void AddRange(IList<StandardMethod> entities)
        {
            _standardMethods.InsertMany(entities);
        }

        public StandardMethod Get(Guid id)
        {
            return _standardMethods.Find(new BsonDocument("_id", id)).FirstOrDefault();
        }

        public IEnumerable<StandardMethod> GetAll()
        {
            return (IEnumerable<StandardMethod>)_standardMethods.Find(Builders<StandardMethod>.Filter.Empty).ToList();
        }

        public async void Remove(StandardMethod entity)
        {
            await _standardMethods.DeleteOneAsync(new BsonDocument("_id", entity.Id));
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(StandardMethod entity)
        {
            var filter = Builders<StandardMethod>.Filter.Eq("_id", entity.Id);

            var update = Builders<StandardMethod>.Update
                .Set("Name", entity.Name)
                .Set("Type", entity.Type)
                .Set("Family", entity.Family)
                .Set("IsModifiable", entity.IsModifiable)
                .Set("Relation", entity.Relation)
                .Set("ParentId", entity.ParentId)
                .Set("SecretLength", entity.SecretLength)
                .Set("Configuration", entity.Configuration);

            _standardMethods.UpdateOne(filter, update);
        }
    }
}
