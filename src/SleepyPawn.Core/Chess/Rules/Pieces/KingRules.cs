using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class KingRules : PieceRule
    {
        internal override bool CanMove(Move move, GameState state)
        {
            if (!CommonCheck(move, state, PieceType.King)) return false;

            if (Math.Abs(move.secondPos.x - move.firstPos.x) > 1) return false;
            if (Math.Abs(move.secondPos.y - move.firstPos.y) > 1) return false;

            if (otherPiece.color == thisPiece.color) return false;

            return true;
        }

        internal override void GenerateThreat(EnginePosition piecePosition, Board board, ThreatBoard threats)
        {
            base.GenerateThreat(piecePosition, board, threats);

            if (thisPiece == null) return;
            if (thisPiece.isEmpty) return;
            if (thisPiece.type != PieceType.King) return;

            threats.AddThreat(new EnginePosition(piecePosition.x - 1, piecePosition.y + 1), thisPiece.color);
            threats.AddThreat(new EnginePosition(piecePosition.x, piecePosition.y + 1), thisPiece.color);
            threats.AddThreat(new EnginePosition(piecePosition.x + 1, piecePosition.y + 1), thisPiece.color);

            threats.AddThreat(new EnginePosition(piecePosition.x - 1, piecePosition.y), thisPiece.color);
            threats.AddThreat(new EnginePosition(piecePosition.x + 1, piecePosition.y), thisPiece.color);

            threats.AddThreat(new EnginePosition(piecePosition.x - 1, piecePosition.y - 1), thisPiece.color);
            threats.AddThreat(new EnginePosition(piecePosition.x, piecePosition.y - 1), thisPiece.color);
            threats.AddThreat(new EnginePosition(piecePosition.x + 1, piecePosition.y - 1), thisPiece.color);
        }
    }
}
