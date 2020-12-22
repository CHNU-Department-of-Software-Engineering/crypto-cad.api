using CryptoCAD.Core.Ciphers.Abstractions;
using CryptoCAD.Core.Ciphers.DES.Structure;
using CryptoCAD.Core.Factories.Abstractions;
using CryptoCAD.Common.Configurations.Ciphers;
using System.Linq;

namespace CryptoCAD.Core.Ciphers.DES.Factory
{
    internal class DESCipherFactory : ICipherFactory
    {
        public ICipher CreateCipher(string configuration)
        {
            var desConfiguration = configuration.DESConfigurationFromJsonString();

            var results = new IntermediateResults
            {
                InitialPermutation = desConfiguration.InitialPermutation.Select(x => (int)x).ToArray(),
                FinalPermutation = desConfiguration.FinalPermutation.Select(x => (int)x).ToArray(),
                ExpansionPermutation = desConfiguration.ExpansionPermutation.Select(x => (int)x).ToArray(),
                Permutation = desConfiguration.Permutation.Select(x => (int)x).ToArray(),
                Pc1Permutation = desConfiguration.Pc1Permutation.Select(x => (int)x).ToArray(),
                Pc2Permutation = desConfiguration.Pc2Permutation.Select(x => (int)x).ToArray(),
                Rotations = desConfiguration.Rotations.Select(x => (int)x).ToArray(),
                SBoxes = desConfiguration.SBoxes.Select(x => x.Select(y => (int)y).ToArray()).ToArray(),

                KeySchedule = new KeyScheduleResults()
            };

            var keySchedule = new KeySchedule(
                desConfiguration.Pc1Permutation,
                desConfiguration.Pc2Permutation,
                desConfiguration.Rotations,
                results.KeySchedule);

            var function = new Function(
                desConfiguration.ExpansionPermutation,
                desConfiguration.Permutation,
                desConfiguration.SBoxes);

            var round = new Round(function);

            var cipher = new Cipher(
                keySchedule,
                round,
                desConfiguration.InitialPermutation,
                desConfiguration.FinalPermutation,
                results);

            return cipher;
        }
    }
}