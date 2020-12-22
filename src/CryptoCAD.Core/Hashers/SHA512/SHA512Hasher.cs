using System.Text;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using CryptoCAD.Common.Helpers;
using CryptoCAD.Core.Hashers.Abstractions;

[assembly: InternalsVisibleTo("CryptoCAD.Core.Tests")]
namespace CryptoCAD.Core.Hashers.SHA512
{
    internal class SHA512Hasher : IHasher
    {
        public string Hash(string data)
        {
            var bytes = data.ToBytes();

            var shaM = new SHA512Managed();
            var hash = shaM.ComputeHash(bytes);

            var hashStr = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                hashStr.Append(string.Format("{0:X2}", hash[i]));
            }

            return hashStr.ToString();
        }
    }
}