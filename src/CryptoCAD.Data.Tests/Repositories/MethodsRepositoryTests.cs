using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using CryptoCAD.Data.Storage;
using CryptoCAD.Data.Repositories;
using CryptoCAD.Domain.Entities.Methods;
using CryptoCAD.Domain.Repositories;
using CryptoCAD.Data.Storage.Abstractions;

namespace CryptoCAD.Data.Tests.Repositories
{
    [TestFixture]
    public class MethodsRepositoryTests
    {
        private IStorageContext Context;
        private IStandardMethodsRepository Repository;

        [SetUp]
        public void Init()
        {
            var directory = Directory.GetCurrentDirectory();
            var path = Path.Combine(directory, "storage.crptcd");
            Context = new StorageContext(path, true);
            Repository = new StandardMethodsRepository(Context);
        }

        [TestCase("storage.crptcd")]
        public void InitData(string fileName)
        {
            //var directory = Directory.GetCurrentDirectory();
            //var path = Path.Combine(directory, fileName);

            //try
            //{
            //    var context = new StorageContext(path);

            //    var repository = new MethodsRepository(context);

            //    var methods = repository.GetAll();

            //    Assert.AreEqual(7, methods.Count());

            //    var desLibraryMethod = repository.Get(new Guid("30870aee-f7ea-4f6d-aa60-fdfc48cc9a60"));
            //    Assert.AreEqual("des_library", desLibraryMethod.Name.ToLowerInvariant());
            //    Assert.AreEqual(MethodTypes.Cipher.ToFriendlyString(), desLibraryMethod.Type);
            //    Assert.IsFalse(desLibraryMethod.IsEditable);
            //    Assert.AreEqual(8, desLibraryMethod.SecretLength);

            //    var aesMethod = repository.Get(new Guid("8cb02965-ceab-4afb-bd70-3e4382f3ddae"));
            //    Assert.AreEqual("aes", aesMethod.Name.ToLowerInvariant());
            //    Assert.AreEqual(MethodTypes.Cipher.ToFriendlyString(), aesMethod.Type);
            //    Assert.IsFalse(aesMethod.IsModifiable);
            //    Assert.IsFalse(aesMethod.IsEditable);
            //    Assert.AreEqual(16, aesMethod.SecretLength);

            //    var gostMethod = repository.Get(new Guid("5c3ba99c-bb77-4bd6-b171-1df79f129941"));
            //    Assert.AreEqual("gost", gostMethod.Name.ToLowerInvariant());
            //    Assert.AreEqual(MethodTypes.Cipher.ToFriendlyString(), gostMethod.Type);
            //    Assert.IsFalse(gostMethod.IsModifiable);
            //    Assert.IsFalse(gostMethod.IsEditable);
            //    Assert.AreEqual(32, gostMethod.SecretLength);

            //    var sha256Method = repository.Get(new Guid("4ea2b184-f6f2-406d-8f66-0a1949c84872"));
            //    Assert.AreEqual("sha256", sha256Method.Name.ToLowerInvariant());
            //    Assert.AreEqual(MethodTypes.Hash.ToFriendlyString(), sha256Method.Type);
            //    Assert.IsFalse(sha256Method.IsModifiable);
            //    Assert.IsFalse(sha256Method.IsEditable);

            //    var sha512Method = repository.Get(new Guid("c3ba717f-42be-4f68-a11c-dd67ec0423c2"));
            //    Assert.AreEqual("sha512", sha512Method.Name.ToLowerInvariant());
            //    Assert.AreEqual(MethodTypes.Hash.ToFriendlyString(), sha512Method.Type);
            //    Assert.IsFalse(sha512Method.IsModifiable);
            //    Assert.IsFalse(sha512Method.IsEditable);

            //    var md5Method = repository.Get(new Guid("612e8668-fdf5-494d-8982-47468fa539de"));
            //    Assert.AreEqual("md5", md5Method.Name.ToLowerInvariant());
            //    Assert.AreEqual(MethodTypes.Hash.ToFriendlyString(), md5Method.Type);
            //    Assert.IsFalse(md5Method.IsModifiable);
            //    Assert.IsFalse(md5Method.IsEditable);

            //    var desMethod = repository.Get(new Guid("ddc14bd0-a864-4895-addd-8ac11cc68b63"));
            //    Assert.AreEqual("des", desMethod.Name.ToLowerInvariant());
            //    Assert.AreEqual(MethodTypes.Cipher.ToFriendlyString(), desMethod.Type);
            //    Assert.IsTrue(desMethod.IsModifiable);
            //    Assert.IsFalse(desMethod.IsEditable);
            //    Assert.AreEqual(8, desMethod.SecretLength);
            //}
            //finally
            //{
            //    if (File.Exists(path))
            //    {
            //        File.Delete(path);
            //    }
            //}
        }

        [TestCase("storage.crptcd")]
        public void SaveChanges(string fileName)
        {
            //var directory = Directory.GetCurrentDirectory();
            //var path = Path.Combine(directory, fileName);

            //try
            //{
            //    var context = new StorageContext(path);
            //    var repository = new MethodsRepository(context);

            //    var ciphers = repository.GetAll();
            //    Assert.AreEqual(7, ciphers.Count());

            //    var desEntity = repository.Get(new Guid("30870aee-f7ea-4f6d-aa60-fdfc48cc9a60"));
            //    repository.Remove(desEntity);
            //    repository.SaveChanges();
            //    ciphers = repository.GetAll();
            //    Assert.AreEqual(6, ciphers.Count());

            //    context = new StorageContext(path);
            //    repository = new MethodsRepository(context);
            //    ciphers = repository.GetAll();
            //    Assert.AreEqual(6, ciphers.Count());
            //}
            //finally
            //{
            //    if (File.Exists(path))
            //    {
            //        File.Delete(path);
            //    }
            //}
        }

        [TestCase("storage.crptcd")]
        public void DropCreate(string fileName)
        {
            //var directory = Directory.GetCurrentDirectory();
            //var path = Path.Combine(directory, fileName);

            //try
            //{
            //    var context = new StorageContext(path);
            //    var repository = new MethodsRepository(context);

            //    var ciphers = repository.GetAll();
            //    Assert.AreEqual(7, ciphers.Count());

            //    var desEntity = repository.Get(new Guid("30870aee-f7ea-4f6d-aa60-fdfc48cc9a60"));
            //    repository.Remove(desEntity);
            //    repository.SaveChanges();
            //    ciphers = repository.GetAll();
            //    Assert.AreEqual(6, ciphers.Count());

            //    context = new StorageContext(path, true);
            //    repository = new MethodsRepository(context);
            //    ciphers = repository.GetAll();
            //    Assert.AreEqual(7, ciphers.Count());
            //}
            //finally
            //{
            //    if (File.Exists(path))
            //    {
            //        File.Delete(path);
            //    }
            //}
        }

        [Test]
        public void Update()
        {
            var allMethods = Repository.GetAll();
            var count = allMethods.Count();

            var method = allMethods.First();

            var methodId = method.Id;

            method.Name = "New test name";
            Repository.Update(method);

            allMethods = Repository.GetAll();
            Assert.AreEqual(count, allMethods.Count());

            var updated = Repository.GetAll().Where(x => x.Id == methodId);
            Assert.AreEqual(1, updated.Count());

            var updatedMethod = updated.First();
            Assert.IsNotNull(updatedMethod);
            Assert.AreEqual("New test name", updatedMethod.Name);
        }
    }
}