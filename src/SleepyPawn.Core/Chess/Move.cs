using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Chess.Rules;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess
{
    public class Move
    {
        internal EnginePosition firstPos;
        internal EnginePosition secondPos;
        internal PieceType promotionPiece;
        internal bool isValid { get; private set; }
        internal bool nullMove { get; private set; }

        public Move()
        {
            isValid = false;
            firstPos = new EnginePosition();
            secondPos = new EnginePosition();
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

            if(firstPos == secondPos)
            {
                isValid = false;
                return;
            }

            if(uciMove.Length == 5)
            {
                promotionPiece = UciUtils.UciToEnginePromotion(uciMove[4]);
            }

            return;
        }
    }
}
