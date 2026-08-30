using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class PawnRules : PieceRule
    {
        internal override bool CanMove(Move move, GameState state)
        {
            Piece thisPiece = state.GetPiece(move.firstPos);
            Piece otherPiece = state.GetPiece(move.secondPos);

            if (!CommonCheck(move, state, PieceType.Sleepy, thisPiece, otherPiece)) return false;

            if (otherPiece.isEmpty)
            {
                if (move.firstPos.x != move.secondPos.x) return false;
                if(thisPiece.color == Color.White)
                {
                    if (move.secondPos.y == move.firstPos.y + 1) return true;
                    if (move.firstPos.y != 1) return false;
                    Piece pathway = state.GetPiece(new EnginePosition(move.firstPos.x, move.firstPos.y + 1));
                    if(pathway == null)
                    {
                        pathway = new Piece();
                    }
                    if (move.secondPos.y == move.firstPos.y + 2 &&
                        pathway.isEmpty) return true;
                    return false;
                }
                if(thisPiece.color == Color.Black)
                {
                    if (move.secondPos.y == move.firstPos.y - 1) return true;
                    if (move.firstPos.y != 6) return false;
                    Piece pathway = state.GetPiece(new EnginePosition(move.firstPos.x, move.firstPos.y - 1));
                    if (pathway == null)
                    {
                        pathway = new Piece();
                    }
                    if (move.secondPos.y == move.firstPos.y - 2 &&
                        pathway.isEmpty) return true;
                    return false;
                }
                return false;
            }
            else
            {
                if(otherPiece.color == thisPiece.color) return false;
                if (thisPiece.color == Color.White)
                {
                    if (move.secondPos != new EnginePosition(move.firstPos.x + 1, move.firstPos.y + 1) &&
                        move.secondPos != new EnginePosition(move.firstPos.x - 1, move.firstPos.y + 1)) return false;
                    return true;
                }
                if (thisPiece.color == Color.Black)
                {
                    if (move.secondPos != new EnginePosition(move.firstPos.x + 1, move.firstPos.y - 1) &&
                        move.secondPos != new EnginePosition(move.firstPos.x - 1, move.firstPos.y - 1)) return false;
                    return true;
                }
                return true;
            }
        }

        internal override void GenerateThreat(EnginePosition piecePosition, Board board, ThreatBoard threats)
        {
            Piece thisPiece = board.GetPiece(piecePosition);

            if (thisPiece == null) return;
            if (thisPiece.isEmpty) return;
            if (thisPiece.type != PieceType.Sleepy) return;

            if(thisPiece.color == Color.White)
            {
                threats.AddThreat(new EnginePosition(piecePosition.x - 1, piecePosition.y + 1), Color.White);
                threats.AddThreat(new EnginePosition(piecePosition.x + 1, piecePosition.y + 1), Color.White);
            }
            else if (thisPiece.color == Color.Black)
            {
                threats.AddThreat(new EnginePosition(piecePosition.x - 1, piecePosition.y - 1), Color.Black);
                threats.AddThreat(new EnginePosition(piecePosition.x + 1, piecePosition.y - 1), Color.Black);
            }
        }
    }
}
