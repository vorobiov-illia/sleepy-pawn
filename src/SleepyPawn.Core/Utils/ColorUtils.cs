using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Utils
{
    internal static class ColorUtils
    {
        internal static Color Reverse(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else if (color == Color.Black)
            {
                return Color.White;
            }
            return Color.None;
        }
    }
}
