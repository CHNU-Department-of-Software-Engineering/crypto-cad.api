namespace CryptoCAD.Core.Utilities
{
    public static class ByteUtill
    {
        public static int GetBit(byte[] data, int position)
        {
            int positionByte = position / 8;
            int positionBit = position % 8;
            byte value = data[positionByte];
            return value >> (8 - (positionBit + 1)) & 0x0001;
        }
        public static void SetBit(byte[] data, int position, int value)
        {
            int positionByte = position / 8;
            int positionBit = position % 8;
            byte old = data[positionByte];
            old = (byte)(((0xFF7F >> positionBit) & old) & 0x00FF);
            data[positionByte] = (byte)((value << (8 - (positionBit + 1))) | old);
        }

        public static byte[] SelectBits(byte[] data, byte[] map)
        {
            var numberOfBytes = (map.Length - 1) / 8 + 1;
            var output = new byte[numberOfBytes];
            for (var i = 0; i < map.Length; i++)
            {
                var value = GetBit(data, map[i] - 1);
                SetBit(output, i, value);
            }

            return output;
        }
        public static byte[] SelectBits(byte[] input, int position, int length)
        {
            var numberOfBytes = (length - 1) / 8 + 1;
            var output = new byte[numberOfBytes];
            for (var i = 0; i < length; i++)
            {
                var value = GetBit(input, position + i);
                SetBit(output, i, value);
            }

            return output;
        }

        public static byte[] SplitBytes(byte[] input, int length)
        {
            int numberOfBytes = (8 * input.Length - 1) / length + 1;
            var output = new byte[numberOfBytes];
            for (int i = 0; i < numberOfBytes; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    int value = GetBit(input, length * i + j);
                    SetBit(output, 8 * i + j, value);
                }
            }

            return output;
        }

        public static byte[] XORBytes(byte[] a, byte[] b)
        {
            var output = new byte[a.Length];
            for (var i = 0; i < a.Length; i++)
            {
                output[i] = (byte)(a[i] ^ b[i]);
            }

            return output;
        }

        public static byte[] ConcatenateBits(byte[] a, int aLength, byte[] b, int bLength)
        {
            var numberOfBytes = (aLength + bLength - 1) / 8 + 1;
            var output = new byte[numberOfBytes];
            int j = 0;
            for (var i = 0; i < aLength; i++)
            {
                var value = GetBit(a, i);
                SetBit(output, j, value);
                j++;
            }

            for (var i = 0; i < bLength; i++)
            {
                var value = GetBit(b, i);
                SetBit(output, j, value);
                j++;
            }

            return output;
        }

        public static byte[] RotateLeft(byte[] input, int length, int step)
        {
            var numberOfBytes = (length - 1) / 8 + 1;
            var output = new byte[numberOfBytes];
            for (var i = 0; i < length; i++)
            {
                var value = GetBit(input, (i + step) % length);
                SetBit(output, i, value);
            }

            return output;
        }
    }
}