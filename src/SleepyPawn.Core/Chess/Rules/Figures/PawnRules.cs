using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess.Rules.Figures
{
    internal class PawnRules : FigureRule
    {
        internal override bool CanMove(Move move, GameState state)
        {
            Figure thisFigure = state.GetFigure(move.firstPos);
            Figure otherFigure = state.GetFigure(move.secondPos);

            if (thisFigure == null) return false;
            if (thisFigure.type != FigureType.Sleepy) return false;

            if(otherFigure == null)
            {
                otherFigure = new Figure();
            }

            if (otherFigure.isEmpty)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}
