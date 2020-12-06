﻿using System;

namespace CryptoCAD.API.Models.Ciphers
{
    public class CipherProcessRequest
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Mode { get; set; }
        public string Key { get; set; }
        public string Data { get; set; }
        public string Configuration { get; set; }
    }
}