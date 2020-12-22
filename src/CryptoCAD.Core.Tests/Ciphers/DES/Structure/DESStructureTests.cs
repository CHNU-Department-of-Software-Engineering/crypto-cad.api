using NUnit.Framework;
using CryptoCAD.Common.Helpers;
using CryptoCAD.Core.Ciphers.Abstractions;
using CryptoCAD.Core.Ciphers.DES.Structure;
using CryptoCAD.Common.Configurations.Ciphers;

namespace CryptoCAD.Core.Tests.Ciphers.DES.Structure
{
    [TestFixture]
    class DESStructureTests
    {
        [TestCase("myperkey", "Some generic text for testing!", true)]
        [TestCase("myperkey", "ASIFUNWQCOIAS CAKSMCpoawmd93j1m2r23f90mACSMCMPQWjr-0123m", true)]
        [TestCase("mypeey", "ASIFUNWQCOIAS CAKSMCpoawmd93j1m2r23f90mACSMCMPQWjr-0123m", false)]
        public void StructureBuild(string key, string data, bool isSuccessful)
        {
            var Cipher = CreateCipher();

            var keyBytes = key.ToBytes();
            var dataBytes = data.ToBytes();

            try
            {
                var encypted = Cipher.Encrypt(keyBytes, dataBytes);
                var decrypted = Cipher.Decrypt(keyBytes, encypted.Data);
                var actual = decrypted.Data.ToString(ConvertMode.UTF8).Trim('\0');

                Assert.AreEqual(data, actual);
            }
            catch
            {
                Assert.IsFalse(isSuccessful);
            }
        }

        private ICipher CreateCipher()
        {
            var results = new IntermediateResults
            {
                KeySchedule = new KeyScheduleResults()
            };

            var keySchedule = new KeySchedule(
                DESConfigurationExtension.PC1_PERMUTATION_TABLE,
                DESConfigurationExtension.PC2_PERMUTATION_TABLE,
                DESConfigurationExtension.ROTATIONS,
                results.KeySchedule);

            var function = new Function(
                DESConfigurationExtension.EXPANSION_PERMUTATION_TABLE,
                DESConfigurationExtension.PERMUTATION_TABLE,
                DESConfigurationExtension.SUBSTITUTION_BOXES);
            var round = new Round(function);

            var cipher = new Cipher(
                keySchedule,
                round,
                DESConfigurationExtension.INITIAL_PERMUTATION_TABLE,
                DESConfigurationExtension.FINAL_PERMUTATION_TABLE,
                results);

            return cipher;
        }
    }
}