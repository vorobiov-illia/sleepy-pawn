using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess.Rules.Figures
{
    internal class PawnRules : FigureRule
    {
        internal override bool CanMove(Move move, GameState state)
        {
            Figure thisFigure = state.GetFigure(move.firstPos);
            Figure otherFigure = state.GetFigure(move.secondPos);

            if (thisFigure == null) return false;
            if (thisFigure.type != FigureType.Sleepy) return false;

            if(otherFigure == null)
            {
                otherFigure = new Figure();
            }

            if (otherFigure.isEmpty)
            {
                if (move.firstPos.x != move.secondPos.x) return false;
                if(thisFigure.color == Color.White)
                {
                    if (move.secondPos.y == move.firstPos.y + 1) return true;
                    if (thisFigure.Moved) return false;
                    Figure pathway = state.GetFigure(new EnginePosition(move.firstPos.x, move.firstPos.y + 1));
                    if(pathway == null)
                    {
                        pathway = new Figure();
                    }
                    if (move.secondPos.y == move.firstPos.y + 2 &&
                        pathway.isEmpty) return true;
                    return false;
                }
                if(thisFigure.color == Color.Black)
                {
                    if (move.secondPos.y == move.firstPos.y - 1) return true;
                    if (thisFigure.Moved) return false;
                    Figure pathway = state.GetFigure(new EnginePosition(move.firstPos.x, move.firstPos.y - 1));
                    if (pathway == null)
                    {
                        pathway = new Figure();
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
