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

        internal override List<Move> GeneratePseudoMoves(EnginePosition piecePosition, GameState state)
        {
            List<Move> moves = new List<Move>();

            Piece bishop = state.GetPiece(piecePosition);
            if (bishop == null) return moves;
            if (bishop.isEmpty) return moves;
            if (bishop.type != PieceType.Bishop) return moves;

            List<Move> rightUpMoves = PieceUtils.GetSlideMoves(piecePosition, state.boardState, 1, 1);
            List<Move> rightDownMoves = PieceUtils.GetSlideMoves(piecePosition, state.boardState, 1, -1);
            List<Move> leftDownMoves = PieceUtils.GetSlideMoves(piecePosition, state.boardState, -1, -1);
            List<Move> leftUpMoves = PieceUtils.GetSlideMoves(piecePosition, state.boardState, -1, 1);

            moves.AddRange(rightUpMoves);
            moves.AddRange(rightDownMoves);
            moves.AddRange(leftDownMoves);
            moves.AddRange(leftUpMoves);

            return moves;
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
