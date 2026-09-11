using SleepyPawn.Core.Chess;

namespace SleepyPawn.Tests
{
    public class PerftTests
    {
        private ulong Perft(Game game ,int depth)
        {
            if (depth == 1) return (ulong)game.GetLegalMoveCount();

            ulong result = 0;

            List<Move> moves = game.GetLegalMoves();

            foreach(Move move in moves)
            {
                Game newGame = new Game(game);
                newGame.TryMove(move);
                result += Perft(newGame, depth-1);
            }
            return result;
        }

        // Position and results from: https://chessprogramming.org/Perft_Results#initial-position
        [Fact]
        public void InitialPosition()
        {
            // Initializing game
            Game game = new Game(true);
            game.SetupInitialPosition();

            bool depth1 = Perft(game, 1) == 20UL;
            bool depth2 = Perft(game, 2) == 400UL;
            bool depth3 = Perft(game, 3) == 8902UL;
            bool depth4 = Perft(game, 4) == 197281UL;
            bool depth5 = Perft(game, 5) == 4865609UL;

            Assert.True(depth1, "Perft results for initial position for depth 1 must be equal to exactly 20 ply.");
            Assert.True(depth2, "Perft results for initial position for depth 2 must be equal to exactly 400 ply.");
            Assert.True(depth3, "Perft results for initial position for depth 3 must be equal to exactly 8,902 ply.");
            Assert.True(depth4, "Perft results for initial position for depth 4 must be equal to exactly 197,281 ply.");
            Assert.True(depth5, "Perft results for initial position for depth 5 must be equal to exactly 4,865,609 ply.");
        }

        // Position and results from: https://chessprogramming.org/Perft_Results#position-2
        [Fact]
        public void Kiwipete()
        {
            // Initializing game
            Game game = new Game("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - ", true);

            bool depth1 = Perft(game, 1) == 48UL;
            bool depth2 = Perft(game, 2) == 2039UL;
            bool depth3 = Perft(game, 3) == 97862UL;
            bool depth4 = Perft(game, 4) == 4085603UL;
            bool depth5 = Perft(game, 5) == 193690690UL;

            Assert.True(depth1, "Perft results for initial position for depth 1 must be equal to exactly 48 ply.");
            Assert.True(depth2, "Perft results for initial position for depth 2 must be equal to exactly 2,039 ply.");
            Assert.True(depth3, "Perft results for initial position for depth 3 must be equal to exactly 97,862 ply.");
            Assert.True(depth4, "Perft results for initial position for depth 4 must be equal to exactly 4,085,603 ply.");
            Assert.True(depth5, "Perft results for initial position for depth 5 must be equal to exactly 193,690,690 ply.");
        }

        // Position and results from: https://chessprogramming.org/Perft_Results#position-3
        [Fact]
        public void PositionN3()
        {
            // Initializing game
            Game game = new Game("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 1 ", true);

            bool depth1 = Perft(game, 1) == 14UL;
            bool depth2 = Perft(game, 2) == 191UL;
            bool depth3 = Perft(game, 3) == 2812UL;
            bool depth4 = Perft(game, 4) == 43238UL;
            bool depth5 = Perft(game, 5) == 674624UL;

            Assert.True(depth1, "Perft results for initial position for depth 1 must be equal to exactly 14 ply.");
            Assert.True(depth2, "Perft results for initial position for depth 2 must be equal to exactly 191 ply.");
            Assert.True(depth3, "Perft results for initial position for depth 3 must be equal to exactly 2,812 ply.");
            Assert.True(depth4, "Perft results for initial position for depth 4 must be equal to exactly 43,238 ply.");
            Assert.True(depth5, "Perft results for initial position for depth 5 must be equal to exactly 674,624 ply.");
        }

        // Position and results from: https://chessprogramming.org/Perft_Results#position-4
        [Fact]
        public void PositionN4()
        {
            // Initializing game
            Game game = new Game("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", true);

            bool depth1 = Perft(game, 1) == 6UL;
            bool depth2 = Perft(game, 2) == 264UL;
            bool depth3 = Perft(game, 3) == 9467UL;
            bool depth4 = Perft(game, 4) == 422333UL;
            bool depth5 = Perft(game, 5) == 15833292UL;

            Assert.True(depth1, "Perft results for initial position for depth 1 must be equal to exactly 6 ply.");
            Assert.True(depth2, "Perft results for initial position for depth 2 must be equal to exactly 264 ply.");
            Assert.True(depth3, "Perft results for initial position for depth 3 must be equal to exactly 9,467 ply.");
            Assert.True(depth4, "Perft results for initial position for depth 4 must be equal to exactly 422,333 ply.");
            Assert.True(depth5, "Perft results for initial position for depth 5 must be equal to exactly 15,833,292 ply.");
        }
    }
}
