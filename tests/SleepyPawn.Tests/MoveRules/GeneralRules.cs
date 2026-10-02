using SleepyPawn.Core.Chess;

namespace SleepyPawn.Tests.MoveRules
{
    public class GeneralRules
    {
        [Fact]
        public void WhitePlayerMovesBlackPiece()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Black pawn
            game.AddPiece("e7bp");

            // White player makes legal move
            bool isLegal = game.TryLanMove("e7e6");
            Assert.False(isLegal, "A player must not be able to move the other player's pieces.");
        }
        [Fact]
        public void BlackPlayerMovesWhitePiece()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // White pawn
            game.AddPiece("e7wp");

            // Skip white move
            game.TryLanMove("0000");
            // Black player makes legal move
            bool isLegal = game.TryLanMove("e7e6");
            Assert.False(isLegal, "A player must not be able to move the other player's pieces.");
        }
        [Fact]
        public void PlayerMovesEmptySquare()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // White player makes illegal move
            bool isLegal = game.TryLanMove("e2e4");
            Assert.False(isLegal, "A player must not be able to make moves with empty square as piece.");
        }
        [Theory]
        [InlineData(true, "e5wp", "e5e5")]
        [InlineData(true, "e5wb", "e5e5")]
        [InlineData(true, "e5wn", "e5e5")]
        [InlineData(true, "e5wr", "e5e5")]
        [InlineData(true, "e5wq", "e5e5")]
        [InlineData(true, "e5wk", "e5e5")]
        [InlineData(false, "e5bp", "e5e5")]
        [InlineData(false, "e5bb", "e5e5")]
        [InlineData(false, "e5bn", "e5e5")]
        [InlineData(false, "e5br", "e5e5")]
        [InlineData(false, "e5bq", "e5e5")]
        [InlineData(false, "e5bk", "e5e5")]
        public void PlayerIsNotMoving(bool white, string piece, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);
            
            game.AddPiece(piece);

            if (!white)
            {
                // Skip white move
                game.TryLanMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryLanMove(move);
            Assert.False(isLegal, "A player must not be able to skip move by not changing piece position.");
        }
    }
}
