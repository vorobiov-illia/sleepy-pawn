using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess.Rules
{
    internal static class LegalMoveAnalyzer
    {
        public static bool IsLegal(Move move, GameState state)
        {
            if(state.playerToMove == ColorUtils.Reverse(state.GetFigure(move.firstPos).color))
            {
                return false;
            }
            return true;
        }
    }
}
