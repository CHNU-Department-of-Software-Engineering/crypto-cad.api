using CryptoCAD.Domain.Entities.Methods.Base;

namespace CryptoCAD.Core.Factories.Abstractions
{
    internal interface IFactoryProvider
    {
        ICipherFactory CreateCipherFactory(MethodFamilies family);
        IHasherFactory CreateHasherFactory(MethodFamilies family);
    }
}