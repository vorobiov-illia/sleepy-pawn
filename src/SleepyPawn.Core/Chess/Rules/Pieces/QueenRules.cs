using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class QueenRules : PieceRule
    {
        internal override bool CanMove(Move move, GameState state)
        {
            if (!CommonCheck(move, state, PieceType.Queen)) return false;

            bool xDis = move.firstPos.x != move.secondPos.x;
            bool yDis = move.firstPos.y != move.secondPos.y;

            int xDif = Math.Abs(move.secondPos.x - move.firstPos.x);
            int yDif = Math.Abs(move.secondPos.y - move.firstPos.y);

            if (xDis && yDis)
            {
                if (xDif != yDif) return false;
            }
            
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
            if (thisPiece.type != PieceType.Queen) return;

            PieceUtils.SlideThreat(piecePosition, board, threats, 1, 1, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, -1, 1, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, 1, -1, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, -1, -1, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, 1, 0, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, -1, 0, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, 0, 1, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, 0, -1, thisPiece.color);
        }
    }
}