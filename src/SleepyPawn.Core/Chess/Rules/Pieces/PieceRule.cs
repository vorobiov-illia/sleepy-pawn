using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal abstract class PieceRule
    {
        protected Piece thisFigure = null!;
        protected Piece otherFigure = null!;

        internal abstract bool CanMove(Move move, GameState state);

        protected bool CommonCheck(Move move, GameState state, PieceType type)
        {
            thisFigure = state.GetPiece(move.firstPos);
            otherFigure = state.GetPiece(move.secondPos);

            if (thisFigure == null) return false;
            if (thisFigure.type != type) return false;

            if (otherFigure == null)
            {
                otherFigure = new Piece();
            }
            return true;
        }
    }
}
