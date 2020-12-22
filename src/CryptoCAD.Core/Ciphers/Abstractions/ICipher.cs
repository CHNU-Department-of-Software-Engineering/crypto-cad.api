using System;
using System.Runtime.CompilerServices;
using CryptoCAD.Core.Ciphers.Models;

[assembly: InternalsVisibleTo("CryptoCAD.Core.Tests")]
namespace CryptoCAD.Core.Ciphers.Abstractions
{
    internal interface ICipher : IDisposable
    {
        CipherResult Encrypt(byte[] key, byte[] data);
        CipherResult Decrypt(byte[] key, byte[] data);
    }
}