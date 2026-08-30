using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class BishopRules : PieceRule
    {
        internal override bool CanMove(Move move, GameState state)
        {
            Piece thisPiece = state.GetPiece(move.firstPos);
            Piece otherPiece = state.GetPiece(move.secondPos);

            if (!CommonCheck(move, state, PieceType.Bishop, thisPiece, otherPiece)) return false;

            int xDif = Math.Abs(move.secondPos.x - move.firstPos.x);
            int yDif = Math.Abs(move.secondPos.y - move.firstPos.y);

            if (xDif != yDif) return false;

            if (!PieceUtils.CheckSlide(move.firstPos, move.secondPos, state.boardState)) return false;

            if (!otherPiece.isEmpty &&
                otherPiece.color == thisPiece.color) return false;

            return true;
        }

        internal override void GenerateThreat(EnginePosition piecePosition, Board board, ThreatBoard threats)
        {
            Piece thisPiece = board.GetPiece(piecePosition);

            if (thisPiece == null) return;
            if (thisPiece.isEmpty) return;
            if (thisPiece.type != PieceType.Bishop) return;

            PieceUtils.SlideThreat(piecePosition, board, threats, 1, 1, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, -1, 1, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, 1, -1, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, -1, -1, thisPiece.color);
        }
    }
}
