using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Chess.Rules;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess
{
    public struct Move
    {
        internal EnginePosition firstPos;
        internal EnginePosition secondPos;
        internal PieceType promotionPiece;
        internal bool isValid { get; private set; }
        internal bool nullMove { get; private set; }

        public Move()
        {
            isValid = false;
            firstPos = BoardUtils.IllegalPosition;
            secondPos = BoardUtils.IllegalPosition;
        }

        internal Move(EnginePosition fp, EnginePosition sp, PieceType promotion = PieceType.None)
        {
            isValid = true;
            nullMove = false;
            firstPos = fp;
            secondPos = sp;
            promotionPiece = promotion;

            isValid = LegalMoveAnalyzer.IsValid(this);
        }

        public void FromLan(string lanMove)
        {
            if (!LanUtils.ValidateMove(lanMove))
            {
                isValid = false;
                return;
            }
            isValid = true;

            if (LanUtils.IsNullMove(lanMove))
            {
                nullMove = true;
                return;
            }

            int fx = LanUtils.LanToEngineChar(lanMove[0]);
            int fy = LanUtils.LanToEngineChar(lanMove[1]);

            int sx = LanUtils.LanToEngineChar(lanMove[2]);
            int sy = LanUtils.LanToEngineChar(lanMove[3]);

            firstPos = new EnginePosition(fx,fy);
            secondPos = new EnginePosition(sx, sy);

            if(firstPos == secondPos)
            {
                isValid = false;
                return;
            }

            if(lanMove.Length == 5)
            {
                promotionPiece = LanUtils.LanToEnginePromotion(lanMove[4]);
            }

            return;
        }
    }
}
