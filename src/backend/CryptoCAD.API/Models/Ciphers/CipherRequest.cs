﻿namespace CryptoCAD.API.Models
{
    public class CipherRequest
    {
        public string Name { get; set; }
        public string Mode { get; set; }
        public string Key { get; set; }
        public string Data { get; set; }
    }
}