using NUnit.Framework;
using CryptoCAD.Common.Configurations.Ciphers;

namespace CryptoCAD.Common.Tests.Configurations.Ciphers
{
    public class DESConfigurationExtensionTests
    {
        [Test]
        public void GetConfiguration()
        {
            var configuration = DESConfigurationExtension.GetConfiguration();

            Assert.AreEqual(
                DESConfigurationExtension.INITIAL_PERMUTATION_TABLE,
                configuration.InitialPermutation);

            Assert.AreEqual(
                DESConfigurationExtension.FINAL_PERMUTATION_TABLE,
                configuration.FinalPermutation);

            Assert.AreEqual(
                DESConfigurationExtension.EXPANSION_PERMUTATION_TABLE,
                configuration.ExpansionPermutation);

            Assert.AreEqual(
                DESConfigurationExtension.PERMUTATION_TABLE,
                configuration.Permutation);

            Assert.AreEqual(
                DESConfigurationExtension.SUBSTITUTION_BOXES,
                configuration.SBoxes);

            Assert.AreEqual(
                DESConfigurationExtension.PC1_PERMUTATION_TABLE,
                configuration.Pc1Permutation);

            Assert.AreEqual(
                DESConfigurationExtension.PC2_PERMUTATION_TABLE,
                configuration.Pc2Permutation);

            Assert.AreEqual(
                DESConfigurationExtension.ROTATIONS,
                configuration.Rotations);
        }

        [Test]
        public void ToFromJson()
        {
            var configuration = DESConfigurationExtension.GetConfiguration();

            var json = configuration.ToJsonString();
            var desConfiguration = json.DESConfigurationFromJsonString();

            Assert.AreEqual(
                DESConfigurationExtension.INITIAL_PERMUTATION_TABLE,
                desConfiguration.InitialPermutation);

            Assert.AreEqual(
                DESConfigurationExtension.FINAL_PERMUTATION_TABLE,
                desConfiguration.FinalPermutation);

            Assert.AreEqual(
                DESConfigurationExtension.EXPANSION_PERMUTATION_TABLE,
                desConfiguration.ExpansionPermutation);

            Assert.AreEqual(
                DESConfigurationExtension.PERMUTATION_TABLE,
                desConfiguration.Permutation);

            Assert.AreEqual(
                DESConfigurationExtension.SUBSTITUTION_BOXES,
                desConfiguration.SBoxes);

            Assert.AreEqual(
                DESConfigurationExtension.PC1_PERMUTATION_TABLE,
                desConfiguration.Pc1Permutation);

            Assert.AreEqual(
                DESConfigurationExtension.PC2_PERMUTATION_TABLE,
                desConfiguration.Pc2Permutation);

            Assert.AreEqual(
                DESConfigurationExtension.ROTATIONS,
                desConfiguration.Rotations);
        }

        [Test]
        public void FromJsonDefault()
        {
            var configuration = string.Empty.DESConfigurationFromJsonString();

            Assert.AreEqual(
                DESConfigurationExtension.INITIAL_PERMUTATION_TABLE,
                configuration.InitialPermutation);

            Assert.AreEqual(
                DESConfigurationExtension.FINAL_PERMUTATION_TABLE,
                configuration.FinalPermutation);

            Assert.AreEqual(
                DESConfigurationExtension.EXPANSION_PERMUTATION_TABLE,
                configuration.ExpansionPermutation);

            Assert.AreEqual(
                DESConfigurationExtension.PERMUTATION_TABLE,
                configuration.Permutation);

            Assert.AreEqual(
                DESConfigurationExtension.SUBSTITUTION_BOXES,
                configuration.SBoxes);

            Assert.AreEqual(
                DESConfigurationExtension.PC1_PERMUTATION_TABLE,
                configuration.Pc1Permutation);

            Assert.AreEqual(
                DESConfigurationExtension.PC2_PERMUTATION_TABLE,
                configuration.Pc2Permutation);

            Assert.AreEqual(
                DESConfigurationExtension.ROTATIONS,
                configuration.Rotations);
        }

        [Test]
        public void FromJsonDefaultTables()
        {
            var table = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var configuration = new DESConfiguration
            {
                InitialPermutation = table,
                SBoxes = new byte[8][]
            };
            configuration.SBoxes[2] = table;

            var json = configuration.ToJsonString();
            var desConfiguration = json.DESConfigurationFromJsonString();

            Assert.AreEqual(
                table,
                desConfiguration.InitialPermutation);

            Assert.AreEqual(
                DESConfigurationExtension.FINAL_PERMUTATION_TABLE,
                desConfiguration.FinalPermutation);

            Assert.AreEqual(
                DESConfigurationExtension.EXPANSION_PERMUTATION_TABLE,
                desConfiguration.ExpansionPermutation);

            Assert.AreEqual(
                DESConfigurationExtension.PERMUTATION_TABLE,
                desConfiguration.Permutation);

            var sboxes = DESConfigurationExtension.SUBSTITUTION_BOXES;
            sboxes[2] = table;

            Assert.AreEqual(
                sboxes,
                desConfiguration.SBoxes);

            Assert.AreEqual(
                DESConfigurationExtension.PC1_PERMUTATION_TABLE,
                desConfiguration.Pc1Permutation);

            Assert.AreEqual(
                DESConfigurationExtension.PC2_PERMUTATION_TABLE,
                desConfiguration.Pc2Permutation);

            Assert.AreEqual(
                DESConfigurationExtension.ROTATIONS,
                desConfiguration.Rotations);
        }
    }
}