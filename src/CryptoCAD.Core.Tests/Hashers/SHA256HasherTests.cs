using NUnit.Framework;
using CryptoCAD.Core.Hashers.SHA256;
using CryptoCAD.Core.Hashers.Abstractions;

namespace CryptoCAD.Core.Tests.Hashers
{
    [TestFixture]
    public class SHA256HasherTests
    {
        private readonly IHasher Hasher;

        public SHA256HasherTests()
        {
            Hasher = new SHA256Hasher();
        }

        [TestCase("Some generic text for testing!")]
        [TestCase("ASIFUNWQCOIAS CAKSMCpoawmd93j1m2r23f90mACSMCMPQWjr-0123m")]
        public void Hash(string data)
        {
            var actualHash1 = Hasher.Hash(data);
            var actualHash2 = Hasher.Hash(data);

            Assert.AreEqual(64, actualHash1.Length);
            Assert.AreEqual(64, actualHash2.Length);

            Assert.AreEqual(actualHash1, actualHash2);
        }
    }
}