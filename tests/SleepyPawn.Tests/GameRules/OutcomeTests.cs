using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Tests.GameRules
{
    public class OutcomeTests
    {
        [Theory]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
        [InlineData("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq -")]
        [InlineData("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 1")]
        [InlineData("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1")]
        [InlineData("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1 ")]
        [InlineData("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8")]
        [InlineData("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10")]
        [InlineData("3rkb1r/pQpbp1pp/8/8/8/8/PPP2PPP/RN2KB1R b KQ - 0 12")]
        [InlineData("rnbk1b1r/ppp1pQpp/8/4N3/8/8/PPPn1PPP/RN2KB1R w KQ - 1 8")]
        [InlineData("rnb1kb1r/ppp1pQpp/8/4N3/8/8/PPPn1PPP/RN2KB1R b KQkq - 0 7")]
        [InlineData("6k1/5pp1/7p/8/7q/3n4/5rK1/8 w - - 2 49")]
        [InlineData("7k/8/8/8/8/8/1q6/K7 w - - 0 1")]
        [InlineData("8/2rrr3/2rkr3/2rRr3/8/8/8/7K b - - 0 1")]
        public void InProgressPosition(string position)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(position, true);

            // Getting outcome for the game
            Outcome result = game.GetOutcome();
            Assert.True(result == Outcome.InProgress, "This position must be neither mate nor stalemate.");
        }

        [Theory]
        [InlineData("rnbqkb1r/ppp1pQpp/8/3pN3/4n3/8/PPPP1PPP/RNB1KB1R b KQkq - 0 1")]
        [InlineData("7k/8/8/8/8/2p5/1q6/K7 w - - 0 1")]
        [InlineData("7k/6Q1/5P2/8/8/8/8/K7 b - - 0 1")]
        [InlineData("k7/8/8/8/8/8/4r3/3r3K w - - 0 1")]
        [InlineData("k2R4/2R5/8/8/8/8/8/7K b - - 0 1")]
        [InlineData("8/8/2BRB3/2RKR3/2BRB3/4n3/8/6k1 w - - 0 1")]
        [InlineData("8/8/2brb3/2rkr3/2brb3/4N3/8/5K2 b - - 0 1")]
        [InlineData("6k1/5pp1/7p/8/8/3n4/5r1q/6K1 w - - 4 50")]
        [InlineData("4r2k/p1p1N2p/3p1Qp1/8/8/1P6/P1Pq1PPP/R3R1K1 b - - 0 21")]
        [InlineData("rR4K1/P7/R7/8/8/8/k7/8 b - - 0 1")]
        [InlineData("8/8/8/8/8/q1n5/Rb5k/K7 w - - 0 1")]
        [InlineData("3Qk3/5p2/4b3/p7/4N3/1P6/P1P5/3RK3 b - - 0 31")]
        public void MatePosition(string position)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(position, true);

            // Getting outcome for the game
            Outcome result = game.GetOutcome();
            Assert.True(result == Outcome.WhiteWon || result == Outcome.BlackWon, "This position must be a mate.");
        }

        [Theory]
        [InlineData("rnbqkb1r/ppp1pQpp/8/3pN3/4n3/8/PPPP1PPP/RNB1KB1R b KQkq - 0 1")]
        [InlineData("7k/6Q1/5P2/8/8/8/8/K7 b - - 0 1")]
        [InlineData("k2R4/2R5/8/8/8/8/8/7K b - - 0 1")]
        [InlineData("8/8/2brb3/2rkr3/2brb3/4N3/8/5K2 b - - 0 1")]
        [InlineData("4r2k/p1p1N2p/3p1Qp1/8/8/1P6/P1Pq1PPP/R3R1K1 b - - 0 21")]
        [InlineData("rR4K1/P7/R7/8/8/8/k7/8 b - - 0 1")]
        [InlineData("3Qk3/5p2/4b3/p7/4N3/1P6/P1P5/3RK3 b - - 0 31")]
        public void WhiteWon(string position)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(position, true);

            // Getting outcome for the game
            Outcome result = game.GetOutcome();
            Assert.True(result == Outcome.WhiteWon, "This position must result in White's victory.");
        }

        [Theory]
        [InlineData("7k/8/8/8/8/2p5/1q6/K7 w - - 0 1")]
        [InlineData("k7/8/8/8/8/8/4r3/3r3K w - - 0 1")]
        [InlineData("8/8/2BRB3/2RKR3/2BRB3/4n3/8/6k1 w - - 0 1")]
        [InlineData("6k1/5pp1/7p/8/8/3n4/5r1q/6K1 w - - 4 50")]
        [InlineData("8/8/8/8/8/q1n5/Rb5k/K7 w - - 0 1")]
        public void BlackWon(string position)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(position, true);

            // Getting outcome for the game
            Outcome result = game.GetOutcome();
            Assert.True(result == Outcome.BlackWon, "This position must result in Black's victory.");
        }

        [Theory]
        // 50-Move rule:
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 100 1")]
        [InlineData("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 100")]
        [InlineData("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 100 1")]
        [InlineData("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 100 1")]
        [InlineData("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 100 1 ")]
        [InlineData("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 100 8")]
        [InlineData("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 100 10")]
        [InlineData("3rkb1r/pQpbp1pp/8/8/8/8/PPP2PPP/RN2KB1R b KQ - 100 12")]
        [InlineData("rnbk1b1r/ppp1pQpp/8/4N3/8/8/PPPn1PPP/RN2KB1R w KQ - 100 8")]
        [InlineData("rnb1kb1r/ppp1pQpp/8/4N3/8/8/PPPn1PPP/RN2KB1R b KQkq - 100 7")]
        [InlineData("6k1/5pp1/7p/8/7q/3n4/5rK1/8 w - - 100 49")]
        [InlineData("7k/8/8/8/8/8/1q6/K7 w - - 100 1")]
        [InlineData("8/2rrr3/2rkr3/2rRr3/8/8/8/7K b - - 100 1")]
        // No legal moves positions:
        [InlineData("6RK/8/8/8/8/8/7p/7k b - - 0 1")]
        [InlineData("K7/P7/8/8/8/8/8/kr6 w - - 0 1")]
        [InlineData("K6k/7P/7Q/8/8/8/8/8 b - - 0 1")]
        [InlineData("8/8/8/8/8/q7/p7/K6k w - - 0 1")]
        [InlineData("8/8/8/8/8/3k4/3p4/3K4 w - - 0 1")]
        [InlineData("3k4/3P4/3K4/8/8/8/8/8 b - - 0 1")]
        [InlineData("KB5r/8/1k6/8/8/8/8/8 w - - 0 1")]
        [InlineData("8/8/8/8/8/6K1/8/R5bk b - - 0 1")]
        [InlineData("8/8/8/8/8/8/4p1pp/4Kbrk b - - 0 1")]
        // Not enough material:
        [InlineData("4k3/8/8/8/8/8/8/4K3 w - - 0 1")]
        [InlineData("8/8/8/5k2/8/8/2K5/8 b - - 0 1")]
        [InlineData("8/1K6/8/8/8/8/3k4/8 w - - 0 1")]
        [InlineData("8/8/8/3K4/5k2/8/8/8 b - - 0 1")]
        [InlineData("4k3/8/8/8/8/8/2N5/4K3 b - - 0 1")]
        [InlineData("4k3/2n5/8/8/8/8/8/4K3 w - - 0 1")]
        [InlineData("4k3/8/8/8/8/8/8/3BK3 w - - 0 1")]
        [InlineData("4k3/8/8/8/8/8/3B4/4K3 w - - 0 1")]
        [InlineData("4k3/8/8/1B6/8/8/8/4K3 w - - 0 1")]
        [InlineData("4kb2/8/8/8/8/8/8/4K3 b - - 0 1")]
        [InlineData("4k3/5b2/8/8/8/8/8/4K3 b - - 0 1")]
        [InlineData("4k3/8/8/8/8/6b1/8/4K3 b - - 0 1")]
        [InlineData("4k3/8/4B3/8/8/8/6b1/4K3 b - - 0 1")]
        [InlineData("4k3/8/3B4/8/3b4/8/8/4K3 w - - 0 1")]

        
        public void StalematePosition(string position)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(position, true);

            // Getting outcome for the game
            Outcome result = game.GetOutcome();
            Assert.True(result == Outcome.Draw, "This position must be a stalemate.");
        }

        [Theory]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", new string[] { "g1f3", "b8c6", "f3g1", "c6b8", "g1f3", "b8c6", "f3g1", "c6b8" }, true)]
        [InlineData("r3k2r/8/8/8/8/8/8/R3K2R w KQkq - 0 1", new string[] { "e1e2", "e8e7", "e2e1", "e7e8", "e1e2", "e8e7", "e2e1", "e7e8" }, false)]
        [InlineData("r3k2r/8/8/8/8/8/8/R3K2R w KQkq - 0 1", new string[] { "e1e2", "e8e7", "e2e1", "e7e8", "e1e2", "e8e7", "e2e1", "e7e8", "e1e2", "e8e7" }, true)]
        [InlineData("4k3/8/8/8/8/8/4P3/4K3 w - - 0 1", new string[] { "e1d1", "e8d8", "d1e1", "d8e8", "e2e3", "e8d8", "e1d1", "d8e8", "d1e1", "e8d8", "e1d1" }, false)]
        public void StalemateByRepetitionRule(string fen, string[] moves, bool result)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(fen, true);

            // Making moves that will lead to stalemate by repetiton rule.
            foreach (string move in moves)
            {
                game.TryUciMove(move);
            }

            // Getting outcome for the game
            Outcome outcome = game.GetOutcome();
            Assert.True((outcome == Outcome.Draw) == result, "This position must be a stalemate by repetiton rule.");
        }
    }
}
