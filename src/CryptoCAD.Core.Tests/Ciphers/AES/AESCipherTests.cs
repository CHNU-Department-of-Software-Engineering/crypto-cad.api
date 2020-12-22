using NUnit.Framework;
using CryptoCAD.Common.Helpers;
using CryptoCAD.Core.Ciphers.AES;
using CryptoCAD.Core.Ciphers.Abstractions;

namespace CryptoCAD.Core.Tests.Ciphers.AES
{
    [TestFixture]
    public class AESCipherTests
    {
        private readonly ICipher Сipher;

        public AESCipherTests()
        {
            Сipher = new AESCipher();
        }

        [TestCase("myperkeymyperkey", "Some generic text for testing!", true)]
        [TestCase("myperkeymyperkey", "ASIFUNWQCOIAS CAKSMCpoawmd93j1m2r23f90mACSMCMPQWjr-0123m", true)]
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