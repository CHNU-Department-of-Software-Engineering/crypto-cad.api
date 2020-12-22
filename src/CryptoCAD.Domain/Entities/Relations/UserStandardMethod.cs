using System;

namespace CryptoCAD.Domain.Entities.Relations
{
    public class UserStandardMethod : Entity
    {
        public Guid UserId { get; set; }
        public Guid StandardMethodId { get; set; }
    }
}