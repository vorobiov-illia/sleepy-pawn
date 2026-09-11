using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class KnightRules : PieceRule
    {
        internal override bool CanMove(Move move, GameState state)
        {
            Piece thisPiece = state.GetPiece(move.firstPos);
            Piece otherPiece = state.GetPiece(move.secondPos);

            if (!CommonCheck(move, state, PieceType.Knight, thisPiece, otherPiece)) return false;

            if (!otherPiece.isEmpty &&
                otherPiece.color == thisPiece.color) return false;

            return IsLMove(move);
        }

        internal override List<Move> GeneratePseudoMoves(EnginePosition piecePosition, GameState state)
        {
            List<Move> moves = new List<Move>();

            Piece knight = state.GetPiece(piecePosition);
            if (knight.isEmpty) return moves;
            if (knight.type != PieceType.Knight) return moves;

            Move rightUpUpMove = new Move(piecePosition, new EnginePosition(piecePosition.x + 1, piecePosition.y + 2));
            Move rightRightUpMove = new Move(piecePosition, new EnginePosition(piecePosition.x + 2, piecePosition.y + 1));
            Move rightRightDownMove = new Move(piecePosition, new EnginePosition(piecePosition.x + 2, piecePosition.y - 1));
            Move rightDownDownMove = new Move(piecePosition, new EnginePosition(piecePosition.x + 1, piecePosition.y - 2));

            Move leftUpUpMove = new Move(piecePosition, new EnginePosition(piecePosition.x - 1, piecePosition.y + 2));
            Move leftLeftUpMove = new Move(piecePosition, new EnginePosition(piecePosition.x - 2, piecePosition.y + 1));
            Move leftLeftDownMove = new Move(piecePosition, new EnginePosition(piecePosition.x - 2, piecePosition.y - 1));
            Move leftDownDownMove = new Move(piecePosition, new EnginePosition(piecePosition.x - 1, piecePosition.y - 2));

            moves.Add(rightUpUpMove);
            moves.Add(rightRightUpMove);
            moves.Add(rightRightDownMove);
            moves.Add(rightDownDownMove);

            moves.Add(leftUpUpMove);
            moves.Add(leftLeftUpMove);
            moves.Add(leftLeftDownMove);
            moves.Add(leftDownDownMove);

            return moves;
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
