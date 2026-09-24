namespace SleepyPawn.Core.Utils
{
    internal static class XorShiftUtils
    {
        private static ulong state = 1812433253UL;

        internal static ulong Next()
        {
            ulong result = state;
            result ^= result << 13;
            result ^= result >> 7;
            result ^= result << 17;
            state = result;
            return result;
        }
    }
}
