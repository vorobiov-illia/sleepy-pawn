using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal abstract class PieceRule
    {
        internal abstract bool CanMove(Move move, GameState state);
        internal abstract void GenerateThreat(EnginePosition piecePosition, Board board, ThreatBoard threats);
        internal abstract List<Move> GeneratePseudoMoves(EnginePosition piecePosition, GameState state);

        protected bool CommonCheck(Move move, GameState state, PieceType type, Piece thisPiece, Piece otherPiece)
        {
            if (thisPiece.type != type) return false;
            return true;
        }
    }
}
