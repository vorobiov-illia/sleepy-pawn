using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class RookRules : PieceRule
    {
        internal override bool CanMove(Move move, GameState state)
        {
            Piece thisPiece = state.GetPiece(move.firstPos);
            Piece otherPiece = state.GetPiece(move.secondPos);

            if (!CommonCheck(move, state, PieceType.Rook, thisPiece, otherPiece)) return false;

            bool xDis = move.firstPos.x != move.secondPos.x;
            bool yDis = move.firstPos.y != move.secondPos.y;

            if (xDis && yDis) return false;

            if (!PieceUtils.CheckSlide(move.firstPos, move.secondPos, state.boardState)) return false;

            if (!otherPiece.isEmpty &&
                otherPiece.color == thisPiece.color) return false;

            return true;
        }

        internal override List<Move> GeneratePseudoMoves(EnginePosition piecePosition, GameState state)
        {
            List<Move> moves = new List<Move>(14);

            Piece rook = state.GetPiece(piecePosition);
            if (rook.isEmpty) return moves;
            if (rook.type != PieceType.Rook) return moves;

            List<Move> rightMoves = PieceUtils.GetSlideMoves(piecePosition, state.boardState, 1, 0);
            List<Move> DownMoves = PieceUtils.GetSlideMoves(piecePosition, state.boardState, 0, -1);
            List<Move> leftMoves = PieceUtils.GetSlideMoves(piecePosition, state.boardState, -1, 0);
            List<Move> upMoves = PieceUtils.GetSlideMoves(piecePosition, state.boardState, 0, 1);

            moves.AddRange(rightMoves);
            moves.AddRange(DownMoves);
            moves.AddRange(leftMoves);
            moves.AddRange(upMoves);

            return moves;
        }

        internal override void GenerateThreat(EnginePosition piecePosition, Board board, ThreatBoard threats)
        {
            Piece thisPiece = board.GetPiece(piecePosition);

            if (thisPiece.isEmpty) return;
            if (thisPiece.type != PieceType.Rook) return;

            PieceUtils.SlideThreat(piecePosition, board, threats, 1, 0 ,thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, -1, 0, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, 0, 1, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, 0, -1, thisPiece.color);
        }
    }
}
