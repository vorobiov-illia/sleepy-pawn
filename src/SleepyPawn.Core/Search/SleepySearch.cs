using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Search
{
    public class SleepySearch
    {
        public Move GetBestMove(SleepyChess game, int depth)
        {
            Span<Move> moves = stackalloc Move[256];
            int count = game.GetLegalMoves(ref moves);

            if(count == 0)
            {
                Move nullMove = new Move();
                nullMove.FromLan("0000");
                return nullMove;
            }

            Negamax moveEvaluator = new Negamax();
            Evaluator positionEvaluator = new Evaluator();
            int maxScore = -9999;
            int maxScoreId = 0;

            for (int i = 0; i < count; i++)
            {
                game.ForceMove(moves[i]);
                int score = moveEvaluator.EvaluateMove(depth - 1, game, positionEvaluator) * -1;
                if (score > maxScore)
                {
                    maxScore = score;
                    maxScoreId = i;
                }
                game.UndoMove();
            }

            return moves[maxScoreId];
        }
    }
}
