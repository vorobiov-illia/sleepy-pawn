using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Search.Evaluators
{
    internal interface IEvaluator
    {
        public int EvaluatePosition(SleepyChess chess, Color player);
    }
}
