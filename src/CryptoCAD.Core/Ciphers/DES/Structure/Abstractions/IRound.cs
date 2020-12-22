using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CryptoCAD.Core.Tests")]
namespace CryptoCAD.Core.Ciphers.DES.Structure.Abstractions
{
    internal interface IRound
    {
        (uint, uint) Process(uint leftBlock32b, uint rightBlock32b, ulong key48b, RoundResults results);
    }
}