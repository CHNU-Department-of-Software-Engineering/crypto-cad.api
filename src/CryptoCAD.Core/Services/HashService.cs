using System;
using CryptoCAD.Common.Helpers;
using CryptoCAD.Core.Hashers.MD5;
using CryptoCAD.Core.Hashers.SHA256;
using CryptoCAD.Core.Hashers.SHA512;
using CryptoCAD.Core.Hashers.Abstractions;
using CryptoCAD.Core.Models.Services;
using CryptoCAD.Core.Services.Abstractions;
using CryptoCAD.Domain.Entities.Methods.Base;

namespace CryptoCAD.Core.Services
{
    public class HashService : IHashService
    {
        public ServiceResponse Hash(byte[] data, MethodFamilies family, string configuration)
        {
            var hasher = GetHasher(family, configuration);
            var bytes = hasher.Hash(data.ToString(ConvertMode.UTF8)).ToBytes();

            return new ServiceResponse
            {
                Data = bytes,
                IntermediateResults = string.Empty
            };
        }

        private IHasher GetHasher(MethodFamilies family, string configuration = null)
        {
            switch (family)
            {
                case MethodFamilies.SHA256:
                    return string.IsNullOrEmpty(configuration) || !configuration.IsValidJson()
                        ? new SHA256Hasher()
                        : throw new NotImplementedException($"{MethodFamilies.SHA256.ToFriendlyString()} hasher family modification not implemented yet!");
                case MethodFamilies.SHA512:
                    return string.IsNullOrEmpty(configuration) || !configuration.IsValidJson()
                        ? new SHA512Hasher()
                        : throw new NotImplementedException($"{MethodFamilies.SHA512.ToFriendlyString()} hasher family modification not implemented yet!");
                case MethodFamilies.MD5:
                    return string.IsNullOrEmpty(configuration) || !configuration.IsValidJson()
                        ? new MD5Hasher()
                        : throw new NotImplementedException($"{MethodFamilies.MD5.ToFriendlyString()} hasher family modification not implemented yet!");
                default:
                    throw new ArgumentException($"{family.ToFriendlyString()} is not hasher family!");
            }
        }
    }
}