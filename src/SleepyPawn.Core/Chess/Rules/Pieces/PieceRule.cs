using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal abstract class PieceRule
    {
        internal abstract bool CanMove(Move move, ChessState state);
        internal abstract void GenerateThreat(EnginePosition piecePosition, Board board, ThreatBoard threats);
        internal abstract int GeneratePseudoMoves(EnginePosition piecePosition, ChessState state, ref Span<Move> pseudoMoves, int count);

        protected bool CommonCheck(Move move, ChessState state, PieceType type, Piece thisPiece, Piece otherPiece)
        {
            if (thisPiece.type != type) return false;
            return true;
        }
    }
}
