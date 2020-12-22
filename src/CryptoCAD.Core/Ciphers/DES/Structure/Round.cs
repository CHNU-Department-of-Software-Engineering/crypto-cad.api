using System;
using System.Runtime.CompilerServices;
using CryptoCAD.Common.Helpers;
using CryptoCAD.Core.Ciphers.DES.Structure.Abstractions;

[assembly: InternalsVisibleTo("CryptoCAD.Core.Tests")]
namespace CryptoCAD.Core.Ciphers.DES.Structure
{
    internal class Round : IRound
    {
        private readonly IFunction Function;

        public Round(IFunction function)
        {
            Function = function;
        }

        public (uint, uint) Process(uint leftBlock32b, uint rightBlock32b, ulong key48b, RoundResults results)
        {
            results.RoundKey = BitConverter.GetBytes(key48b).Trim().ToHexadecimalString();
            results.InitialLeftBlock = BitConverter.GetBytes(leftBlock32b).ToHexadecimalString();
            results.InitialRightBlock = BitConverter.GetBytes(rightBlock32b).ToHexadecimalString();

            var newRightBlock32b = Function.Process(rightBlock32b, key48b);
            newRightBlock32b ^= leftBlock32b;

            results.ProcessedLeftBlock = BitConverter.GetBytes(rightBlock32b).ToHexadecimalString();
            results.ProcessedRightBlock = BitConverter.GetBytes(newRightBlock32b).ToHexadecimalString();

            return (rightBlock32b, newRightBlock32b);
        }
    }
}