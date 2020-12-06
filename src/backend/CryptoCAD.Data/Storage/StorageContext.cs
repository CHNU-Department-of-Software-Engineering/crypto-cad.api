﻿using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using CryptoCAD.Domain.Entities.Methods;
using CryptoCAD.Data.Storage.Abstractions;
using CryptoCAD.Common.CiphersConfiguration;

namespace CryptoCAD.Data.Storage
{
    public class StorageContext : IStorageContext
    {
        private readonly string StoragePath;

        private ICollection<Method> _methods;
        public ICollection<Method> Methods => _methods;

        public StorageContext(string storagePath, bool dropCreate = false)
        {
            StoragePath = storagePath;

            if (!File.Exists(StoragePath) || dropCreate)
            {
                Seed();
                Pull();
            }
            else
            {
                Fetch();
            }
        }

        public void SaveChanges()
        {
            Pull();
        }

        private void Seed()
        {
            var desConfigurations = JsonConvert.SerializeObject(DESConfigurations.GetConfiguration());

            _methods = new List<Method>
            {
                new Method
                {
                    Id = new Guid("ddc14bd0-a864-4895-addd-8ac11cc68b63"),
                    Name = "des",
                    Type = MethodTypes.Cipher,
                    IsModifiable = false,
                    IsEditable = false,
                    Configuration = desConfigurations,
                    SecretLength = 8
                },
                new Method
                {
                    Id = new Guid("30870aee-f7ea-4f6d-aa60-fdfc48cc9a60"),
                    Name = "des_library",
                    Type = MethodTypes.Cipher,
                    IsModifiable = false,
                    IsEditable = false,
                    Configuration = string.Empty,
                    SecretLength = 8
                },
                new Method
                {
                    Id = new Guid("8cb02965-ceab-4afb-bd70-3e4382f3ddae"),
                    Name = "aes",
                    Type = MethodTypes.Cipher,
                    IsModifiable = false,
                    IsEditable = false,
                    Configuration = string.Empty,
                    SecretLength = 16
                },
                new Method
                {
                    Id = new Guid("5c3ba99c-bb77-4bd6-b171-1df79f129941"),
                    Name = "gost",
                    Type = MethodTypes.Cipher,
                    IsModifiable = false,
                    IsEditable = false,
                    Configuration = string.Empty,
                    SecretLength = 32
                },
                new Method
                {
                    Id = new Guid("4ea2b184-f6f2-406d-8f66-0a1949c84872"),
                    Name = "sha256",
                    Type = MethodTypes.Hash,
                    IsModifiable = false,
                    IsEditable = false,
                    Configuration = string.Empty
                }
            };
        }
        private void Fetch()
        {
            var json = File.ReadAllText(StoragePath);
            var context = JsonConvert.DeserializeObject<Context>(json);

            _methods = context.Methods;
        }
        private void Pull()
        {
            var context = new Context
            {
                Methods = _methods
            };

            var json = JsonConvert.SerializeObject(context);
            File.WriteAllText(StoragePath, json);
        }
    }

    class Context
    {
        public ICollection<Method> Methods { get; set; }
    }
}