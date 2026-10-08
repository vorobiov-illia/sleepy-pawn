using SleepyPawn.Core.Chess;

namespace SleepyPawn.Core.Search
{
    internal class Negamax
    {
        internal int EvaluateMove(int depth, SleepyChess chess, Evaluator ev)
        {
            if (depth == 0) return ev.EvaluatePosition(chess, chess.GetPlayerToMove());
            int max = int.MinValue;

            Span<Move> moves = stackalloc Move[256];
            int count = chess.GetLegalMoves(ref moves);

            if (count == 0) return ev.EvaluatePosition(chess, chess.GetPlayerToMove());

            for (int i = 0; i < count; i++)
            {
                chess.ForceMove(moves[i]);
                int opponentScore = EvaluateMove(depth - 1, chess, ev) * -1;
                if (opponentScore > max)
                {
                    max = opponentScore;
                }
                chess.UndoMove();
            }
            return max;
        }
    }
}
