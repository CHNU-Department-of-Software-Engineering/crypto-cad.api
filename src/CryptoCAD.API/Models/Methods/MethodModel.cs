using System;

namespace CryptoCAD.API.Models.Methods
{
    public class MethodModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Family { get; set; }
        public bool IsModifiable { get; set; }
        public string Relation { get; set; }
        public Guid? ParentId { get; set; }
        public int SecretLength { get; set; }
        public string Configuration { get; set; }

    }
}