using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CryptoCAD.Core.Tests")]
namespace CryptoCAD.Core.Ciphers.DES.Structure.Abstractions
{
    internal interface IFunction
    {
        uint Process(uint block32b, ulong key48b);
    }
}