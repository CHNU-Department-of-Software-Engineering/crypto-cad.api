using NUnit.Framework;
using CryptoCAD.Common.Helpers;

namespace CryptoCAD.Common.Tests.Helpers
{
    public class CryptoHelperTests
    {
        [TestCase("U29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3Rpbmch", ConvertMode.ASCII)]
        [TestCase("U29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3Rpbmch", ConvertMode.BASE64)]
        [TestCase("U29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3Rpbmch", ConvertMode.UTF8)]
        public void ToString(string data, ConvertMode mode)
        {
            var bytes = data.ToBytes(mode);
            Assert.AreEqual(data, bytes.ToString(mode));
        }
        [TestCase("U29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3Rpbmch")]
        public void ToString_Default(string data)
        {
            var bytes = data.ToBytes();
            Assert.AreEqual(data, bytes.ToString(ConvertMode.UTF8));
        }

        [TestCase("U29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3Rpbmch")]
        [TestCase("U29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3RpbmchU29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3Rpbmch")]
        [TestCase("U29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3RpbmchU29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3RpbmchU29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3Rpbmch")]
        public void ToUInts64(string data)
        {
            var bytes = data.ToBytes();
            var blocks64b = bytes.ToUInts64();
            Assert.AreEqual(GetBlocksLength(bytes, 8), blocks64b.Length);

            var newBytes = blocks64b.ToBytes();
            Assert.AreEqual(data, newBytes.ToString(ConvertMode.UTF8));
        }

        [TestCase("U29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3Rpbmch")]
        [TestCase("U29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3RpbmchU29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3Rpbmch")]
        [TestCase("U29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3RpbmchU29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3RpbmchU29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3Rpbmch")]
        public void ToUInts32(string data)
        {
            var bytes = data.ToBytes();
            var blocks32b = bytes.ToUInts32();
            Assert.AreEqual(GetBlocksLength(bytes, 4), blocks32b.Length);

            var newBytes = blocks32b.ToBytes();
            Assert.AreEqual(data, newBytes.ToString(ConvertMode.UTF8));
        }

        [TestCase("U29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3Rpbmch")]
        [TestCase("U29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3RpbmchU29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3Rpbmch")]
        [TestCase("U29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3RpbmchU29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3RpbmchU29tZSBnZW5lcmljIHRleHQgZm9yIHRlc3Rpbmch")]
        public void ToUInts16(string data)
        {
            var bytes = data.ToBytes();
            var blocks16b = bytes.ToUInts16();
            Assert.AreEqual(GetBlocksLength(bytes, 2), blocks16b.Length);

            var newBytes = blocks16b.ToBytes();
            Assert.AreEqual(data, newBytes.ToString(ConvertMode.UTF8));
        }

        private uint GetBlocksLength(byte[] bytes, byte blockLength)
        {
            uint length = (uint)(bytes.Length / blockLength);
            length += (bytes.Length - length * blockLength == 0 ? 0 : 1);
            return length;
        }
    }
}