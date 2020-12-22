using System;

namespace CryptoCAD.Domain.Entities
{
    public class CipherSetup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Configuration { get; set; }
    }
}