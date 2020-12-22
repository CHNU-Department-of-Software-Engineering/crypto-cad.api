using System.Security.Cryptography;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CryptoCAD.Core.Tests")]
namespace CryptoCAD.Core.Ciphers.AES
{
    internal class AESCipher : BaseCipher
    {
        /// TODO: Generate new IV
        private static readonly byte[] IV = new byte[16]
        {
            133, 111, 234, 255, 191, 107, 55, 16,
            233, 111, 234, 255, 191, 107, 55, 16
        };

        private readonly Aes _aes;

        public AESCipher()
        {
            _aes = Aes.Create();
        }

        protected override ICryptoTransform GetEncryptor(byte[] key)
        {
            return _aes.CreateEncryptor(key, IV);
        }

        protected override ICryptoTransform GetDecryptor(byte[] key)
        {
            return _aes.CreateDecryptor(key, IV);
        }

        public override void Dispose()
        {
            _aes.Dispose();
        }
    }
}