using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess.Rules
{
    public class Move
    {
        internal EnginePosition firstPos;
        internal EnginePosition secondPos;
        internal FigureType promotionFigure;
        internal bool isValid { get; private set; }
        internal bool nullMove { get; private set; }

        public Move()
        {
            isValid = false;
            firstPos = new EnginePosition();
            firstPos = new EnginePosition();
        }

        public void FromUci(string uciMove)
        {
            if (!UciUtils.ValidateMove(uciMove))
            {
                isValid = false;
                return;
            }
            isValid = true;

            if (UciUtils.IsNullMove(uciMove))
            {
                nullMove = true;
                return;
            }

            int fx = UciUtils.UciToEngineChar(uciMove[0]);
            int fy = UciUtils.UciToEngineChar(uciMove[1]);

            int sx = UciUtils.UciToEngineChar(uciMove[2]);
            int sy = UciUtils.UciToEngineChar(uciMove[3]);

            firstPos = new EnginePosition(fx,fy);
            secondPos = new EnginePosition(sx, sy);

            if(uciMove.Length == 5)
            {
                promotionFigure = UciUtils.UciToEnginePromotion(uciMove[4]);
            }

            return;
        }
    }
}
