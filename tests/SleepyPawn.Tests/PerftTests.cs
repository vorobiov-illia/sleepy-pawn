using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Tests
{
    public class PerftTests
    {
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

            bool match = PerftUtils.Perft(game, depth) == answer;

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

            bool match = PerftUtils.Perft(game, depth) == answer;

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

            bool match = PerftUtils.Perft(game, depth) == answer;

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

            bool match = PerftUtils.Perft(game, depth) == answer;

            Assert.True(match, "Perft results for N4 position for depth " + depth + " must be equal to exactly " + answer + " ply.");
        }
    }
}
