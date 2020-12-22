using System.IO;
using System.Security.Cryptography;
using CryptoCAD.Common.Helpers;
using CryptoCAD.Core.Ciphers.Abstractions;
using CryptoCAD.Core.Ciphers.Models;

namespace CryptoCAD.Core.Ciphers
{
    internal abstract class BaseCipher : ICipher
    {
        /// <inheritdoc/>
        public CipherResult Decrypt(byte[] key, byte[] data)
        {
            byte[] decryptedData;

            using (var memoryStream = new MemoryStream(data))
            {
                using (var cryptoStream = new CryptoStream(memoryStream, GetDecryptor(key), CryptoStreamMode.Read))
                {
                    decryptedData = new byte[data.Length];

                    cryptoStream.Read(decryptedData, 0, decryptedData.Length);
                }
            }

            return new CipherResult
            {
                Data = decryptedData.Trim()
            };
        }

        /// <inheritdoc/>
        public CipherResult Encrypt(byte[] key, byte[] data)
        {
            byte[] encryptedData;

            using (var memoryStream = new MemoryStream())
            {

                using (var cryptoStream = new CryptoStream(memoryStream, GetEncryptor(key), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();

                    encryptedData = memoryStream.ToArray();
                }
            }

            return new CipherResult
            {
                Data = encryptedData.Trim()
            };
        }

        protected abstract ICryptoTransform GetEncryptor(byte[] key);
        protected abstract ICryptoTransform GetDecryptor(byte[] key);

        public abstract void Dispose();
    }
}