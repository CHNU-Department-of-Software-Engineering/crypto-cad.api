using System.Linq;
using Newtonsoft.Json;

namespace CryptoCAD.Common.Configurations.Ciphers
{
    public static class DESConfigurationExtension
    {
        public const byte INITIAL_PERMUTATION_TABLE_LENGTH = 64;
        public static readonly byte[] INITIAL_PERMUTATION_TABLE = new byte[INITIAL_PERMUTATION_TABLE_LENGTH] {
            58, 50, 42, 34, 26, 18, 10, 2,
            60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6,
            64, 56, 48, 40, 32, 24, 16, 8,
            57, 49, 41, 33, 25, 17, 9,  1,
            59, 51, 43, 35, 27, 19, 11, 3,
            61, 53, 45, 37, 29, 21, 13, 5,
            63, 55, 47, 39, 31, 23, 15, 7
        };

        public const byte FINAL_PERMUTATION_TABLE_LENGTH = 64;
        public static readonly byte[] FINAL_PERMUTATION_TABLE = new byte[FINAL_PERMUTATION_TABLE_LENGTH] {
            40, 8, 48, 16, 56, 24, 64, 32,
            39, 7, 47, 15, 55, 23, 63, 31,
            38, 6, 46, 14, 54, 22, 62, 30,
            37, 5, 45, 13, 53, 21, 61, 29,
            36, 4, 44, 12, 52, 20, 60, 28,
            35, 3, 43, 11, 51, 19, 59, 27,
            34, 2, 42, 10, 50, 18, 58, 26,
            33, 1, 41, 9, 49, 17, 57, 25
        };

        public const byte EXPANSION_PERMUTATION_TABLE_LENGTH = 48;
        public static readonly byte[] EXPANSION_PERMUTATION_TABLE = new byte[EXPANSION_PERMUTATION_TABLE_LENGTH] {
            32, 1,  2,  3,  4,  5,
            4,  5,  6,  7,  8,  9,
            8,  9,  10, 11, 12, 13,
            12, 13, 14, 15, 16, 17,
            16, 17, 18, 19, 20, 21,
            20, 21, 22, 23, 24, 25,
            24, 25, 26, 27, 28, 29,
            28, 29, 30, 31, 32, 1
        };

        public const byte SUBSTITUTION_BOXES_LENGTH = 8;
        public const byte SUBSTITUTION_BOX_LENGTH = 64;
        public static readonly byte[][] SUBSTITUTION_BOXES = new byte[SUBSTITUTION_BOXES_LENGTH][] {
            new byte[SUBSTITUTION_BOX_LENGTH] {
                14, 4,  13, 1,  2,  15, 11, 8,  3,  10, 6,  12, 5,  9,  0,  7,
                0,  15, 7,  4,  14, 2,  13, 1,  10, 6,  12, 11, 9,  5,  3,  8,
                4,  1,  14, 8,  13, 6,  2,  11, 15, 12, 9,  7,  3,  10, 5,  0,
                15, 12, 8,  2,  4,  9,  1,  7,  5,  11, 3,  14, 10, 0,  6,  13
            },
            new byte[SUBSTITUTION_BOX_LENGTH] {
                15, 1,  8,  14, 6,  11, 3,  4,  9,  7,  2,  13, 12, 0,  5,  10,
                3,  13, 4,  7,  15, 2,  8,  14, 12, 0,  1,  10, 6,  9,  11, 5,
                0,  14, 7,  11, 10, 4,  13, 1,  5,  8,  12, 6,  9,  3,  2,  15,
                13, 8,  10, 1,  3,  15, 4,  2,  11, 6,  7,  12, 0,  5,  14, 9
            },
            new byte[SUBSTITUTION_BOX_LENGTH] {
                10, 0,  9,  14, 6,  3,  15, 5,  1,  13, 12, 7,  11, 4,  2,  8,
                13, 7,  0,  9,  3,  4,  6,  10, 2,  8,  5,  14, 12, 11, 15, 1,
                13, 6,  4,  9,  8,  15, 3,  0,  11, 1,  2,  12, 5,  10, 14, 7,
                1,  10, 13, 0,  6,  9,  8,  7,  4,  15, 14, 3,  11, 5,  2,  12
            },
            new byte[SUBSTITUTION_BOX_LENGTH] {
                7,  13, 14, 3,  0,  6,  9,  10, 1,  2,  8,  5,  11, 12, 4,  15,
                13, 8,  11, 5,  6,  15, 0,  3,  4,  7,  2,  12, 1,  10, 14, 9,
                10, 6,  9,  0,  12, 11, 7,  13, 15, 1,  3,  14, 5,  2,  8,  4,
                3,  15, 0,  6,  10, 1,  13, 8,  9,  4,  5,  11, 12, 7,  2,  14
            },
            new byte[SUBSTITUTION_BOX_LENGTH] {
                2,  12, 4,  1,  7,  10, 11, 6,  8,  5,  3,  15, 13, 0,  14, 9,
                14, 11, 2,  12, 4,  7,  13, 1,  5,  0,  15, 10, 3,  9,  8,  6,
                4,  2,  1,  11, 10, 13, 7,  8,  15, 9,  12, 5,  6,  3,  0,  14,
                11, 8,  12, 7,  1,  14, 2,  13, 6,  15, 0,  9,  10, 4,  5,  3
            },
            new byte[SUBSTITUTION_BOX_LENGTH] {
                12, 1,  10, 15, 9,  2,  6,  8,  0,  13, 3,  4,  14, 7,  5,  11,
                10, 15, 4,  2,  7,  12, 9,  5,  6,  1,  13, 14, 0,  11, 3,  8,
                9,  14, 15, 5,  2,  8,  12, 3,  7,  0,  4,  10, 1,  13, 11, 6,
                4,  3,  2,  12, 9,  5,  15, 10, 11, 14, 1,  7,  6,  0,  8,  13
            },
            new byte[SUBSTITUTION_BOX_LENGTH] {
                4,  11, 2,  14, 15, 0,  8,  13, 3,  12, 9,  7,  5,  10, 6,  1,
                13, 0,  11, 7,  4,  9,  1,  10, 14, 3,  5,  12, 2,  15, 8,  6,
                1,  4,  11, 13, 12, 3,  7,  14, 10, 15, 6,  8,  0,  5,  9,  2,
                6,  11, 13, 8,  1,  4,  10, 7,  9,  5,  0,  15, 14, 2,  3,  12
            },
            new byte[SUBSTITUTION_BOX_LENGTH] {
                13, 2,  8,  4,  6,  15, 11, 1,  10, 9,  3,  14, 5,  0,  12, 7,
                1,  15, 13, 8,  10, 3,  7,  4,  12, 5,  6,  11, 0,  14, 9,  2,
                7,  11, 4,  1,  9,  12, 14, 2,  0,  6,  10, 13, 15, 3,  5,  8,
                2,  1,  14, 7,  4,  10, 8,  13, 15, 12, 9,  0,  3,  5,  6,  11
            }
        };

        public const byte PERMUTATION_TABLE_LENGTH = 32;
        public static readonly byte[] PERMUTATION_TABLE = new byte[PERMUTATION_TABLE_LENGTH] {
            16, 7,  20, 21, 29, 12, 28, 17,
            1,  15, 23, 26, 5,  18, 31, 10,
            2,  8,  24, 14, 32, 27, 3,  9,
            19, 13, 30, 6, 22, 11, 4,  25
        };

        public const byte PC1_PERMUTATION_TABLE_LENGTH = 56;
        public static readonly byte[] PC1_PERMUTATION_TABLE = new byte[PC1_PERMUTATION_TABLE_LENGTH] {
            57, 49, 41, 33, 25, 17, 9, 1,  58, 50, 42, 34, 26, 18,
            10, 2,  59, 51, 43, 35, 27, 19, 11, 3,  60, 52, 44, 36,
            63, 55, 47, 39, 31, 23, 15, 7,  62, 54, 46, 38, 30, 22,
            14, 6,  61, 53, 45, 37, 29, 21, 13, 5,  28, 20, 12, 4
        };

        public const byte PC2_PERMUTATION_TABLE_LENGTH = 48;
        public static readonly byte[] PC2_PERMUTATION_TABLE = new byte[PC2_PERMUTATION_TABLE_LENGTH] {
            14, 17, 11, 24, 1,  5, 3,  28, 15, 6,  21, 10,
            23, 19, 12, 4,  26, 8, 16, 7,  27, 20, 13, 2,
            41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48,
            44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32
        };

        public const byte ROTATIONS_TABLE_LENGTH = 16;
        public static readonly byte[] ROTATIONS = new byte[ROTATIONS_TABLE_LENGTH] {
            1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1
        };

        public static DESConfiguration GetConfiguration()
        {
            return new DESConfiguration
            {
                InitialPermutation = INITIAL_PERMUTATION_TABLE,
                FinalPermutation = FINAL_PERMUTATION_TABLE,
                ExpansionPermutation = EXPANSION_PERMUTATION_TABLE,
                Permutation = PERMUTATION_TABLE,
                SBoxes = SUBSTITUTION_BOXES,
                Pc1Permutation = PC1_PERMUTATION_TABLE,
                Pc2Permutation = PC2_PERMUTATION_TABLE,
                Rotations = ROTATIONS
            };
        }
        public static string ToJsonString(this DESConfiguration configuration)
        {
            return JsonConvert.SerializeObject(new DESConfigurationDTO
            {
                InitialPermutation = configuration.InitialPermutation?.Select(x => (int)x).ToArray(),
                FinalPermutation = configuration.FinalPermutation?.Select(x => (int)x).ToArray(),
                ExpansionPermutation = configuration.ExpansionPermutation?.Select(x => (int)x).ToArray(),
                Permutation = configuration.Permutation?.Select(x => (int)x).ToArray(),
                Pc1Permutation = configuration.Pc1Permutation?.Select(x => (int)x).ToArray(),
                Pc2Permutation = configuration.Pc2Permutation?.Select(x => (int)x).ToArray(),
                Rotations = configuration.Rotations?.Select(x => (int)x).ToArray(),
                SBox1 = configuration.SBoxes?[0]?.Select(x => (int)x).ToArray(),
                SBox2 = configuration.SBoxes?[1]?.Select(x => (int)x).ToArray(),
                SBox3 = configuration.SBoxes?[2]?.Select(x => (int)x).ToArray(),
                SBox4 = configuration.SBoxes?[3]?.Select(x => (int)x).ToArray(),
                SBox5 = configuration.SBoxes?[4]?.Select(x => (int)x).ToArray(),
                SBox6 = configuration.SBoxes?[5]?.Select(x => (int)x).ToArray(),
                SBox7 = configuration.SBoxes?[6]?.Select(x => (int)x).ToArray(),
                SBox8 = configuration.SBoxes?[7]?.Select(x => (int)x).ToArray()
            });
        }
        public static DESConfiguration DESConfigurationFromJsonString(this string configuration)
        {
            var configurationDTO = JsonConvert.DeserializeObject<DESConfigurationDTO>(configuration);

            if (configurationDTO is null)
            {
                return GetConfiguration();
            }

            var desConfiguration = new DESConfiguration();

            if (configurationDTO.InitialPermutation is null)
            {
                desConfiguration.InitialPermutation = INITIAL_PERMUTATION_TABLE;
            }
            else
            {
                desConfiguration.InitialPermutation = configurationDTO.InitialPermutation.Select(x => (byte)x).ToArray();
            }

            if (configurationDTO.FinalPermutation is null)
            {
                desConfiguration.FinalPermutation = FINAL_PERMUTATION_TABLE;
            }
            else
            {
                desConfiguration.FinalPermutation = configurationDTO.FinalPermutation.Select(x => (byte)x).ToArray();
            }

            if (configurationDTO.ExpansionPermutation is null)
            {
                desConfiguration.ExpansionPermutation = EXPANSION_PERMUTATION_TABLE;
            }
            else
            {
                desConfiguration.ExpansionPermutation = configurationDTO.ExpansionPermutation.Select(x => (byte)x).ToArray();
            }

            if (configurationDTO.Permutation is null)
            {
                desConfiguration.Permutation = PERMUTATION_TABLE;
            }
            else
            {
                desConfiguration.Permutation = configurationDTO.Permutation.Select(x => (byte)x).ToArray();
            }

            if (configurationDTO.Pc1Permutation is null)
            {
                desConfiguration.Pc1Permutation = PC1_PERMUTATION_TABLE;
            }
            else
            {
                desConfiguration.Pc1Permutation = configurationDTO.Pc1Permutation.Select(x => (byte)x).ToArray();
            }

            if (configurationDTO.Pc2Permutation is null)
            {
                desConfiguration.Pc2Permutation = PC2_PERMUTATION_TABLE;
            }
            else
            {
                desConfiguration.Pc2Permutation = configurationDTO.Pc2Permutation.Select(x => (byte)x).ToArray();
            }

            if (configurationDTO.Rotations is null)
            {
                desConfiguration.Rotations = ROTATIONS;
            }
            else
            {
                desConfiguration.Rotations = configurationDTO.Rotations.Select(x => (byte)x).ToArray();
            }

            desConfiguration.SBoxes = new byte[SUBSTITUTION_BOXES_LENGTH][];

            if (configurationDTO.SBox1 is null)
            {
                desConfiguration.SBoxes[0] = SUBSTITUTION_BOXES[0];
            }
            else
            {
                desConfiguration.SBoxes[0] = configurationDTO.SBox1.Select(x => (byte)x).ToArray();
            }
            if (configurationDTO.SBox2 is null)
            {
                desConfiguration.SBoxes[1] = SUBSTITUTION_BOXES[1];
            }
            else
            {
                desConfiguration.SBoxes[1] = configurationDTO.SBox2.Select(x => (byte)x).ToArray();
            }
            if (configurationDTO.SBox3 is null)
            {
                desConfiguration.SBoxes[2] = SUBSTITUTION_BOXES[2];
            }
            else
            {
                desConfiguration.SBoxes[2] = configurationDTO.SBox3.Select(x => (byte)x).ToArray();
            }
            if (configurationDTO.SBox4 is null)
            {
                desConfiguration.SBoxes[3] = SUBSTITUTION_BOXES[3];
            }
            else
            {
                desConfiguration.SBoxes[3] = configurationDTO.SBox4.Select(x => (byte)x).ToArray();
            }
            if (configurationDTO.SBox5 is null)
            {
                desConfiguration.SBoxes[4] = SUBSTITUTION_BOXES[4];
            }
            else
            {
                desConfiguration.SBoxes[4] = configurationDTO.SBox5.Select(x => (byte)x).ToArray();
            }
            if (configurationDTO.SBox6 is null)
            {
                desConfiguration.SBoxes[5] = SUBSTITUTION_BOXES[5];
            }
            else
            {
                desConfiguration.SBoxes[5] = configurationDTO.SBox6.Select(x => (byte)x).ToArray();
            }
            if (configurationDTO.SBox7 is null)
            {
                desConfiguration.SBoxes[6] = SUBSTITUTION_BOXES[6];
            }
            else
            {
                desConfiguration.SBoxes[6] = configurationDTO.SBox7.Select(x => (byte)x).ToArray();
            }
            if (configurationDTO.SBox8 is null)
            {
                desConfiguration.SBoxes[7] = SUBSTITUTION_BOXES[7];
            }
            else
            {
                desConfiguration.SBoxes[7] = configurationDTO.SBox8.Select(x => (byte)x).ToArray();
            }

            return desConfiguration;
        }

        public static DESConfigurationDTO ToDTO(this DESConfiguration configuration)
        {
            return new DESConfigurationDTO
            {
                InitialPermutation = configuration.InitialPermutation?.Select(x => (int)x).ToArray(),
                FinalPermutation = configuration.FinalPermutation?.Select(x => (int)x).ToArray(),
                ExpansionPermutation = configuration.ExpansionPermutation?.Select(x => (int)x).ToArray(),
                Permutation = configuration.Permutation?.Select(x => (int)x).ToArray(),
                Pc1Permutation = configuration.Pc1Permutation?.Select(x => (int)x).ToArray(),
                Pc2Permutation = configuration.Pc2Permutation?.Select(x => (int)x).ToArray(),
                Rotations = configuration.Rotations?.Select(x => (int)x).ToArray(),
                SBox1 = configuration.SBoxes?[0]?.Select(x => (int)x).ToArray(),
                SBox2 = configuration.SBoxes?[1]?.Select(x => (int)x).ToArray(),
                SBox3 = configuration.SBoxes?[2]?.Select(x => (int)x).ToArray(),
                SBox4 = configuration.SBoxes?[3]?.Select(x => (int)x).ToArray(),
                SBox5 = configuration.SBoxes?[4]?.Select(x => (int)x).ToArray(),
                SBox6 = configuration.SBoxes?[5]?.Select(x => (int)x).ToArray(),
                SBox7 = configuration.SBoxes?[6]?.Select(x => (int)x).ToArray(),
                SBox8 = configuration.SBoxes?[7]?.Select(x => (int)x).ToArray()
            };
        }

        public class DESConfigurationDTO
        {
            public int[] InitialPermutation { get; set; }
            public int[] FinalPermutation { get; set; }
            public int[] ExpansionPermutation { get; set; }
            public int[] Permutation { get; set; }
            public int[] Pc1Permutation { get; set; }
            public int[] Pc2Permutation { get; set; }
            public int[] Rotations { get; set; }

            public int[] SBox1 { get; set; }
            public int[] SBox2 { get; set; }
            public int[] SBox3 { get; set; }
            public int[] SBox4 { get; set; }
            public int[] SBox5 { get; set; }
            public int[] SBox6 { get; set; }
            public int[] SBox7 { get; set; }
            public int[] SBox8 { get; set; }
        }
    }
}