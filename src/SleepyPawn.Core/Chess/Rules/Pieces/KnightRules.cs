using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class KnightRules : PieceRule
    {
        internal override bool CanMove(Move move, GameState state)
        {
            if(!CommonCheck(move, state, PieceType.Knight)) return false;

            if (!otherPiece.isEmpty &&
                otherPiece.color == thisPiece.color) return false;

            return IsLMove(move);
        }

        internal override void GenerateThreat(EnginePosition piecePosition, Board board, ThreatBoard threats)
        {
            base.GenerateThreat(piecePosition, board, threats);

            if (thisPiece == null) return;
            if (thisPiece.isEmpty) return;
            if (thisPiece.type != PieceType.Knight) return;

            threats.AddThreat(new EnginePosition(piecePosition.x + 1, piecePosition.y + 2), thisPiece.color);
            threats.AddThreat(new EnginePosition(piecePosition.x - 1, piecePosition.y + 2), thisPiece.color);

            threats.AddThreat(new EnginePosition(piecePosition.x + 1, piecePosition.y - 2), thisPiece.color);
            threats.AddThreat(new EnginePosition(piecePosition.x - 1, piecePosition.y - 2), thisPiece.color);

            threats.AddThreat(new EnginePosition(piecePosition.x + 2, piecePosition.y + 1), thisPiece.color);
            threats.AddThreat(new EnginePosition(piecePosition.x + 2, piecePosition.y - 1), thisPiece.color);

            threats.AddThreat(new EnginePosition(piecePosition.x - 2, piecePosition.y + 1), thisPiece.color);
            threats.AddThreat(new EnginePosition(piecePosition.x - 2, piecePosition.y - 1), thisPiece.color);
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
