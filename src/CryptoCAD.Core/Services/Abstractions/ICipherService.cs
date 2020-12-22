using CryptoCAD.Core.Models.Services;
using CryptoCAD.Domain.Entities.Ciphers;
using CryptoCAD.Domain.Entities.Methods.Base;

namespace CryptoCAD.Core.Services.Abstractions
{
    public interface ICipherService
    {
        ServiceResponse Process(
            byte[] key,
            byte[] data,
            CipherModes mode,
            MethodFamilies family,
            string configuration);
    }
}