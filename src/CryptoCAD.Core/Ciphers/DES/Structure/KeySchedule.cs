using System;
using System.Runtime.CompilerServices;
using CryptoCAD.Common.Configurations.Ciphers;
using CryptoCAD.Common.Helpers;
using CryptoCAD.Core.Ciphers.DES.Structure.Abstractions;

[assembly: InternalsVisibleTo("CryptoCAD.Core.Tests")]
namespace CryptoCAD.Core.Ciphers.DES.Structure
{
    internal class KeySchedule : IKeySchedule
    {
        private readonly byte[] PC1_PERMUTATION_TABLE;
        private readonly byte[] PC2_PERMUTATION_TABLE;
        private readonly byte[] ROTATIONS_TABLE;
        private readonly KeyScheduleResults Results;

        public KeySchedule(byte[] pc1PermutationTable, byte[] pc2PermutationTable, byte[] rotationsTable, KeyScheduleResults results)
        {
            if (pc1PermutationTable is null)
            {
                throw new ArgumentNullException(nameof(pc1PermutationTable));
            }

            if (pc2PermutationTable is null)
            {
                throw new ArgumentNullException(nameof(pc2PermutationTable));
            }

            if (rotationsTable is null)
            {
                throw new ArgumentNullException(nameof(rotationsTable));
            }

            if (pc1PermutationTable.Length != DESConfigurationExtension.PC1_PERMUTATION_TABLE_LENGTH)
            {
                throw new ArgumentException($"PC1 Permutation table should have {DESConfigurationExtension.PC1_PERMUTATION_TABLE_LENGTH} lenght, not {pc1PermutationTable.Length}");
            }

            if (pc2PermutationTable.Length != DESConfigurationExtension.PC2_PERMUTATION_TABLE_LENGTH)
            {
                throw new ArgumentException($"PC2 Permutation table should have {DESConfigurationExtension.PC2_PERMUTATION_TABLE_LENGTH} lenght, not {pc2PermutationTable.Length}");
            }

            PC1_PERMUTATION_TABLE = pc1PermutationTable;
            PC2_PERMUTATION_TABLE = pc2PermutationTable;
            ROTATIONS_TABLE = rotationsTable;
            Results = results;
        }

        public ulong[] GenerateSubkeys(byte[] key)
        {
            var key64b = BitConverter.ToUInt64(key, 0);
            Results.InitialKey = key.Trim().ToHexadecimalString();

            var keyParts = KeyPermutation56bitsTo28bits(key64b);
            Results.Subkeys = new SubkeysResults[ROTATIONS_TABLE.Length];
            var keySchedule = KeyExpansionTo48bits(keyParts.Item1, keyParts.Item2, ROTATIONS_TABLE);

            return keySchedule;
        }

        private (uint, uint) KeyPermutation56bitsTo28bits(ulong block56b)
        {
            uint k1 = 0, k2 = 0;
            for (byte i = 0; i < 28; ++i)
            {
                k1 |= (uint)(((block56b >> (64 - PC1_PERMUTATION_TABLE[i])) & 0x01) << (31 - i));
                k2 |= (uint)(((block56b >> (64 - PC1_PERMUTATION_TABLE[i + 28])) & 0x01) << (31 - i));
            }
            return (k1, k2);
        }
        private ulong[] KeyExpansionTo48bits(uint block28b_1, uint block28b_2, byte[] keyShifts)
        {
            Results.InitialLeftKey = BitConverter.GetBytes(block28b_1).Trim().ToHexadecimalString();
            Results.InitialRightKey = BitConverter.GetBytes(block28b_2).Trim().ToHexadecimalString();
            var keys48b = new ulong[keyShifts.Length];
            for (byte i = 0; i < keyShifts.Length; ++i)
            {
                Results.Subkeys[i] = new SubkeysResults();
                block28b_1 = block28b_1.LeftShift28b(keyShifts[i]);
                block28b_2 = block28b_2.LeftShift28b(keyShifts[i]);
                Results.Subkeys[i].KeyPartLeft = BitConverter.GetBytes(block28b_1).Trim().ToHexadecimalString();
                Results.Subkeys[i].KeyPartRight = BitConverter.GetBytes(block28b_2).Trim().ToHexadecimalString();
                var block56b = Join28bitsTo56bits(block28b_1, block28b_2);
                keys48b[i] = KeyContractionPermutation(block56b);
                Results.Subkeys[i].Subkey = BitConverter.GetBytes(keys48b[i]).Trim().ToHexadecimalString();
            }
            return keys48b;
        }

        private ulong Join28bitsTo56bits(uint block28b_1, uint block28b_2)
        {
            ulong block56b = (block28b_1 >> 4);
#pragma warning disable CS0675 // Bitwise-or operator used on a sign-extended operand
            block56b = ((block56b << 32) | block28b_2) << 4;
#pragma warning restore CS0675 // Bitwise-or operator used on a sign-extended operand
            return block56b;
        }
        private ulong KeyContractionPermutation(ulong block56b)
        {
            ulong block48b = 0;
            for (byte i = 0; i < 48; ++i)
            {
                block48b |= ((block56b >> (64 - PC2_PERMUTATION_TABLE[i])) & 0x01) << (63 - i);
            }
            return block48b;
        }
    }
}