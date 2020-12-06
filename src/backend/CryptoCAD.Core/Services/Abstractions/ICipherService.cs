﻿using CryptoCAD.Domain.Entities.Ciphers;

namespace CryptoCAD.Core.Services.Abstractions
{
    public interface ICipherService
    {
        byte[] Process(string name, CipherModes mode, byte[] key, byte[] data, string configuration);
    }
}