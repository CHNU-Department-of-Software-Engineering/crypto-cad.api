using System;
using System.Runtime.CompilerServices;
using CryptoCAD.Common.Configurations.Ciphers;
using CryptoCAD.Core.Ciphers.DES.Structure.Abstractions;

[assembly: InternalsVisibleTo("CryptoCAD.Core.Tests")]
namespace CryptoCAD.Core.Ciphers.DES.Structure
{
    internal class Function : IFunction
    {
        private const byte BLOCK_SIZE = 32;
        private const byte EXTENDED_BLOCK_SIZE = 48;

        private readonly byte[] ExpansionPermutationTable;
        private readonly byte[] PermutationTable;
        private readonly byte[][] SubstitutionBoxes;

        public Function(
            byte[] expansionPermutationTable,
            byte[] permutationTable,
            byte[][] substitutionBoxes
            )
        {
            if (expansionPermutationTable.Length != DESConfigurationExtension.EXPANSION_PERMUTATION_TABLE_LENGTH)
            {
                throw new ArgumentOutOfRangeException(nameof(expansionPermutationTable));
            }
            if (permutationTable.Length != DESConfigurationExtension.PERMUTATION_TABLE_LENGTH)
            {
                throw new ArgumentOutOfRangeException(nameof(permutationTable));
            }
            if (substitutionBoxes.Length != DESConfigurationExtension.SUBSTITUTION_BOXES_LENGTH)
            {
                throw new ArgumentOutOfRangeException(nameof(substitutionBoxes));
            }
            if (substitutionBoxes[0].Length != DESConfigurationExtension.SUBSTITUTION_BOX_LENGTH)
            {
                throw new ArgumentOutOfRangeException(nameof(substitutionBoxes));
            }

            ExpansionPermutationTable = expansionPermutationTable;
            PermutationTable = permutationTable;
            SubstitutionBoxes = substitutionBoxes;
        }

        public uint Process(uint block32b, ulong key48b)
        {
            var block48b = ExpansionPermutation(block32b);
            block48b ^= key48b;
            block32b = Substitution(block48b);
            block32b = Permutation(block32b);
            return block32b;
        }

        private ulong ExpansionPermutation(uint block32b)
        {
            ulong block48b = 0;
            for (byte i = 0; i < EXTENDED_BLOCK_SIZE; ++i)
            {
                block48b |= ((block32b >> (BLOCK_SIZE - ExpansionPermutationTable[i])) & 0x01) << (63 - i);
            }

            return block32b;
        }
        private uint Substitution(ulong block48b)
        {
            var blocks6b = Split48bitsTo6bits(block48b);
            var blocks4b = Substitution6bitsTo4bits(blocks6b);
            var block32b = Join4bitsTo32bits(blocks4b);
            return block32b;
        }
        private uint Permutation(uint block32b)
        {
            uint new_block32b = 0;
            for (byte i = 0; i < BLOCK_SIZE; ++i)
            {
                new_block32b |= ((block32b >> (BLOCK_SIZE - PermutationTable[i])) & 0x01) << (31 - i);
            }
            return new_block32b;
        }

        private byte[] Split64bitsTo8bits(ulong block64b)
        {
            byte[] blocks8b = new byte[8];
            for (byte i = 0; i < 8; ++i)
            {
                blocks8b[i] = (byte)(block64b >> ((7 - i) * 8));
            }

            return blocks8b;
        }
        private byte[] Split48bitsTo6bits(ulong block48b)
        {
            byte[] blocks6b = new byte[8];
            for (byte i = 0; i < 8; ++i)
            {
                blocks6b[i] = (byte)((block48b >> (58 - (i * 6))) << 2);
            }
            return blocks6b;
        }
        uint Join4bitsTo32bits(byte[] blocks4b)
        {
            uint block32b = 0;
            for (byte p = (byte)blocks4b.Length; p < blocks4b.Length + 4; ++p)
            {
                block32b = (block32b << 8) | p;
            }
            return block32b;
        }

        private byte[] Substitution6bitsTo4bits(byte[] blocks6b)
        {
            byte block2b, block4b, index;
            byte[] blocks4b = new byte[8];

            for (byte i = 0, j = 0; i < 8; i += 2, ++j)
            {
                block2b = ExtremeBits(blocks6b[i]);
                block4b = MiddleBits(blocks6b[i]);
                index = (byte)(block2b * 16 + block4b);
                blocks4b[j] = SubstitutionBoxes[i][index];

                block2b = ExtremeBits(blocks6b[i + 1]);
                block4b = MiddleBits(blocks6b[i + 1]);
                index = (byte)(block2b * 16 + block4b);
                blocks4b[j] = (byte)((blocks4b[j] << 4) | SubstitutionBoxes[i + 1][index]);
            }

            return blocks4b;
        }

        private byte ExtremeBits(byte block6b) =>
            (byte)(((block6b >> 6) & 0x2) | ((block6b >> 2) & 0x1));
        private byte MiddleBits(byte block6b) =>
            (byte)((block6b >> 3) & 0xF);
    }
}