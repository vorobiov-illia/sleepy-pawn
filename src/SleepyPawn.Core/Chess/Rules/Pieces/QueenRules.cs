using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class QueenRules : PieceRule
    {
        internal override bool CanMove(Move move, GameState state)
        {
            Piece thisPiece = state.GetPiece(move.firstPos);
            Piece otherPiece = state.GetPiece(move.secondPos);

            if (!CommonCheck(move, state, PieceType.Queen, thisPiece, otherPiece)) return false;

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

        internal override List<Move> GeneratePseudoMoves(EnginePosition piecePosition, GameState state)
        {
            List<Move> moves = new List<Move>(27);

            Piece queen = state.GetPiece(piecePosition);
            if (queen.isEmpty) return moves;
            if (queen.type != PieceType.Queen) return moves;

            List<Move> rightUpMoves = PieceUtils.GetSlideMoves(piecePosition, state.boardState, 1, 1);
            List<Move> rightDownMoves = PieceUtils.GetSlideMoves(piecePosition, state.boardState, 1, -1);
            List<Move> leftDownMoves = PieceUtils.GetSlideMoves(piecePosition, state.boardState, -1, -1);
            List<Move> leftUpMoves = PieceUtils.GetSlideMoves(piecePosition, state.boardState, -1, 1);

            moves.AddRange(rightUpMoves);
            moves.AddRange(rightDownMoves);
            moves.AddRange(leftDownMoves);
            moves.AddRange(leftUpMoves);

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