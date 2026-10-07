using SleepyPawn.Core.Chess;

namespace SleepyPawn.Tests.MoveRules
{
    public class PawnRules
    {        
        //================== WHITE PAWN TESTS:
        //----- Simple moves:
        [Fact]
        public void WhitePawnOpenMove()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // White pawn
            game.AddPiece("e2wp");

            // White player makes legal move
            bool isLegal = game.TryLanMove("e2e3");
            Assert.True(isLegal, "Pawn should be able to move forward on empty tile.");
        }
        [Fact]
        public void WhitePawnClosedMove()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // White pawn
            game.AddPiece("e2wp");
            // Black pawn blocks the way
            game.AddPiece("e3bp");

            // White player makes illegal move
            bool isLegal = game.TryLanMove("e2e3");
            Assert.False(isLegal, "Pawn must not be able to move forward on occupied tile.");
        }
        [Fact]
        public void WhitePawnMoveBack()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // White pawn
            game.AddPiece("e2wp");

            // White player makes illegal move
            bool isLegal = game.TryLanMove("e2e1");
            Assert.False(isLegal, "Pawn must not be able to move backwards.");
        }
        [Fact]
        public void WhiteLongOpenMove()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // White pawn
            game.AddPiece("e2wp");

            // White player makes illegal move
            bool isLegal = game.TryLanMove("e2e5");
            Assert.False(isLegal, "Pawn must not be able to move forward that far.");
        }
        [Theory]
        [InlineData("e2d2")]
        [InlineData("e2f2")]
        public void WhitePawnHorizontalMove(string illegalMove)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // White pawn
            game.AddPiece("e2wp");

            // White player makes illegal move
            bool isLegal = game.TryLanMove(illegalMove);
            Assert.False(isLegal, "Pawn must not be able to move horizontally.");
        }
        [Theory]
        [InlineData("e8wp", "e8e9")]
        [InlineData("h2wp", "h2i3")]
        [InlineData("a2wp", "a2z3")]
        public void WhitePawnOutOfBounds(string pawn, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // White pawn
            game.AddPiece(pawn);

            // White player makes illegal move
            bool isLegal = game.TryLanMove(move);
            Assert.False(isLegal, "Pawn must not be able to move out of game board.");
        }
        //----- Double moves:
        [Fact]
        public void WhitePawnDoubleOpenMove()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // White pawn
            game.AddPiece("e2wp");

            // White player makes legal move
            bool isLegal = game.TryLanMove("e2e4");
            Assert.True(isLegal, "Pawn should be able to perform double forward move one time.");
        }
        [Fact]
        public void WhitePawnDoubleOpenMoveBlocked()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // White pawn
            game.AddPiece("e2wp");
            // Black pawn blocks the way
            game.AddPiece("e3bp");

            // White player makes illegal move
            bool isLegal = game.TryLanMove("e2e4");
            Assert.False(isLegal, "Pawn must not be able to perform double forward move if piece blocks the way.");
        }
        [Fact]
        public void WhitePawnDoubleClosedMove()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // White pawn
            game.AddPiece("e2wp");
            // Black pawn occupies the tile
            game.AddPiece("e4bp");

            // White player makes illegal move
            bool isLegal = game.TryLanMove("e2e4");
            Assert.False(isLegal, "Pawn must not be able to perform double forward move on occupied tile.");
        }
        [Fact]
        public void WhitePawnDoubleIllegalMove()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // White pawn
            game.AddPiece("e2wp");

            // White player makes legal move
            game.TryLanMove("e2e3");
            // Skipping black move
            game.TryLanMove("0000");
            // White player makes illegal move
            bool isLegal = game.TryLanMove("e3e5");
            Assert.False(isLegal, "Pawn must not be able to perform double forward move after moving.");
        }
        //----- Attacks:
        [Theory]
        [InlineData("f3bp", "e2f3")]
        [InlineData("d3bp", "e2d3")]
        public void WhitePawnTakesBlack(string captureTarget, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // White pawn
            game.AddPiece("e2wp");
            // Black pawn to take
            game.AddPiece(captureTarget);

            // White player takes Black pawn
            bool isLegal = game.TryLanMove(move);

            Assert.True(isLegal, "White Pawn should be able to take Black pieces diagonally.");
        }
        [Theory]
        [InlineData("f1bp", "e2f1")]
        [InlineData("d1bp", "e2d1")]
        public void WhitePawnTakesBlackBack(string captureTarget, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // White pawn
            game.AddPiece("e2wp");
            // Black pawn to take
            game.AddPiece(captureTarget);

            // White player takes Black pawn
            bool isLegal = game.TryLanMove(move);

            Assert.False(isLegal, "White Pawn must not be able to take Black pieces diagonally from back.");
        }
        [Theory]
        [InlineData("f3wp", "e2f3")]
        [InlineData("d3wp", "e2d3")]
        public void WhitePawnTakesWhite(string captureTarget, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // White pawn
            game.AddPiece("e2wp");
            // White pawn to take
            game.AddPiece(captureTarget);

            // White player takes White pawn
            bool isLegal = game.TryLanMove(move);

            Assert.False(isLegal, "White Pawn must not be able to take White pieces.");
        }
        [Theory]
        [InlineData("e2f3")]
        [InlineData("e2d3")]
        public void WhitePawnTakesNothing(string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // White pawn
            game.AddPiece("e2wp");

            // White player takes nothing
            bool isLegal = game.TryLanMove(move);

            Assert.False(isLegal, "White Pawn must not be able to move diagonally without piece to capture.");
        }
        //================== BLACK PAWN TESTS:
        //----- Simple moves:
        [Fact]
        public void BlackPawnOpenMove()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Black pawn
            game.AddPiece("e7bp");

            // Skipping white move
            game.TryLanMove("0000");
            // Black player makes legal move
            bool isLegal = game.TryLanMove("e7e6");
            Assert.True(isLegal, "Pawn should be able to move forward on empty tile.");
        }
        [Fact]
        public void BlackPawnClosedMove()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Black pawn
            game.AddPiece("e7bp");
            // White pawn blocks the way
            game.AddPiece("e6wp");

            // Skipping white move
            game.TryLanMove("0000");
            // Black player makes illegal move
            bool isLegal = game.TryLanMove("e7e6");
            Assert.False(isLegal, "Pawn must not be able to move forward on occupied tile.");
        }
        [Fact]
        public void BlackPawnMoveBack()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Black pawn
            game.AddPiece("e7bp");

            // Skipping white move
            game.TryLanMove("0000");
            // Black player makes illegal move
            bool isLegal = game.TryLanMove("e7e8");
            Assert.False(isLegal, "Pawn must not be able to move backwards.");
        }
        [Fact]
        public void BlackLongOpenMove()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Black pawn
            game.AddPiece("e7bp");

            // Skipping white move
            game.TryLanMove("0000");
            // Black player makes illegal move
            bool isLegal = game.TryLanMove("e7e4");
            Assert.False(isLegal, "Pawn must not be able to move forward that far.");
        }
        [Theory]
        [InlineData("e7d7")]
        [InlineData("e7f7")]
        public void BlackPawnHorizontalMove(string illegalMove)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Black pawn
            game.AddPiece("e7bp");

            // Skipping white move
            game.TryLanMove("0000");
            // Black player makes illegal move
            bool isLegal = game.TryLanMove(illegalMove);
            Assert.False(isLegal, "Pawn must not be able to move horizontally.");
        }
        //----- Double moves:
        [Fact]
        public void BlackPawnDoubleOpenMove()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Black pawn
            game.AddPiece("e7bp");

            // Skipping white move
            game.TryLanMove("0000");
            // Black player makes legal move
            bool isLegal = game.TryLanMove("e7e5");
            Assert.True(isLegal, "Pawn should be able to perform double forward move one time.");
        }
        [Fact]
        public void BlackPawnDoubleOpenMoveBlocked()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Black pawn
            game.AddPiece("e7bp");
            // White pawn blocks the way
            game.AddPiece("e6wp");

            // Skipping white move
            game.TryLanMove("0000");
            // Black player makes illegal move
            bool isLegal = game.TryLanMove("e7e5");
            Assert.False(isLegal, "Pawn must not be able to perform double forward move if piece blocks the way.");
        }
        [Fact]
        public void BlackPawnDoubleClosedMove()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Black pawn
            game.AddPiece("e7bp");
            // White pawn occupies the tile
            game.AddPiece("e5wp");

            // Skipping white move
            game.TryLanMove("0000");
            // Black player makes illegal move
            bool isLegal = game.TryLanMove("e7e5");
            Assert.False(isLegal, "Pawn must not be able to perform double forward move on occupied tile.");
        }
        [Fact]
        public void BlackPawnDoubleIllegalMove()
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Black pawn
            game.AddPiece("e7bp");

            // Skipping white move
            game.TryLanMove("0000");
            // Black player makes legal move
            game.TryLanMove("e7e6");
            // Skipping white move
            game.TryLanMove("0000");
            // Black player makes illegal move
            bool isLegal = game.TryLanMove("e6e4");
            Assert.False(isLegal, "Pawn must not be able to perform double forward move after moving.");
        }
        //----- Attacks:
        [Theory]
        [InlineData("f6wp", "e7f6")]
        [InlineData("d6wp", "e7d6")]
        public void BlackPawnTakesWhite(string captureTarget, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Black pawn
            game.AddPiece("e7bp");
            // White pawn to take
            game.AddPiece(captureTarget);

            // Skipping white move
            game.TryLanMove("0000");
            // Black player takes White pawn
            bool isLegal = game.TryLanMove(move);

            Assert.True(isLegal, "Black Pawn should be able to take White pieces diagonally.");
        }
        [Theory]
        [InlineData("f8wp", "e7f8")]
        [InlineData("d8wp", "e7d8")]
        public void BlackPawnTakesWhiteBack(string captureTarget, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Black pawn
            game.AddPiece("e7bp");
            // White pawn to take
            game.AddPiece(captureTarget);

            // Skipping white move
            game.TryLanMove("0000");
            // Black player takes White pawn
            bool isLegal = game.TryLanMove(move);

            Assert.False(isLegal, "Black Pawn must not be able to take White pieces diagonally from back.");
        }
        [Theory]
        [InlineData("f6bp", "e7f6")]
        [InlineData("d6bp", "e7d6")]
        public void BlackPawnTakesBlack(string captureTarget, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Black pawn
            game.AddPiece("e7bp");
            // Black pawn to take
            game.AddPiece(captureTarget);

            // Skipping white move
            game.TryLanMove("0000");
            // Black player takes black pawn
            bool isLegal = game.TryLanMove(move);

            Assert.False(isLegal, "Black Pawn must not be able to take Black pieces.");
        }
        [Theory]
        [InlineData("e7f6")]
        [InlineData("e7d6")]
        public void BlackPawnTakesNothing(string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Black pawn
            game.AddPiece("e7bp");

            // Skipping white move
            game.TryLanMove("0000");
            // Black player takes nothing
            bool isLegal = game.TryLanMove(move);

            Assert.False(isLegal, "Black Pawn must not be able to move diagonally without piece to capture.");
        }
    }
}