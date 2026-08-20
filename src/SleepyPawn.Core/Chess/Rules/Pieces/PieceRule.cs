namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal abstract class PieceRule
    {
        internal abstract bool CanMove(Move move, GameState state);
    }
}
