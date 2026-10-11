using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Search.Evaluators;
using SleepyPawn.Core.Search.PositionEvaluator;
using SleepyPawn.Core.Search.SearchAlgorithms;

namespace SleepyPawn.Core.Search
{
    public class SleepySearch
    {
        public Move GetBestMove(SleepyChess game, int depth)
        {
            NegamaxAlphaBeta search = new NegamaxAlphaBeta();
            Evaluator evaluator = new Evaluator();
            return search.FindBestMove(depth, game, evaluator);
        }
    }
}
