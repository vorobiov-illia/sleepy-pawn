using SleepyPawn.Core.Chess;
using System.Runtime.CompilerServices;

namespace SleepyPawn.Core.Utils
{
    internal static class BoardUtils
    {
        internal static readonly Piece[] emptyPieces = new Piece[64];
        internal static readonly EnginePosition IllegalPosition = new EnginePosition(-1, -1);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int EnginePositionToIndex(EnginePosition position)
        {
            return (position.y * 8) + position.x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int EnginePositionToIndex(int x, int y)
        {
            return (y * 8) + x;
        }
    }
}
