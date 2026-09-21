using SleepyPawn.Core.Utils;

namespace SleepyPawn.Tests
{
    public class FenTests
    {
        [Theory]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
        [InlineData("8/8/8/8/8/8/8/8 w KQkq - 0 1")]
        [InlineData("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - ")]
        [InlineData("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 1 ")]
        [InlineData("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1")]
        [InlineData("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8  ")]
        [InlineData("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10 ")]
        [InlineData("rnbqkbnr/pppppppp/8/35/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
        [InlineData("rnbqkbnr/ppp1p1pp/8/35/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1")]
        [InlineData("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R b KQ - 1 8  ")]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq -")]
        // Incorrect player index, but Engine should pass this case and use default value.
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR i KQkq - 0 1")]
        // Incorrect castling information, but Engine should pass this case and use default value.
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w XXXX - 0 1")]
        // Incorrect En Passant information, but Engine should pass this case and use default value.
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq yyyyyyyyyy 0 1")]
        public void ValidPosition(string fen)
        {
            Assert.True(FenUtils.IsValid(fen), "This FEN position must be valid.");
        }

        [Fact]
        public void NullString()
        {
            Assert.False(FenUtils.IsValid(null!), "Null must not be a valid FEN position.");
        }

        [Theory]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP w KQkq - 0 1")]
        [InlineData("pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP w KQkq - 0 1")]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
        [InlineData("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/PPPBBPPP/R3K2R w KQkq - ")]
        [InlineData("r3k2r/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - ")]
        [InlineData("8/8/8/8/8/8/8 w KQkq - 0 1")]
        [InlineData("8/8/8 w KQkq - 0 1")]
        [InlineData("w KQkq - 0 1")]
        [InlineData("PPPPPPPP b KQkq - 0 1")]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/RNBQKBNR b KQkq - 0 1")]
        public void NotEnoughRanks(string fen)
        {
            Assert.False(FenUtils.IsValid(fen), "Count of ranks in FEN position must be exactly 8.");
        }

        [Theory]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR/PPPPPPPP w KQkq - 0 1")]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/8/8/8/8/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
        public void TooManyRanks(string fen)
        {
            Assert.False(FenUtils.IsValid(fen), "Count of ranks in FEN position must be exactly 8.");
        }

        [Theory]
        [InlineData("rnbqkbnr/ppppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
        [InlineData("/////// w KQkq - 0 1")]
        [InlineData("8/2/8/8/8/8/8/8 w KQkq - 0 1")]
        [InlineData("r3k2r/p1piqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - ")]
        [InlineData("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/7 w - - 0 1 ")]
        // Used some Russian letters instead of English.
        [InlineData("r3k2r/Pppp1ррp/1b3nbN/nP6/ВВP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1")]
        [InlineData("rnbq1k1r/pp1Pbppp/2p5/8/2B6/8/PPP1NnPP/RNB3QK2R w KQ - 1 8  ")]
        public void InvalidRanks(string fen)
        {
            Assert.False(FenUtils.IsValid(fen), "Sum of tiles for each rank must be exactly 8, and ranks must contain only English letters (that represent chess pieces) and digits.");
        }

        [Theory]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq")]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w")]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR")]
        [InlineData("8/8/8/8/8/8/8/8 w KQkq")]
        [InlineData("8/8/8/8/8/8/8/8 w")]
        [InlineData("8/8/8/8/8/8/8/8")]
        [InlineData("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq")]
        [InlineData("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w")]
        [InlineData("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R")]
        [InlineData("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - ")]
        [InlineData("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w ")]
        [InlineData("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8")]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq        ")]
        [InlineData("8/8/8/8/8/8/8/8      ")]
        [InlineData("     ")]
        [InlineData(" ")]
        public void TooShort(string fen)
        {
            Assert.False(FenUtils.IsValid(fen), "FEN string cannot be that short.");
        }

        [Theory]
        [InlineData(" rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
        [InlineData("  rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
        [InlineData("   rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
        [InlineData(" 8/8/8/8/8/8/8/8 w KQkq - 0 1")]
        [InlineData("      r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - ")]
        [InlineData("  8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 1 ")]
        [InlineData("    r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1")]
        [InlineData("       rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8  ")]
        public void LeadingSpaces(string fen)
        {
            Assert.False(FenUtils.IsValid(fen), "FEN string must not contain any leading spaces.");
        }
    }
}
