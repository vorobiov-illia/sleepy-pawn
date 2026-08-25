using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class RookRules : PieceRule
    {
        internal override bool CanMove(Move move, GameState state)
        {
            if (!CommonCheck(move, state, PieceType.Rook)) return false;

            bool xDis = move.firstPos.x != move.secondPos.x;
            bool yDis = move.firstPos.y != move.secondPos.y;

            if (xDis && yDis) return false;

            if (!PieceUtils.CheckSlide(move.firstPos, move.secondPos, state.boardState)) return false;

            if (!otherPiece.isEmpty &&
                otherPiece.color == thisPiece.color) return false;

            return true;
        }
        internal override void GenerateThreat(EnginePosition piecePosition, Board board, ThreatBoard threats)
        {
            base.GenerateThreat(piecePosition, board, threats);

            if (thisPiece == null) return;
            if (thisPiece.isEmpty) return;
            if (thisPiece.type != PieceType.Rook) return;

            PieceUtils.SlideThreat(piecePosition, board, threats, 1, 0 ,thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, -1, 0, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, 0, 1, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, 0, -1, thisPiece.color);
        }
    }
}
