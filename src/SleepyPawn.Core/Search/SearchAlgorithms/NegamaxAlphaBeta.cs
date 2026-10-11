using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Search.PositionEvaluator;

namespace SleepyPawn.Core.Search.SearchAlgorithms
{
    internal class NegamaxAlphaBeta : ISearch
    {
        public Move FindBestMove(int depth, SleepyChess chess, Evaluator ev)
        {
            Span<Move> moves = stackalloc Move[256];
            int count = chess.GetLegalMoves(ref moves);

            if (count == 0)
            {
                Move nullMove = new Move();
                nullMove.FromLan("0000");
                return nullMove;
            }

            int maxScoreId = 0;

            int alpha = -999999;
            int beta = 999999;

            for (int i = 0; i < count; i++)
            {
                chess.ForceMove(moves[i]);
                int score = EvaluateMove(depth - 1, -beta, -alpha, chess, ev) * -1;
                if (alpha < score)
                {
                    alpha = score;
                    maxScoreId = i;
                }
                chess.UndoMove();
                if (beta <= alpha) break;
            }

            return moves[maxScoreId];
        }

        private int EvaluateMove(int depth, int alpha, int beta, SleepyChess chess, Evaluator ev)
        {
            Span<Move> moves = stackalloc Move[256];
            int count = chess.GetLegalMoves(ref moves);

            if(count == 0)
            {
                if (chess.KingChecked()) return -100000 + chess.GetState().ply;
                return 0;
            }
            if (chess.GetState().CheckRepetitionRule(2) || chess.GetState().info.halfMoveClock >= 100)
            {
                return 0;
            }

            if (depth == 0) return ev.EvaluatePosition(chess, chess.GetPlayerToMove());

            for (int i = 0; i < count; i++)
            {
                chess.ForceMove(moves[i]);
                int opponentScore = EvaluateMove(depth - 1, -beta, -alpha, chess, ev) * -1;
                if (alpha < opponentScore) alpha = opponentScore;
                
                chess.UndoMove();
                if (beta <= alpha) break;
            }
            return alpha;
        }
    }
}
