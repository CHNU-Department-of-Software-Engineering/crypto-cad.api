using System.Text;
using System.Runtime.CompilerServices;
using CryptoCAD.Common.Helpers;
using CryptoCAD.Core.Hashers.Abstractions;

[assembly: InternalsVisibleTo("CryptoCAD.Core.Tests")]
namespace CryptoCAD.Core.Hashers.MD5
{
    internal class MD5Hasher : IHasher
    {
        public string Hash(string data)
        {
            var bytes = data.ToBytes();

            var md5 = System.Security.Cryptography.MD5.Create();
            var hash = md5.ComputeHash(bytes);

            var hashStr = new StringBuilder();
            for (byte i = 0; i < hash.Length; i++)
            {
                hashStr.Append(string.Format("{0:X2}", hash[i]));
            }

            return hashStr.ToString();
        }
    }
}