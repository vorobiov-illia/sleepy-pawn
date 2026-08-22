using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal abstract class PieceRule
    {
        protected Piece thisPiece = null!;
        protected Piece otherPiece = null!;

        internal abstract bool CanMove(Move move, GameState state);
        internal virtual void GenerateThreat(EnginePosition piecePosition, Board board, ThreatBoard threats)
        {
            thisPiece = board.GetPiece(piecePosition);
        }

        protected bool CommonCheck(Move move, GameState state, PieceType type)
        {
            thisPiece = state.GetPiece(move.firstPos);
            otherPiece = state.GetPiece(move.secondPos);

            if (thisPiece == null) return false;
            if (thisPiece.type != type) return false;

            if (otherPiece == null)
            {
                otherPiece = new Piece();
            }
            return true;
        }
    }
}
