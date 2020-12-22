using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCAD.Core.Ciphers.DES.Structure
{
    internal class IntermediateResults
    {
        public int[] InitialPermutation { get; set; }
        public int[] FinalPermutation { get; set; }
        public int[] ExpansionPermutation { get; set; }
        public int[] Permutation { get; set; }
        public int[][] SBoxes { get; set; }
        public int[] Pc1Permutation { get; set; }
        public int[] Pc2Permutation { get; set; }
        public int[] Rotations { get; set; }

        public BlockResults[] Blocks { get; set; }
        public KeyScheduleResults KeySchedule { get; set; }
    }

    internal class BlockResults
    {
        public string InitialBlock { get; set; }
        public string AfterFirstPermutation { get; set; }
        public RoundResults[] Rounds { get; set; }
        public string FinalBlock { get; set; }
        public string AfterSecondPermutation { get; set; }
    }

    internal class RoundResults
    {
        public string RoundKey { get; set; }
        public string InitialLeftBlock { get; set; }
        public string InitialRightBlock { get; set; }
        public FunctionResults FunctionResults { get; set; }
        public string ProcessedLeftBlock { get; set; }
        public string ProcessedRightBlock { get; set; }
    }
    internal class KeyScheduleResults
    {
        public string InitialKey { get; set; }
        public string InitialLeftKey { get; set; }
        public string InitialRightKey { get; set; }
        public SubkeysResults[] Subkeys { get; set; }
    }
    internal class SubkeysResults
    {
        public string KeyPartLeft { get; set; }
        public string KeyPartRight { get; set; }
        public string Subkey { get; set; }
    }
    internal class FunctionResults
    {

    }
}