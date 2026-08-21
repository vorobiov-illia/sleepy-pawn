using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class KnightRules : PieceRule
    {
        internal override bool CanMove(Move move, GameState state)
        {
            if(!CommonCheck(move, state, PieceType.Knight)) return false;

            if (!otherFigure.isEmpty &&
                otherFigure.color == thisFigure.color) return false;

            return IsLMove(move);
        }

        private bool IsLMove(Move move)
        {
            if(Math.Abs(move.secondPos.x - move.firstPos.x) == 1)
            {
                if (Math.Abs(move.secondPos.y - move.firstPos.y) == 2) return true;
            }
            if (Math.Abs(move.secondPos.x - move.firstPos.x) == 2)
            {
                if (Math.Abs(move.secondPos.y - move.firstPos.y) == 1) return true;
            }
            return false;
        }
    }
}
