using CryptoCAD.Core.Models.Services;
using CryptoCAD.Domain.Entities.Methods.Base;

namespace CryptoCAD.Core.Services.Abstractions
{
    public interface IHashService
    {
        ServiceResponse Hash(
            byte[] data,
            MethodFamilies family,
            string configuration);
    }
}