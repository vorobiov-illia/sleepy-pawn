using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Search
{
    internal class Evaluator
    {
        internal int EvaluatePosition(SleepyChess chess, Color player)
        {
            if (player == Color.None) return 0;

            switch (chess.GetOutcome())
            {
                case Outcome.WhiteWon:
                    return player == Color.White ? 9999 : -9999;
                case Outcome.BlackWon:
                    return player == Color.Black ? 9999 : -9999;
                case Outcome.Draw:
                    return 0;
            }

            int playerMaterial = chess.GetState().material.GetMaterial(player);
            int oponentMaterial = chess.GetState().material.GetMaterial(ColorUtils.Reverse(player));

            return playerMaterial - oponentMaterial;
        }
    }
}
