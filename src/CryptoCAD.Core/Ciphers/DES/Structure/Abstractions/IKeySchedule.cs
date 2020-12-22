using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CryptoCAD.Core.Tests")]
namespace CryptoCAD.Core.Ciphers.DES.Structure.Abstractions
{
    internal interface IKeySchedule
    {
        ulong[] GenerateSubkeys(byte[] key);
    }
}