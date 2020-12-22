using Newtonsoft.Json;

namespace CryptoCAD.Domain.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CipherKeyEntity
    {
        private const byte CHAR_ENCODING_LENGTH = 8;

        [JsonProperty]
        public string Type { get; set; }
        [JsonProperty]
        public byte Length { get; set; }
        public short BitLenght => (short)(Length * CHAR_ENCODING_LENGTH);
    }
}