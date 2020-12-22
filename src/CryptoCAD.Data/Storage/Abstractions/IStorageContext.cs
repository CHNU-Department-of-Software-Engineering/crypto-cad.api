using System.Collections.Generic;
using CryptoCAD.Domain.Entities.Methods;

namespace CryptoCAD.Data.Storage.Abstractions
{
    public interface IStorageContext
    {
        ICollection<StandardMethod> StandardMethods { get; }

        void SaveChanges();
    }
}