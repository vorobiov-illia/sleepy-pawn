namespace SleepyPawn.Core.Chess.Rules
{
    internal static class LegalMoveAnalizer
    {
        public static bool IsLegal(Move move, GameState state)
        {
            return move.isValid;
        }
    }
}
