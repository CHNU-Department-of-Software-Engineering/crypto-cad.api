using CryptoCAD.Core.Hashers.Abstractions;

namespace CryptoCAD.Core.Factories.Abstractions
{
    internal interface IHasherFactory
    {
        IHasher CreateHasher(string configuration);
    }
}