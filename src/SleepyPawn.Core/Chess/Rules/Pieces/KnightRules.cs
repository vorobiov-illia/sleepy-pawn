using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class KnightRules : PieceRule
    {
        internal override bool CanMove(Move move, ChessState state)
        {
            Piece thisPiece = state.GetPiece(move.firstPos);
            Piece otherPiece = state.GetPiece(move.secondPos);

            if (!CommonCheck(move, state, PieceType.Knight, thisPiece, otherPiece)) return false;

            if (!otherPiece.isEmpty &&
                otherPiece.color == thisPiece.color) return false;

            return IsLMove(move);
        }

        internal override int GeneratePseudoMoves(EnginePosition piecePosition, ChessState state, ref Span<Move> pseudoMoves, int count)
        {
            int newCount = count;
            Piece knight = state.GetPiece(piecePosition);
            if (knight.isEmpty) return newCount;
            if (knight.type != PieceType.Knight) return newCount;

            Move rightUpUpMove = new Move(piecePosition, new EnginePosition(piecePosition.x + 1, piecePosition.y + 2));
            Move rightRightUpMove = new Move(piecePosition, new EnginePosition(piecePosition.x + 2, piecePosition.y + 1));
            Move rightRightDownMove = new Move(piecePosition, new EnginePosition(piecePosition.x + 2, piecePosition.y - 1));
            Move rightDownDownMove = new Move(piecePosition, new EnginePosition(piecePosition.x + 1, piecePosition.y - 2));

            Move leftUpUpMove = new Move(piecePosition, new EnginePosition(piecePosition.x - 1, piecePosition.y + 2));
            Move leftLeftUpMove = new Move(piecePosition, new EnginePosition(piecePosition.x - 2, piecePosition.y + 1));
            Move leftLeftDownMove = new Move(piecePosition, new EnginePosition(piecePosition.x - 2, piecePosition.y - 1));
            Move leftDownDownMove = new Move(piecePosition, new EnginePosition(piecePosition.x - 1, piecePosition.y - 2));

            pseudoMoves[newCount++] = rightUpUpMove;
            pseudoMoves[newCount++] = rightRightUpMove;
            pseudoMoves[newCount++] = rightRightDownMove;
            pseudoMoves[newCount++] = rightDownDownMove;

            pseudoMoves[newCount++] = leftUpUpMove;
            pseudoMoves[newCount++] = leftLeftUpMove;
            pseudoMoves[newCount++] = leftLeftDownMove;
            pseudoMoves[newCount++] = leftDownDownMove;

            return newCount;
        }

        internal override void GenerateThreat(EnginePosition piecePosition, Board board, ThreatBoard threats)
        {
            Piece thisPiece = board.GetPiece(piecePosition);

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
