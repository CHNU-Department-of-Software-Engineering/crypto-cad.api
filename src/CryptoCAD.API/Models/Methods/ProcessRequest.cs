using System;

namespace CryptoCAD.API.Models.Methods
{
    public class ProcessRequest
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Family { get; set; }
        public string Mode { get; set; }
        public string Secret { get; set; }
        public string Data { get; set; }
        public string Configuration { get; set; }
    }
}