using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Search.PositionEvaluator;

namespace SleepyPawn.Core.Search.SearchAlgorithms
{
    internal interface ISearch
    {
        public Move FindBestMove(int depth, SleepyChess chess, Evaluator ev);
    }
}
