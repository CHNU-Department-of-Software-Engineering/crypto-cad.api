using System;

namespace CryptoCAD.Domain.Entities.Ciphers
{
    public enum CipherModes
    {
        Encrypt = 0,
        Decrypt = 1
    }

    public static class CipherModesExtensions
    {
        private const string ENCRYPTION_NAME = "encryption";
        private const string DECRYPTION_NAME = "decryption";

        public static string ToFriendlyString(this CipherModes mode)
        {
            switch (mode)
            {
                case CipherModes.Encrypt:
                    return ENCRYPTION_NAME;
                case CipherModes.Decrypt:
                    return DECRYPTION_NAME;
                default:
                    throw new NotSupportedException($"Cipher mode {mode} is not supported!");
            }
        }

        public static CipherModes ToCipherMode(this string type)
        {
            switch (type)
            {
                case ENCRYPTION_NAME:
                    return CipherModes.Encrypt;
                case DECRYPTION_NAME:
                    return CipherModes.Decrypt;
                case "none":
                default:
                    throw new NotSupportedException($"Cipher mode {type} is not supported! Only cipher modes supported: '{ENCRYPTION_NAME}' and '{DECRYPTION_NAME}'");
            }
        }
    }
}