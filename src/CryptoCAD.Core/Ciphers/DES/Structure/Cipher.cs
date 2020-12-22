using System;
using System.Runtime.CompilerServices;
using CryptoCAD.Common.Helpers;
using CryptoCAD.Core.Utilities;
using CryptoCAD.Core.Ciphers.Abstractions;
using CryptoCAD.Core.Ciphers.DES.Structure.Abstractions;
using CryptoCAD.Domain.Entities.Ciphers;
using CryptoCAD.Core.Ciphers.Models;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("CryptoCAD.Core.Tests")]
namespace CryptoCAD.Core.Ciphers.DES.Structure
{
    internal class Cipher : ICipher
    {
        private readonly IKeySchedule KeySchedule;
        private readonly IRound Round;

        private readonly byte[] IP;
        private readonly byte[] FP;
        private readonly IntermediateResults Results;

        public Cipher(IKeySchedule keySchedule, IRound round, byte[] ip, byte[] fp, IntermediateResults results)
        {
            KeySchedule = keySchedule;
            Round = round;
            IP = ip;
            FP = fp;
            Results = results;
        }

        public CipherResult Decrypt(byte[] key, byte[] data)
        {
            var bytes = Process(key, data, CipherModes.Encrypt);
            var json = JsonConvert.SerializeObject(Results);
            return new CipherResult
            {
                Data = bytes.Trim(),
                IntermediateResults = json
            };
        }
        public CipherResult Encrypt(byte[] key, byte[] data)
        {
            var bytes = Process(key, data, CipherModes.Decrypt);
            var json = JsonConvert.SerializeObject(Results);
            return new CipherResult
            {
                Data = bytes.Trim(),
                IntermediateResults = json
            };
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private byte[] Process(byte[] key, byte[] data, CipherModes mode)
        {
            if (key.Length != 8)
            {
                throw new ArgumentException("Invalid DES key lenght!");
            }
            var keys = KeySchedule.GenerateSubkeys(key);
            if (mode == CipherModes.Decrypt)
            {
                Array.Reverse(keys);
            }

            var blocks64b = data.ToUInts64();
            var resultBlocks64b = new ulong[blocks64b.Length];

            Results.Blocks = new BlockResults[blocks64b.Length];
            for (byte i = 0; i < blocks64b.Length; i++)
            {
                Results.Blocks[i] = new BlockResults
                {
                    InitialBlock = BitConverter.GetBytes(blocks64b[i]).ToHexadecimalString()
                };
                Results.Blocks[i].Rounds = new RoundResults[keys.Length];
                resultBlocks64b[i] = Permutation(blocks64b[i], IP);
                Results.Blocks[i].AfterFirstPermutation = BitConverter.GetBytes(resultBlocks64b[i]).ToHexadecimalString();
                resultBlocks64b[i] = FeistelCipher(resultBlocks64b[i], keys, Results.Blocks[i].Rounds);
                Results.Blocks[i].FinalBlock = BitConverter.GetBytes(resultBlocks64b[i]).ToHexadecimalString();
                resultBlocks64b[i] = Permutation(resultBlocks64b[i], FP);
                Results.Blocks[i].AfterSecondPermutation = BitConverter.GetBytes(resultBlocks64b[i]).ToHexadecimalString();
            }

            return resultBlocks64b.ToBytes();
        }

        private ulong FeistelCipher(ulong block64b, ulong[] keys, RoundResults[] RoundResults)
        {
            uint N1 = (uint)(block64b & 0xFFFFFFFF), N2 = (uint)((block64b >> 32) & 0xFFFFFFFF);
            for (byte i = 0; i < keys.Length; i++)
            {
                RoundResults[i] = new RoundResults();
                var res = Round.Process(N1, N2, keys[i], RoundResults[i]);
                N1 = res.Item1;
                N2 = res.Item2;
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
 
        private ulong Permutation(ulong block64b, byte[] permutationTable)
        {
            ulong newBlock64b = 0;
            for (byte i = 0; i < 64; ++i)
            {
                newBlock64b |= ((block64b >> (64 - permutationTable[i])) & 0x01) << (63 - i);
            }
            return newBlock64b;
        }
    }
}