using System;
using System.Text;
using Newtonsoft.Json.Linq;

namespace CryptoCAD.Common.Helpers
{
    public static class CryptoHelper
    {
        public static string ToString(this byte[] data, ConvertMode mode)
        {
            switch (mode)
            {
                case ConvertMode.UTF8:
                    return Encoding.UTF8.GetString(data);
                case ConvertMode.ASCII:
                    return Encoding.ASCII.GetString(data);
                case ConvertMode.BASE64:
                    return Convert.ToBase64String(data);
                default:
                    return Encoding.UTF8.GetString(data);
            }
        }
        public static byte[] ToBytes(this string data, ConvertMode mode = ConvertMode.UTF8)
        {
            switch (mode)
            {
                case ConvertMode.UTF8:
                    return Encoding.UTF8.GetBytes(data);
                case ConvertMode.ASCII:
                    return Encoding.ASCII.GetBytes(data);
                case ConvertMode.BASE64:
                    return Convert.FromBase64String(data);
                default:
                    return Encoding.UTF8.GetBytes(data); ;
            }
        }

        public static ulong[] ToUInts64(this byte[] data)
        {
            const byte BLOCK_LENGTH = 8;

            var blocks64b = new ulong[GetBlocksLength(data, BLOCK_LENGTH)];

            var index = 0;
            for (uint i = 0; i < data.Length; i += BLOCK_LENGTH)
            {
                if (data.Length - i >= BLOCK_LENGTH)
                {
                    blocks64b[index] = BitConverter.ToUInt64(data, (int)i);
                }
                else
                {
                    var lastBlock = GetBytes(data, i, BLOCK_LENGTH);
                    blocks64b[index] = BitConverter.ToUInt64(lastBlock, 0);
                    break;
                }
                index++;
            }

            return blocks64b;
        }
        public static uint[] ToUInts32(this byte[] data)
        {
            const byte BLOCK_LENGTH = 4;

            var blocks32b = new uint[GetBlocksLength(data, BLOCK_LENGTH)];

            var index = 0;
            for (uint i = 0; i < data.Length; i += BLOCK_LENGTH)
            {
                if (data.Length - i >= BLOCK_LENGTH)
                {
                    blocks32b[index] = BitConverter.ToUInt32(data, (int)i);
                }
                else
                {
                    var lastBlock = GetBytes(data, i, BLOCK_LENGTH);
                    blocks32b[index] = BitConverter.ToUInt32(lastBlock, 0);
                    break;
                }
                index++;
            }

            return blocks32b;
        }
        public static ushort[] ToUInts16(this byte[] data)
        {
            const byte BLOCK_LENGTH = 2;

            var blocks16b = new ushort[GetBlocksLength(data, BLOCK_LENGTH)];

            var index = 0;
            for (uint i = 0; i < data.Length; i += BLOCK_LENGTH)
            {
                if (data.Length - i >= BLOCK_LENGTH)
                {
                    blocks16b[index] = BitConverter.ToUInt16(data, (int)i);
                }
                else
                {
                    var lastBlock = GetBytes(data, i, BLOCK_LENGTH);
                    blocks16b[index] = BitConverter.ToUInt16(lastBlock, 0);
                    break;
                }
                index++;
            }

            return blocks16b;
        }

        public static byte[] ToBytes(this ulong[] blocks64b)
        {
            var bytes = new byte[blocks64b.Length * 8];

            var index = 0;
            for (var i = 0; i < blocks64b.Length; i++)
            {
                var block64b = BitConverter.GetBytes(blocks64b[i]);
                for (byte j = 0; j < block64b.Length; j++)
                {
                    bytes[index++] = block64b[j];
                }
            }

            return bytes;
        }
        public static byte[] ToBytes(this uint[] blocks32b)
        {
            var bytes = new byte[blocks32b.Length * 4];

            var index = 0;
            for (var i = 0; i < blocks32b.Length; i++)
            {
                var block32b = BitConverter.GetBytes(blocks32b[i]);
                for (byte j = 0; j < block32b.Length; j++)
                {
                    bytes[index++] = block32b[j];
                }
            }

            return bytes;
        }
        public static byte[] ToBytes(this ushort[] blocks16b)
        {
            var bytes = new byte[blocks16b.Length * 2];

            var index = 0;
            for (var i = 0; i < blocks16b.Length; i++)
            {
                var block16b = BitConverter.GetBytes(blocks16b[i]);
                for (var j = 0; j < block16b.Length; j++)
                {
                    bytes[index++] = block16b[j];
                }
            }

            return bytes;
        }

        public static string ToHexadecimalString(this byte[] bytes)
        {
            var hexadecimal = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
            {
                hexadecimal.AppendFormat("{0:x2}", b);
            }
            return hexadecimal.ToString();
        }

        public static byte[] Trim(this byte[] bytes)
        {
            var index = bytes.Length - 1;
            while (bytes[index] == 0)
            {
                --index;
                if (index < 0)
                {
                    return new byte[1];
                }
            }
            if (index == bytes.Length - 1)
            {
                return bytes;
            }
            var trimmedBytes = new byte[index + 1];
            Array.Copy(bytes, trimmedBytes, index + 1);
            return trimmedBytes;
        }

        private static byte[] GetBytes(byte[] bytes, uint startIndex, uint length)
        {
            var newBytes = new byte[length];
            var index = 0;
            for (var j = startIndex; j < bytes.Length; j++)
            {
                newBytes[index++] = bytes[j];
            }
            return newBytes;
        }
        private static uint GetBlocksLength(byte[] bytes, byte blockLength)
        {
            uint length = (uint)(bytes.Length / blockLength);
            length += (uint)(bytes.Length - length * blockLength == 0 ? 0 : 1);
            return length;
        }

        public static bool IsValidJson(this string value)
        {
            value = value.Trim();
            try
            {
                if (value.StartsWith("{") && value.EndsWith("}"))
                {
                    JToken.Parse(value);
                }
                else if (value.StartsWith("[") && value.EndsWith("]"))
                {
                    JArray.Parse(value);
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public enum ConvertMode
    {
        UTF8 = 1,
        ASCII = 2,
        BASE64 = 3
    }
}