using SleepyPawn.Core.Chess;

namespace SleepyPawn.Core.Utils
{
    internal static class BoardUtils
    {
        internal static readonly Piece[,] emptyPieces = new Piece[8, 8];
        internal static readonly EnginePosition IllegalPosition = new EnginePosition(-1, -1);
    }
}
