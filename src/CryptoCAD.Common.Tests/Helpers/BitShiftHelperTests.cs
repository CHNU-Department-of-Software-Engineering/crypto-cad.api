using System;
using System.Collections.Generic;
using System.Text;
using CryptoCAD.Common.Helpers;
using NUnit.Framework;

namespace CryptoCAD.Common.Tests.Helpers
{
    public class BitShiftHelperTests
    {
        [TestCase("1111000011001100", 1, "1110000110011001")]
        [TestCase("1111000011001100", 2, "1100001100110011")]
        [TestCase("1111000011001100", 4, "110011001111")]
        [TestCase("1111000011001100", 5, "1100110011110")]
        public void LeftShiftUshort(string input, int shift, string expected)
        {
            var value = Convert.ToUInt16(input, 2);
            var valueShifted = value.LeftShift((byte)shift);
            var actual = Convert.ToString(valueShifted, 2);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("11110000110011001111000011001100", 1, "11100001100110011110000110011001")]
        [TestCase("11110000110011001111000011001100", 2, "11000011001100111100001100110011")]
        [TestCase("11110000110011001111000011001100", 4, "1100110011110000110011001111")]
        [TestCase("11110000110011001111000011001100", 5, "11001100111100001100110011110")]
        public void LeftShiftUint(string input, int shift, string expected)
        {
            var value = Convert.ToUInt32(input, 2);
            var valueShifted = value.LeftShift((byte)shift);
            var actual = Convert.ToString(valueShifted, 2);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("1111000011001100111100001100110011110000110011001111000011001100", 1, "1110000110011001111000011001100111100001100110011110000110011001")]
        [TestCase("1111000011001100111100001100110011110000110011001111000011001100", 2, "1100001100110011110000110011001111000011001100111100001100110011")]
        [TestCase("1111000011001100111100001100110011110000110011001111000011001100", 4, "110011001111000011001100111100001100110011110000110011001111")]
        [TestCase("1111000011001100111100001100110011110000110011001111000011001100", 5, "1100110011110000110011001111000011001100111100001100110011110")]
        public void LeftShiftUlong(string input, int shift, string expected)
        {
            var value = Convert.ToUInt64(input, 2);
            var valueShifted = value.LeftShift((byte)shift);
            var actual = Convert.ToString((long)valueShifted, 2);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("11110000110011001111000011001100", 1,  "0001100110011110000110011000")]
        [TestCase("11110000110011001111000011001100", 2,  "0011001100111100001100110000")]
        [TestCase("11110000110011001111000011001100", 4,  "1100110011110000110011000000")]
        [TestCase("11110000110011001111000011001100", 5,  "1001100111100001100110000001")]
        [TestCase("11110000110011001111000011001100", 28, "0000110011001111000011001100")]
        public void LeftShift28(string input, int shift, string expected)
        {
            var value = Convert.ToUInt32(input, 2);
            var valueShifted = value.LeftShift28b((byte)shift);
            var actual = Convert.ToString(valueShifted, 2).PadLeft(28, '0');

            Assert.AreEqual(expected, actual);
        }
    }
}