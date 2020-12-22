using System;

namespace CryptoCAD.Domain.Entities.Methods.Base
{
    public enum MethodTypes
    {
        SymmetricCipher = 0,
        AsymmetricCipher = 1,
        Hash = 2
    }

    public static class MethodTypesExtensions
    {
        private const string SYMMETRIC_CIPHER_NAME = "cipher";
        private const string ASYMMETRIC_CIPHER_NAME = "asymmetric cipher";
        private const string HASH_NAME = "hash";

        public static string ToFriendlyString(this MethodTypes type)
        {
            switch (type)
            {
                case MethodTypes.SymmetricCipher:
                    return SYMMETRIC_CIPHER_NAME;
                case MethodTypes.AsymmetricCipher:
                    return ASYMMETRIC_CIPHER_NAME;
                case MethodTypes.Hash:
                    return HASH_NAME;
                default:
                    throw new NotSupportedException($"Method type {type} is not supported!");
            }
        }

        public static MethodTypes ToMethodType(this string type)
        {
            switch (type.ToLowerInvariant())
            {
                case SYMMETRIC_CIPHER_NAME:
                    return MethodTypes.SymmetricCipher;
                case ASYMMETRIC_CIPHER_NAME:
                    return MethodTypes.AsymmetricCipher;
                case HASH_NAME:
                    return MethodTypes.Hash;
                default:
                    throw new NotSupportedException($"Method type {type} is not supported! Only method types supported: '{SYMMETRIC_CIPHER_NAME}', '{ASYMMETRIC_CIPHER_NAME}', '{HASH_NAME}'");
            }
        }
    }
}