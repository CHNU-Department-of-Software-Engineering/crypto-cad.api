using System;
using CryptoCAD.Core.Ciphers.AES;
using CryptoCAD.Core.Ciphers.DES;
using CryptoCAD.Core.Ciphers.DES.Factory;
using CryptoCAD.Core.Ciphers.GOST;
using CryptoCAD.Core.Ciphers.Abstractions;
using CryptoCAD.Core.Models.Services;
using CryptoCAD.Core.Services.Abstractions;
using CryptoCAD.Domain.Entities.Ciphers;
using CryptoCAD.Domain.Entities.Methods.Base;
using CryptoCAD.Common.Helpers;
using CryptoCAD.Core.Ciphers.Models;

namespace CryptoCAD.Core.Services
{
    public class CipherService : ICipherService
    {
        public ServiceResponse Process(byte[] key, byte[] data, CipherModes mode, MethodFamilies family, string configuration)
        {
            var cipher = GetCipher(family, configuration);
            CipherResult response;

            switch (mode)
            {
                case CipherModes.Encrypt:
                    response = cipher.Encrypt(key, data);
                    break;
                case CipherModes.Decrypt:
                    response = cipher.Decrypt(key, data);
                    break;
                default:
                    throw new NotImplementedException("Cipher operation is not supported!");
            }

            return new ServiceResponse
            {
                Data = response.Data,
                IntermediateResults = response.IntermediateResults
            };
        }

        private ICipher GetCipher(MethodFamilies family, string configuration = null)
        {
            switch (family)
            {
                case MethodFamilies.DES:
                    return string.IsNullOrEmpty(configuration) || !configuration.IsValidJson()
                        ? new DESCipher()
                        : new DESCipherFactory()
                            .CreateCipher(configuration);
                case MethodFamilies.AES:
                    return string.IsNullOrEmpty(configuration) || !configuration.IsValidJson()
                        ? new AESCipher()
                        : throw new NotImplementedException($"{MethodFamilies.AES.ToFriendlyString()} cipher family modification not implemented yet!");
                case MethodFamilies.GOST:
                    return string.IsNullOrEmpty(configuration) || !configuration.IsValidJson()
                        ? new GOSTCipher()
                        : throw new NotImplementedException($"{MethodFamilies.GOST.ToFriendlyString()} cipher family modification not implemented yet!");
                default:
                    throw new ArgumentException($"{family.ToFriendlyString()} is not cipher family!");
            }
        }
    }
}