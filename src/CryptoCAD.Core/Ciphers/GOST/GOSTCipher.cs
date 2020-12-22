using System;
using System.Threading.Tasks;
using CryptoCAD.Core.Ciphers.Abstractions;
using System.Runtime.CompilerServices;
using CryptoCAD.Common.Helpers;
using CryptoCAD.Core.Ciphers.Models;

[assembly: InternalsVisibleTo("CryptoCAD.Core.Tests")]
namespace CryptoCAD.Core.Ciphers.GOST
{
    internal class GOSTCipher : ICipher
    {
        private const byte KEY_LENGHT = 32;
        private byte[][] SubstitutionBox =
        {
              new byte[] { 0x0,0x1,0x2,0x3,0x4,0x5,0x6,0x7,0x8,0x9,0xA,0xB,0xC,0xD,0xE,0xF },
              new byte[] { 0x0,0x1,0x2,0x3,0x4,0x5,0x6,0x7,0x8,0x9,0xA,0xB,0xC,0xD,0xE,0xF },
              new byte[] { 0x0,0x1,0x2,0x3,0x4,0x5,0x6,0x7,0x8,0x9,0xA,0xB,0xC,0xD,0xE,0xF },
              new byte[] { 0x0,0x1,0x2,0x3,0x4,0x5,0x6,0x7,0x8,0x9,0xA,0xB,0xC,0xD,0xE,0xF },
              new byte[] { 0x0,0x1,0x2,0x3,0x4,0x5,0x6,0x7,0x8,0x9,0xA,0xB,0xC,0xD,0xE,0xF },
              new byte[] { 0x0,0x1,0x2,0x3,0x4,0x5,0x6,0x7,0x8,0x9,0xA,0xB,0xC,0xD,0xE,0xF },
              new byte[] { 0x0,0x1,0x2,0x3,0x4,0x5,0x6,0x7,0x8,0x9,0xA,0xB,0xC,0xD,0xE,0xF },
              new byte[] { 0x0,0x1,0x2,0x3,0x4,0x5,0x6,0x7,0x8,0x9,0xA,0xB,0xC,0xD,0xE,0xF }
        };

        public CipherResult Decrypt(byte[] key, byte[] data)
        {
            var blocks64b = data.ToUInts64();
            var decryptedBlocks64b = new ulong[blocks64b.Length];

            var subkeys = GenerateKeys(key);
            var result = new byte[data.Length];
            var block = new byte[8];

            var isParallel = false;

            if (isParallel)
            {
                Parallel.For(0, data.Length / 8, i =>
                {
                    //Array.Copy(data, 8 * i, block, 0, 8);
                    //Array.Copy(DecryptBlock(block, subkeys), 0, result, 8 * i, 8);
                });
            }
            else
            {
                for (int i = 0; i < blocks64b.Length; i++) // N blocks 64bits length.
                {
                    decryptedBlocks64b[i] = DecryptBlock(blocks64b[i], subkeys);
                    //Array.Copy(data, 8 * i, block, 0, 8);
                    //Array.Copy(DecryptBlock(block, subkeys), 0, result, 8 * i, 8);
                }
            }
            return new CipherResult
            {
                Data = decryptedBlocks64b.ToBytes().Trim()
            };
        }

        public CipherResult Encrypt(byte[] key, byte[] data)
        {
            var blocks64b = data.ToUInts64();
            var encryptedBlocks64b = new ulong[blocks64b.Length];

            var subkeys = GenerateKeys(key);
            var result = new byte[data.Length];
            var block = new byte[8];

            var isParallel = false;

            if (isParallel)
            {
                Parallel.For(0, data.Length / 8, i =>
                {
                    //Array.Copy(data, 8 * i, block, 0, 8);
                    //Array.Copy(EncryptBlock(block, subkeys), 0, result, 8 * i, 8);
                });
            }
            else
            {
                for (int i = 0; i < blocks64b.Length; i++) // N blocks 64bits length.
                {
                    encryptedBlocks64b[i] = EncryptBlock(blocks64b[i], subkeys);
                    //Array.Copy(data, 8 * i, block, 0, 8);
                    //Array.Copy(EncryptBlock(block, subkeys), 0, result, 8 * i, 8);
                }
            }

            return new CipherResult
            {
                Data = encryptedBlocks64b.ToBytes().Trim()
            };
        }

        public void Dispose()
        {

        }

        private ulong DecryptBlock(ulong block64b, uint[] keys)
        {
            uint N1 = (uint)(block64b & 0xFFFFFFFF), N2 = (uint)((block64b >> 32) & 0xFFFFFFFF);

            for (byte i = 0; i < 32; i++)
            {
                int keyIndex = i < 8 ? (i % 8) : (7 - i % 8); // to 24th cycle : 0 to 7; after - 7 to 0;
                var s = (N1 + keys[keyIndex]) % uint.MaxValue; // (N1 + X[i]) mod 2^32
                s = Substitution(s); // substitute from box
                s = (s << 11) | (s >> 21);
                s ^= N2;
                if (i < 31) // last cycle : N1 don't change; N2 = s;
                {
                    N2 = N1;
                    N1 = s;
                }
                else
                {
                    N2 = s;
                }
            }

            var output = new byte[8];
            var N1buff = BitConverter.GetBytes(N1);
            var N2buff = BitConverter.GetBytes(N2);

            for (byte i = 0; i < 4; i++)
            {
                output[i] = N1buff[i];
                output[4 + i] = N2buff[i];
            }

            return BitConverter.ToUInt64(output, 0);
        }
        private ulong EncryptBlock(ulong block64b, uint[] keys)
        {
            uint N1 = (uint)(block64b & 0xFFFFFFFF), N2 = (uint)((block64b >> 32) & 0xFFFFFFFF);

            for (byte i = 0; i < 32; i++)
            {
                var keyIndex = i < 24 ? (i % 8) : (7 - i % 8); // to 24th cycle : 0 to 7; after - 7 to 0;
                var s = (N1 + keys[keyIndex]) % uint.MaxValue; // (N1 + X[i]) mod 2^32
                s = Substitution(s); // substitute from box
                s = (s << 11) | (s >> 21);
                s ^= N2; // ( s + N2 ) mod 2
                //N2 = N1;
                //N1 = s;
                if (i < 31) // last cycle : N1 don't change; N2 = s;
                {
                    N2 = N1;
                    N1 = s;
                }
                else
                {
                    N2 = s;
                }
            }

            var output = new byte[8];
            var N1buff = BitConverter.GetBytes(N1);
            var N2buff = BitConverter.GetBytes(N2);

            for (int i = 0; i < 4; i++)
            {
                output[i] = N1buff[i];
                output[4 + i] = N2buff[i];
            }

            return BitConverter.ToUInt64(output, 0);
        }

        private uint[] GenerateKeys(byte[] key)
        {
            if (key.Length != KEY_LENGHT)
            {
                throw new ArgumentException($"Key lenght should be {KEY_LENGHT}, not {key.Length}");
            }

            var subkeys = new uint[8];

            for (byte i = 0; i < 8; i++)
            {
                subkeys[i] = BitConverter.ToUInt32(key, 4 * i);
            }

            return subkeys;
        }
        private uint Substitution(uint value)
        {
            uint output = 0;

            for (byte i = 0; i < 8; i++)
            {
                var temp = (byte)((value >> (4 * i)) & 0x0f);
                temp = SubstitutionBox[i][temp];
                output |= (uint)temp << (4 * i);
            }

            return output;
        }
    }
}