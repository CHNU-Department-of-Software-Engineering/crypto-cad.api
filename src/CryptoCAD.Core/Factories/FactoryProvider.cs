using System;
using CryptoCAD.Core.Ciphers.DES.Factory;
using CryptoCAD.Core.Factories.Abstractions;
using CryptoCAD.Domain.Entities.Methods.Base;

namespace CryptoCAD.Core.Factories
{
    internal class FactoryProvider : IFactoryProvider
    {
        public ICipherFactory CreateCipherFactory(MethodFamilies family)
        {
            switch (family)
            {
                case MethodFamilies.DES:
                    return new DESCipherFactory();
                default:
                    return null;
            }
        }

        public IHasherFactory CreateHasherFactory(MethodFamilies family)
        {
            throw new NotImplementedException();
        }
    }
}