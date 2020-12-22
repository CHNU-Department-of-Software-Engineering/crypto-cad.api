using NUnit.Framework;
using CryptoCAD.Common.Helpers;
using CryptoCAD.Core.Ciphers.DES;
using CryptoCAD.Core.Ciphers.Abstractions;

namespace CryptoCAD.Core.Tests.Ciphers.DES
{
    [TestFixture]
    public class DESCipherTests
    {
        private readonly ICipher Сipher;

        public DESCipherTests()
        {
            Сipher = new DESCipher();
        }

        [TestCase("myperkey", "Some generic text for testing!", true)]
        [TestCase("myperkey", "ASIFUNWQCOIAS CAKSMCpoawmd93j1m2r23f90mACSMCMPQWjr-0123m", true)]
        [TestCase("mypeey", "ASIFUNWQCOIAS CAKSMCpoawmd93j1m2r23f90mACSMCMPQWjr-0123m", false)]
        public void Encryption(string key, string data, bool isSuccessful)
        {
            var keyBytes = key.ToBytes();
            var dataBytes = data.ToBytes();

            try
            {
                var encypted = Сipher.Encrypt(keyBytes, dataBytes);
                var decrypted = Сipher.Decrypt(keyBytes, encypted.Data);
                var actual = decrypted.Data.Trim().ToString(ConvertMode.UTF8);

                Assert.AreEqual(data, actual);
            }
            catch
            {
                Assert.IsFalse(isSuccessful);
            }
        }
    }
}