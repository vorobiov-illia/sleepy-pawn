using SleepyPawn.Core.Chess;

namespace SleepyPawn.Tests
{
    public class PerftTests
    {
        private struct PerftTask
        {
            internal Game game = new Game(true);
            internal int depth;

            internal PerftTask(Game game, int depth)
            {
                this.game = game;
                this.depth = depth;
            }
        }
        private ulong Perft(Game game ,int depth)
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

                if(currentTask.depth == 1)
                {
                    preResult += (ulong)currentTask.game.GetLegalMoves(ref moves);
                    continue;
                }

                int count = currentTask.game.GetLegalMoves(ref moves);

                for(int i = 0; i < count; i++)
                {
                    Game newGame = new Game(currentTask.game);
                    newGame.ForceMove(moves[i]);
                    tasks.Enqueue(new PerftTask(newGame, currentTask.depth - 1));
                }
            }

            ulong sufResult = 0;

            Parallel.ForEach(tasks, task => {
                Interlocked.Add(ref sufResult, RunPerftTask(task.game,task.depth));
            });

            return preResult + sufResult;
        }
        private ulong RunPerftTask(Game game, int depth)
        {
            Span<Move> moves = stackalloc Move[256];
            int count = game.GetLegalMoves(ref moves);


            if (depth == 1) return (ulong)count;

            ulong result = 0;

            for (int i = 0; i < count; i++)
            {
                Game newGame = new Game(game);
                newGame.ForceMove(moves[i]);
                result += RunPerftTask(newGame, depth - 1);
            }
            return result;
        }

        // Position and results from: https://chessprogramming.org/Perft_Results#initial-position
        [Theory]
        [InlineData(1, 20UL)]
        [InlineData(2, 400UL)]
        [InlineData(3, 8902UL)]
        [InlineData(4, 197281UL)]
        [InlineData(5, 4865609UL)]
        public void InitialPosition(int depth, ulong answer)
        {
            // Initializing game
            Game game = new Game(true);
            game.SetupInitialPosition();

            bool match = Perft(game, depth) == answer;

            Assert.True(match, "Perft results for initial position for depth " + depth + " must be equal to exactly " + answer + " ply.");
        }
        [Theory]
        [InlineData(1, 20UL)]
        [InlineData(2, 400UL)]
        [InlineData(3, 8902UL)]
        [InlineData(4, 197281UL)]
        public void InitialPositionSingleCore(int depth, ulong answer)
        {
            // Initializing game
            Game game = new Game(true);
            game.SetupInitialPosition();

            bool match = RunPerftTask(game, depth) == answer;

            Assert.True(match, "Perft results for initial position for depth " + depth + " must be equal to exactly " + answer + " ply.");
        }

        // Position and results from: https://chessprogramming.org/Perft_Results#position-2
        [Theory]
        [InlineData(1, 48UL)]
        [InlineData(2, 2039UL)]
        [InlineData(3, 97862UL)]
        [InlineData(4, 4085603UL)]
        [InlineData(5, 193690690UL)]
        public void Kiwipete(int depth, ulong answer)
        {
            // Initializing game
            Game game = new Game("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - ", true);

            bool match = Perft(game, depth) == answer;

            Assert.True(match, "Perft results for kiwipete position for depth " + depth + " must be equal to exactly " + answer + " ply.");
        }
        [Theory]
        [InlineData(1, 48UL)]
        [InlineData(2, 2039UL)]
        [InlineData(3, 97862UL)]
        [InlineData(4, 4085603UL)]
        public void KiwipeteSingleCore(int depth, ulong answer)
        {
            // Initializing game
            Game game = new Game("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - ", true);

            bool match = RunPerftTask(game, depth) == answer;

            Assert.True(match, "Perft results for kiwipete position for depth " + depth + " must be equal to exactly " + answer + " ply.");
        }

        // Position and results from: https://chessprogramming.org/Perft_Results#position-3
        [Theory]
        [InlineData(1, 14UL)]
        [InlineData(2, 191UL)]
        [InlineData(3, 2812UL)]
        [InlineData(4, 43238UL)]
        [InlineData(5, 674624UL)]
        public void PositionN3(int depth, ulong answer)
        {
            // Initializing game
            Game game = new Game("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 1 ", true);

            bool match = Perft(game, depth) == answer;

            Assert.True(match, "Perft results for N3 position for depth " + depth + " must be equal to exactly " + answer + " ply.");
        }
        [Theory]
        [InlineData(1, 14UL)]
        [InlineData(2, 191UL)]
        [InlineData(3, 2812UL)]
        [InlineData(4, 43238UL)]
        public void PositionN3SingleCore(int depth, ulong answer)
        {
            // Initializing game
            Game game = new Game("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 1 ", true);

            bool match = RunPerftTask(game, depth) == answer;

            Assert.True(match, "Perft results for N3 position for depth " + depth + " must be equal to exactly " + answer + " ply.");
        }

        // Position and results from: https://chessprogramming.org/Perft_Results#position-4
        [Theory]
        [InlineData(1, 6UL)]
        [InlineData(2, 264UL)]
        [InlineData(3, 9467UL)]
        [InlineData(4, 422333UL)]
        [InlineData(5, 15833292UL)]
        public void PositionN4(int depth, ulong answer)
        {
            // Initializing game
            Game game = new Game("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", true);

            bool match = Perft(game, depth) == answer;

            Assert.True(match, "Perft results for N4 position for depth " + depth + " must be equal to exactly " + answer + " ply.");
        }
        [Theory]
        [InlineData(1, 6UL)]
        [InlineData(2, 264UL)]
        [InlineData(3, 9467UL)]
        [InlineData(4, 422333UL)]
        public void PositionN4SingleCore(int depth, ulong answer)
        {
            // Initializing game
            Game game = new Game("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", true);

            bool match = RunPerftTask(game, depth) == answer;

            Assert.True(match, "Perft results for N4 position for depth " + depth + " must be equal to exactly " + answer + " ply.");
        }
    }
}
