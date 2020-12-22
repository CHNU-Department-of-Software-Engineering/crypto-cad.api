namespace CryptoCAD.Common.Configurations.Ciphers
{
    public class DESConfiguration
    {
        public byte[] InitialPermutation { get; set; }
        public byte[] FinalPermutation { get; set; }
        public byte[] ExpansionPermutation { get; set; }
        public byte[] Permutation { get; set; }
        public byte[][] SBoxes { get; set; }
        public byte[] Pc1Permutation { get; set; }
        public byte[] Pc2Permutation { get; set; }
        public byte[] Rotations { get; set; }
    }
}