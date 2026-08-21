using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class PawnRules : PieceRule
    {
        internal override bool CanMove(Move move, GameState state)
        {
            if (!CommonCheck(move, state, PieceType.Sleepy)) return false;

            if (otherFigure.isEmpty)
            {
                if (move.firstPos.x != move.secondPos.x) return false;
                if(thisFigure.color == Color.White)
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
                if(thisFigure.color == Color.Black)
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
                if(otherFigure.color == thisFigure.color) return false;
                if (thisFigure.color == Color.White)
                {
                    if (move.secondPos != new EnginePosition(move.firstPos.x + 1, move.firstPos.y + 1) &&
                        move.secondPos != new EnginePosition(move.firstPos.x - 1, move.firstPos.y + 1)) return false;
                    return true;
                }
                if (thisFigure.color == Color.Black)
                {
                    if (move.secondPos != new EnginePosition(move.firstPos.x + 1, move.firstPos.y - 1) &&
                        move.secondPos != new EnginePosition(move.firstPos.x - 1, move.firstPos.y - 1)) return false;
                    return true;
                }
                return true;
            }
        }
    }
}
