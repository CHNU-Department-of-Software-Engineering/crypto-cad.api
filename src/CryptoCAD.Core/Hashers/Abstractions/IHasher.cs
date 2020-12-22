using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CryptoCAD.Core.Tests")]
namespace CryptoCAD.Core.Hashers.Abstractions
{
    internal interface IHasher
    {
        string Hash(string data);
    }
}