using System;

namespace CryptoCAD.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}