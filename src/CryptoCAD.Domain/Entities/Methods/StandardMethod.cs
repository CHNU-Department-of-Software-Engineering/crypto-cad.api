using System;
using CryptoCAD.Domain.Entities.Methods.Base;

namespace CryptoCAD.Domain.Entities.Methods
{
    public class StandardMethod : Method
    {
        public bool IsModifiable { get; set; }

        public StandardMethodRelations Relation { get; set; }
        public Guid? ParentId { get; set; }

        public ushort SecretLength { get; set; }
        public string Configuration { get; set; }
    }
}