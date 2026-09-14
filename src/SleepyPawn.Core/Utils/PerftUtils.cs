using SleepyPawn.Core.Chess;

namespace SleepyPawn.Core.Utils
{
    public static class PerftUtils
    {
        private struct PerftTask
        {
            internal SleepyChess game = new SleepyChess(true);
            internal int depth;

            internal PerftTask(SleepyChess game, int depth)
            {
                this.game = game;
                this.depth = depth;
            }
        }
        public static ulong Perft(SleepyChess game, int depth)
        {
            if (depth == 1)
            {
                Span<Move> res = stackalloc Move[256];
                return (ulong)game.GetLegalMoves(ref res);
            }

            int targetTaskCount = Environment.ProcessorCount;

            Queue<PerftTask> tasks = new Queue<PerftTask>();
            tasks.Enqueue(new PerftTask(game, depth));

            ulong preResult = 0;

            Span<Move> moves = stackalloc Move[256];

            while (tasks.Count > 0 && tasks.Count < targetTaskCount)
            {
                PerftTask currentTask = tasks.Dequeue();

                if (currentTask.depth == 1)
                {
                    preResult += (ulong)currentTask.game.GetLegalMoves(ref moves);
                    continue;
                }

                int count = currentTask.game.GetLegalMoves(ref moves);

                for (int i = 0; i < count; i++)
                {
                    SleepyChess newGame = new SleepyChess(currentTask.game);
                    newGame.ForceMove(moves[i]);
                    tasks.Enqueue(new PerftTask(newGame, currentTask.depth - 1));
                }
            }

            ulong sufResult = 0;

            Parallel.ForEach(tasks, task => {
                Interlocked.Add(ref sufResult, PerftSingleCore(task.game, task.depth));
            });

            return preResult + sufResult;
        }
        public static ulong PerftSingleCore(SleepyChess game, int depth)
        {
            Span<Move> moves = stackalloc Move[512];
            int count = game.GetLegalMoves(ref moves);

            if (depth == 1) return (ulong)count;

            ulong result = 0;

            for (int i = 0; i < count; i++)
            {
                game.ForceMove(moves[i]);
                result += PerftSingleCore(game, depth - 1);
                game.UndoMove();
            }
            return result;
        }
    }
}
