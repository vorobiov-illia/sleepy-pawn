namespace SleepyPawn.Core.Chess.Rules.Figures
{
    internal abstract class FigureRule
    {
        internal abstract bool CanMove(Move move, GameState state);
    }
}
