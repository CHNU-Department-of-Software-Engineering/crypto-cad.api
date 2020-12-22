using NUnit.Framework;
using CryptoCAD.Common.Helpers;
using CryptoCAD.Core.Ciphers.GOST;
using CryptoCAD.Core.Ciphers.Abstractions;

namespace CryptoCAD.Core.Tests.Ciphers.GOST
{
    [TestFixture]
    public class GOSTCipherTests
    {
        private readonly ICipher Сipher;

        public GOSTCipherTests()
        {
            Сipher = new GOSTCipher();
        }

        [TestCase("myperkeymyperkeymyperkeymyperkey", "Some generic text for testing!", true)]
        [TestCase("myperkeymyperkeymyperkeymyperkey", "ASIFUNWQCOIAS CAKSMCpoawmd93j1m2r23f90mACSMCMPQWjr-0123m", true)]
        [TestCase("mypeey", "ASIFUNWQCOIAS CAKSMCpoawmd93j1m2r23f90mACSMCMPQWjr-0123m", false)]
        public void Encryption(string key, string data, bool isSuccessful)
        {
            var keyBytes = key.ToBytes();
            var dataBytes = data.ToBytes();

            try
            {
                var encypted = Сipher.Encrypt(keyBytes, dataBytes);
                var decypted = Сipher.Decrypt(keyBytes, encypted.Data);
                var actual = decypted.Data.ToString(ConvertMode.UTF8).Trim('\0');

                Assert.AreEqual(data, actual);
            }
            catch
            {
                Assert.IsFalse(isSuccessful);
            }
        }
    }
}