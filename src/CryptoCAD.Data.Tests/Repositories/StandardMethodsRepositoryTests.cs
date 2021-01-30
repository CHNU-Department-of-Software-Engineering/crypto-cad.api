using System;
using System.Linq;
using System.Collections.Generic;
using CryptoCAD.Data.Repositories;
using CryptoCAD.Domain.Repositories;
using CryptoCAD.Domain.Entities.Methods;
using CryptoCAD.Domain.Entities.Methods.Base;
using CryptoCAD.Common.Configurations.Ciphers;
using MongoDB.Driver;
using NUnit.Framework;
using Newtonsoft.Json;

namespace CryptoCAD.Data.Tests.Repositories
{
    internal class StandardMethodsRepositoryTests
    {
        private IStandardMethodsRepository _standardMethodsRepository;

        private List<StandardMethod> _standardMethods;

        [SetUp]
        public void Setup()
        {
            var client = new MongoClient(Constants.MongoDbConnectionUri);
            var collection = client.GetDatabase("crypto_cad").GetCollection<StandardMethod>("standardMethods_test");
            _standardMethodsRepository = new StandartMethodsRepository(collection);

            var desConfigurations = JsonConvert.SerializeObject(DESConfigurationExtension.GetConfiguration().ToDTO());

            _standardMethods = new List<StandardMethod>
            {
                new StandardMethod
                {
                    Id = new Guid("ddc14bd0-a864-4895-addd-8ac11cc68b63"),
                    Name = "DES",
                    Type = MethodTypes.SymmetricCipher,
                    Family = MethodFamilies.DES,
                    IsModifiable = true,
                    Relation = StandardMethodRelations.Parent,
                    SecretLength = 8,
                    Configuration = desConfigurations
                },
                new StandardMethod
                {
                    Id = new Guid("30870aee-f7ea-4f6d-aa60-fdfc48cc9a60"),
                    Name = "des_library",
                    Type = MethodTypes.SymmetricCipher,
                    Family = MethodFamilies.DES,
                    IsModifiable = false,
                    Relation = StandardMethodRelations.Parent,
                    SecretLength = 8
                },
                new StandardMethod
                {
                    Id = new Guid("8cb02965-ceab-4afb-bd70-3e4382f3ddae"),
                    Name = "AES",
                    Type = MethodTypes.SymmetricCipher,
                    Family = MethodFamilies.AES,
                    IsModifiable = false,
                    Relation = StandardMethodRelations.Parent,
                    SecretLength = 16,
                },
                new StandardMethod
                {
                    Id = new Guid("5c3ba99c-bb77-4bd6-b171-1df79f129941"),
                    Name = "GOST",
                    Type = MethodTypes.SymmetricCipher,
                    Family = MethodFamilies.GOST,
                    IsModifiable = false,
                    Relation = StandardMethodRelations.Parent,
                    SecretLength = 32
                },
                new StandardMethod
                {
                    Id = new Guid("4ea2b184-f6f2-406d-8f66-0a1949c84872"),
                    Name = "SHA256",
                    Type = MethodTypes.Hash,
                    Family = MethodFamilies.SHA256,
                    IsModifiable = false,
                    Relation = StandardMethodRelations.Parent
                },
                new StandardMethod
                {
                    Id = new Guid("c3ba717f-42be-4f68-a11c-dd67ec0423c2"),
                    Name = "SHA512",
                    Type = MethodTypes.Hash,
                    Family = MethodFamilies.SHA512,
                    IsModifiable = false,
                    Relation = StandardMethodRelations.Parent
                },
                new StandardMethod
                {
                    Id = new Guid("612e8668-fdf5-494d-8982-47468fa539de"),
                    Name = "MD5",
                    Type = MethodTypes.Hash,
                    Family = MethodFamilies.MD5,
                    IsModifiable = false,
                    Relation = StandardMethodRelations.Parent
                }
            };
        }

        [Test]
        public void Add()
        {
            var expected_method = _standardMethods.First();

            try
            {
                _standardMethodsRepository.Add(expected_method);

                var actual_method = _standardMethodsRepository.Get(expected_method.Id);

                Assert.AreEqual(expected_method.Id, actual_method.Id);
            }
            finally
            {
                _standardMethodsRepository.Remove(expected_method);
            }
        }

        [Test]
        public void AddRange()
        {
            try
            {
                _standardMethodsRepository.AddRange(_standardMethods);
                var actual = _standardMethodsRepository.GetAll();

                Assert.AreEqual(_standardMethods.Count(), actual.Count());
            }
            finally
            {
                _standardMethods.ForEach(method => _standardMethodsRepository.Remove(method));
            }
        }

        [Test]
        public void Update()
        {
            var expected_method = _standardMethods.First();

            try
            {
                _standardMethodsRepository.Add(expected_method);

                var actual_method = _standardMethodsRepository.Get(expected_method.Id);
                actual_method.Name += "_Updated";

                _standardMethodsRepository.Update(actual_method);
                var updated_method = _standardMethodsRepository.Get(expected_method.Id);

                Assert.AreEqual(actual_method.Name, updated_method.Name);
            }
            finally
            {
                _standardMethodsRepository.Remove(expected_method);
            }
        }

        [Test]
        public void Remove()
        {
            var expected_method = _standardMethods.First();

            _standardMethodsRepository.Add(expected_method);
            _standardMethodsRepository.Remove(expected_method);

            var actual_method = _standardMethodsRepository.Get(expected_method.Id);

            Assert.IsNull(actual_method);
        }
    }
}