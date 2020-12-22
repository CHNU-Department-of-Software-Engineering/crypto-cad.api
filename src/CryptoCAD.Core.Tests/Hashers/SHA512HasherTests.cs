using System;
using NUnit.Framework;
using CryptoCAD.Core.Hashers.SHA512;
using CryptoCAD.Core.Hashers.Abstractions;

namespace CryptoCAD.Core.Tests.Hashers
{
    [TestFixture]
    public class SHA512HasherTests
    {
        private readonly IHasher Hasher;

        public SHA512HasherTests()
        {
            Hasher = new SHA512Hasher();
        }

        [TestCase("Some generic text for testing!")]
        [TestCase("ASIFUNWQCOIAS CAKSMCpoawmd93j1m2r23f90mACSMCMPQWjr-0123m")]
        public void Hash(string data)
        {
            try
            {
                var actualHash1 = Hasher.Hash(data);
                var actualHash2 = Hasher.Hash(data);

                Assert.AreEqual(128, actualHash1.Length);
                Assert.AreEqual(128, actualHash2.Length);

                Assert.AreEqual(actualHash1, actualHash2);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            
        }
    }
}
