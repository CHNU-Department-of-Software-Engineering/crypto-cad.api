using CryptoCAD.Core.Ciphers.Abstractions;

namespace CryptoCAD.Core.Factories.Abstractions
{
    internal interface ICipherFactory
    {
        ICipher CreateCipher(string configuration);
    }
}