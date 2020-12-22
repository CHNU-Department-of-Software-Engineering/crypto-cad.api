namespace CryptoCAD.Common.Helpers
{
    public static class BitShiftHelper
    {
        public static uint LeftShift28b(this uint value, byte shift) =>
            ((value << shift) | ((value & ~unchecked((uint)(15 << 28))) >> 28 - shift) ) & ~unchecked((uint)(15 << 28));

        public static ushort LeftShift(this ushort value, byte shift) =>
            (ushort)((value << shift) | (value >> 16 - shift));

        public static uint LeftShift(this uint value, byte shift) =>
            (value << shift) | (value >> 32 - shift);

        public static ulong LeftShift(this ulong value, byte shift) =>
            (value << shift) | (value >> 64 - shift);
    }
}