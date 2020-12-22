using System.Security.Cryptography;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CryptoCAD.Core.Tests")]
namespace CryptoCAD.Core.Ciphers.DES
{
    internal class DESCipher : BaseCipher
    {
        private static readonly byte[] IV = new byte[8]
        {
            133, 111, 234, 255, 191, 107, 55, 16
        };

        private readonly System.Security.Cryptography.DES _des;

        public DESCipher()
        {
            _des = System.Security.Cryptography.DES.Create();

        }

        protected override ICryptoTransform GetEncryptor(byte[] key)
        {
            return _des.CreateEncryptor(key, IV);
        }

        protected override ICryptoTransform GetDecryptor(byte[] key)
        {
            return _des.CreateDecryptor(key, IV);
        }

        public override void Dispose()
        {
            _des.Dispose();
        }
    }
}