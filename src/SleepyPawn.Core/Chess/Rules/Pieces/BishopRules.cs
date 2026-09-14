using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;
using System.Net.Http.Headers;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class BishopRules : PieceRule
    {
        internal override bool CanMove(Move move, ChessState state)
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

        internal override int GeneratePseudoMoves(EnginePosition piecePosition, ChessState state, ref Span<Move> pseudoMoves, int count)
        {
            int newCount = count;
            
            Piece bishop = state.GetPiece(piecePosition);
            if (bishop.isEmpty) return newCount;
            if (bishop.type != PieceType.Bishop) return newCount;

            newCount = PieceUtils.GetSlideMoves(piecePosition, state.boardState, 1, 1, ref pseudoMoves, newCount);
            newCount = PieceUtils.GetSlideMoves(piecePosition, state.boardState, 1, -1, ref pseudoMoves, newCount);
            newCount = PieceUtils.GetSlideMoves(piecePosition, state.boardState, -1, -1, ref pseudoMoves, newCount);
            newCount = PieceUtils.GetSlideMoves(piecePosition, state.boardState, -1, 1, ref pseudoMoves, newCount);

            return newCount;
        }

        internal override void GenerateThreat(EnginePosition piecePosition, Board board, ThreatBoard threats)
        {
            Piece thisPiece = board.GetPiece(piecePosition);

            if (thisPiece.isEmpty) return;
            if (thisPiece.type != PieceType.Bishop) return;

            PieceUtils.SlideThreat(piecePosition, board, threats, 1, 1, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, -1, 1, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, 1, -1, thisPiece.color);
            PieceUtils.SlideThreat(piecePosition, board, threats, -1, -1, thisPiece.color);
        }
    }
}
