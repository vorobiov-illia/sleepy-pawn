using SleepyPawn.Core.Chess;

namespace SleepyPawn.Tests.MoveRules
{
    public class CastlingRules
    {
        //================== WHITE CASTLING TESTS:
        //----- Legal cases:
        [Fact]
        public void WhiteShortLegalCastling()
        {
            // Initializing game
            Game game = new Game(true);

            // White rook
            game.AddPiece("h1wr");
            // White king
            game.AddPiece("e1wk");

            // White player makes short castling
            bool isLegal = game.TryUciMove("e1g1");
            Assert.True(isLegal, "White player should be able to perform short castling when possible.");
        }
        [Fact]
        public void WhiteLongLegalCastling()
        {
            // Initializing game
            Game game = new Game(true);

            // White rook
            game.AddPiece("a1wr");
            // White king
            game.AddPiece("e1wk");

            // White player makes long castling
            bool isLegal = game.TryUciMove("e1c1");
            Assert.True(isLegal, "White player should be able to perform long castling when possible.");
        }
        [Fact]
        public void WhiteShortFakeThreat()
        {
            // Initializing game
            Game game = new Game(true);

            // White rook
            game.AddPiece("h1wr");
            // White king
            game.AddPiece("e1wk");
            // Black rook
            game.AddPiece("h8br");

            // White player makes short castling
            bool isLegal = game.TryUciMove("e1g1");
            Assert.True(isLegal, "White player should be able to perform short castling, even when white rook is under attack.");
        }
        [Fact]
        public void WhiteLongFakeThreat()
        {
            // Initializing game
            Game game = new Game(true);

            // White rook
            game.AddPiece("a1wr");
            // White king
            game.AddPiece("e1wk");
            // Black rook
            game.AddPiece("a8br");

            // White player makes long castling
            bool isLegal = game.TryUciMove("e1c1");
            Assert.True(isLegal, "White player should be able to perform long castling, even when white rook is under attack.");
        }
        [Fact]
        public void WhiteLongAnotherFakeThreat()
        {
            // Initializing game
            Game game = new Game(true);

            // White rook
            game.AddPiece("a1wr");
            // White king
            game.AddPiece("e1wk");
            // Black rook
            game.AddPiece("b8br");

            // White player makes long castling
            bool isLegal = game.TryUciMove("e1c1");
            Assert.True(isLegal, "White player should be able to perform long castling, even when b1 tile is under attack.");
        }
        //----- Illegal cases:
        [Fact]
        public void WhiteShortCastlingKingMoved()
        {
            // Initializing game
            Game game = new Game(true);

            // White rook
            game.AddPiece("h1wr");
            // White king
            game.AddPiece("e1wk");

            // White player moves king
            game.TryUciMove("e1e2");
            // Skipping black move
            game.TryUciMove("0000");
            // White player moves king back
            game.TryUciMove("e2e1");
            // Skipping black move
            game.TryUciMove("0000");
            // White player makes short castling
            bool isLegal = game.TryUciMove("e1g1");
            Assert.False(isLegal, "White player must not be able to perform short castling after king has been moved.");
        }
        [Fact]
        public void WhiteLongCastlingKingMoved()
        {
            // Initializing game
            Game game = new Game(true);

            // White rook
            game.AddPiece("a1wr");
            // White king
            game.AddPiece("e1wk");

            // White player moves king
            game.TryUciMove("e1e2");
            // Skipping black move
            game.TryUciMove("0000");
            // White player moves king back
            game.TryUciMove("e2e1");
            // Skipping black move
            game.TryUciMove("0000");
            // White player makes long castling
            bool isLegal = game.TryUciMove("e1c1");
            Assert.False(isLegal, "White player must not be able to perform long castling after king has been moved.");
        }
        [Fact]
        public void WhiteShortCastlingRookMoved()
        {
            // Initializing game
            Game game = new Game(true);

            // White rook
            game.AddPiece("h1wr");
            // White king
            game.AddPiece("e1wk");

            // White player moves rook
            game.TryUciMove("h1h2");
            // Skipping black move
            game.TryUciMove("0000");
            // White player moves rook back
            game.TryUciMove("h2h1");
            // Skipping black move
            game.TryUciMove("0000");
            // White player makes short castling
            bool isLegal = game.TryUciMove("e1g1");
            Assert.False(isLegal, "White player must not be able to perform short castling after rook has been moved.");
        }
        [Fact]
        public void WhiteLongCastlingRookMoved()
        {
            // Initializing game
            Game game = new Game(true);

            // White rook
            game.AddPiece("a1wr");
            // White king
            game.AddPiece("e1wk");

            // White player moves rook
            game.TryUciMove("a1a2");
            // Skipping black move
            game.TryUciMove("0000");
            // White player moves rook back
            game.TryUciMove("a2a1");
            // Skipping black move
            game.TryUciMove("0000");
            // White player makes long castling
            bool isLegal = game.TryUciMove("e1c1");
            Assert.False(isLegal, "White player must not be able to perform long castling after rook has been moved.");
        }
        [Theory]
        [InlineData("f1wb")]
        [InlineData("f1bb")]
        [InlineData("g1wb")]
        [InlineData("g1bb")]
        public void WhiteShortCastlingBlocked(string blockPiece)
        {
            // Initializing game
            Game game = new Game(true);

            // White rook
            game.AddPiece("h1wr");
            // White king
            game.AddPiece("e1wk");
            // Block
            game.AddPiece(blockPiece);

            // White player makes short castling
            bool isLegal = game.TryUciMove("e1g1");
            Assert.False(isLegal, "White player must not be able to perform short castling while a piece blocks the way.");
        }
        [Theory]
        [InlineData("d1wb")]
        [InlineData("d1bb")]
        [InlineData("c1wb")]
        [InlineData("c1bb")]
        [InlineData("b1wb")]
        [InlineData("b1bb")]
        public void WhiteLongCastlingBlocked(string blockPiece)
        {
            // Initializing game
            Game game = new Game(true);

            // White rook
            game.AddPiece("a1wr");
            // White king
            game.AddPiece("e1wk");
            // Block
            game.AddPiece(blockPiece);

            // White player makes long castling
            bool isLegal = game.TryUciMove("e1c1");
            Assert.False(isLegal, "White player must not be able to perform long castling while a piece blocks the way.");
        }
        [Theory]
        [InlineData("g3bb")]
        [InlineData("h3bb")]
        [InlineData("h2bb")]
        public void WhiteShortCastlingUnderThreat(string threatPiece)
        {
            // Initializing game
            Game game = new Game(true);

            // White rook
            game.AddPiece("h1wr");
            // White king
            game.AddPiece("e1wk");
            // Threat
            game.AddPiece(threatPiece);

            // White player makes short castling
            bool isLegal = game.TryUciMove("e1g1");
            Assert.False(isLegal, "White player must not be able to perform short castling while the way of king is attacked by enemy pieces.");
        }
        [Theory]
        [InlineData("g3bb")]
        [InlineData("f3bb")]
        [InlineData("e3bb")]
        public void WhiteLongCastlingUnderThreat(string threatPiece)
        {
            // Initializing game
            Game game = new Game(true);

            // White rook
            game.AddPiece("a1wr");
            // White king
            game.AddPiece("e1wk");
            // Threat
            game.AddPiece(threatPiece);

            // White player makes long castling
            bool isLegal = game.TryUciMove("e1c1");
            Assert.False(isLegal, "White player must not be able to perform long castling while the way of king is attacked by enemy pieces.");
        }
        [Fact]
        public void WhiteShortNoRook()
        {
            // Initializing game
            Game game = new Game(true);

            // White king
            game.AddPiece("e1wk");

            // White player makes short castling
            bool isLegal = game.TryUciMove("e1g1");
            Assert.False(isLegal, "White player must not be able to perform short castling when there is no rook.");
        }
        [Fact]
        public void WhiteLongNoRook()
        {
            // Initializing game
            Game game = new Game(true);

            // White king
            game.AddPiece("e1wk");

            // White player makes long castling
            bool isLegal = game.TryUciMove("e1c1");
            Assert.False(isLegal, "White player must not be able to perform long castling when there is no rook.");
        }
        [Theory]
        [InlineData("h1wp")]
        [InlineData("h1wb")]
        [InlineData("h1wn")]
        [InlineData("h1wq")]
        [InlineData("h1wk")]
        [InlineData("h1bp")]
        [InlineData("h1bb")]
        [InlineData("h1bn")]
        [InlineData("h1br")]
        [InlineData("h1bq")]
        [InlineData("h1bk")]
        public void WhiteShortWrongPiece(string wrongPiece)
        {
            // Initializing game
            Game game = new Game(true);

            // White king
            game.AddPiece("e1wk");
            // Wrong piece
            game.AddPiece(wrongPiece);

            // White player makes short castling
            bool isLegal = game.TryUciMove("e1g1");
            Assert.False(isLegal, "White player must not be able to perform short castling with piece that is not a white rook.");
        }
        [Theory]
        [InlineData("a1wp")]
        [InlineData("a1wb")]
        [InlineData("a1wn")]
        [InlineData("a1wq")]
        [InlineData("a1wk")]
        [InlineData("a1bp")]
        [InlineData("a1bb")]
        [InlineData("a1bn")]
        [InlineData("a1br")]
        [InlineData("a1bq")]
        [InlineData("a1bk")]
        public void WhiteLongWrongPiece(string wrongPiece)
        {
            // Initializing game
            Game game = new Game(true);

            // White king
            game.AddPiece("e1wk");
            // Wrong piece
            game.AddPiece(wrongPiece);

            // White player makes long castling
            bool isLegal = game.TryUciMove("e1c1");
            Assert.False(isLegal, "White player must not be able to perform long castling with piece that is not a white rook.");
        }
        //================== BLACK CASTLING TESTS:
        //----- Legal cases:
        [Fact]
        public void BlackShortLegalCastling()
        {
            // Initializing game
            Game game = new Game(true);

            // Black rook
            game.AddPiece("h8br");
            // Black king
            game.AddPiece("e8bk");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes short castling
            bool isLegal = game.TryUciMove("e8g8");
            Assert.True(isLegal, "Black player should be able to perform short castling when possible.");
        }
        [Fact]
        public void BlackLongLegalCastling()
        {
            // Initializing game
            Game game = new Game(true);

            // Black rook
            game.AddPiece("a8br");
            // Black king
            game.AddPiece("e8bk");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes long castling
            bool isLegal = game.TryUciMove("e8c8");
            Assert.True(isLegal, "Black player should be able to perform long castling when possible.");
        }
        [Fact]
        public void BlackShortFakeThreat()
        {
            // Initializing game
            Game game = new Game(true);

            // Black rook
            game.AddPiece("h8br");
            // Black king
            game.AddPiece("e8bk");
            // White rook
            game.AddPiece("h1wr");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes short castling
            bool isLegal = game.TryUciMove("e8g8");
            Assert.True(isLegal, "Black player should be able to perform short castling, even when black rook is under attack.");
        }
        [Fact]
        public void BlackLongFakeThreat()
        {
            // Initializing game
            Game game = new Game(true);

            // Black rook
            game.AddPiece("a8br");
            // Black king
            game.AddPiece("e8bk");
            // White rook
            game.AddPiece("a1wr");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes long castling
            bool isLegal = game.TryUciMove("e8c8");
            Assert.True(isLegal, "Black player should be able to perform long castling, even when black rook is under attack.");
        }
        [Fact]
        public void BlackLongAnotherFakeThreat()
        {
            // Initializing game
            Game game = new Game(true);

            // Black rook
            game.AddPiece("a8br");
            // Black king
            game.AddPiece("e8bk");
            // White rook
            game.AddPiece("b1wr");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes long castling
            bool isLegal = game.TryUciMove("e8c8");
            Assert.True(isLegal, "Black player should be able to perform long castling, even when b8 tile is under attack.");
        }
        //----- Illegal cases:
        [Fact]
        public void BlackShortCastlingKingMoved()
        {
            // Initializing game
            Game game = new Game(true);

            // Black rook
            game.AddPiece("h8br");
            // Black king
            game.AddPiece("e8bk");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player moves king
            game.TryUciMove("e8e7");
            // Skipping white move
            game.TryUciMove("0000");
            // Black player moves king back
            game.TryUciMove("e7e8");
            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes short castling
            bool isLegal = game.TryUciMove("e8g8");
            Assert.False(isLegal, "Black player must not be able to perform short castling after king has been moved.");
        }
        [Fact]
        public void BlackLongCastlingKingMoved()
        {
            // Initializing game
            Game game = new Game(true);

            // Black rook
            game.AddPiece("a8br");
            // Black king
            game.AddPiece("e8bk");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player moves king
            game.TryUciMove("e8e7");
            // Skipping white move
            game.TryUciMove("0000");
            // Black player moves king back
            game.TryUciMove("e7e8");
            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes long castling
            bool isLegal = game.TryUciMove("e8c8");
            Assert.False(isLegal, "Black player must not be able to perform long castling after king has been moved.");
        }
        [Fact]
        public void BlackShortCastlingRookMoved()
        {
            // Initializing game
            Game game = new Game(true);

            // Black rook
            game.AddPiece("h8br");
            // Black king
            game.AddPiece("e8bk");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player moves rook
            game.TryUciMove("h8h7");
            // Skipping white move
            game.TryUciMove("0000");
            // Black player moves rook back
            game.TryUciMove("h7h8");
            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes short castling
            bool isLegal = game.TryUciMove("e8g8");
            Assert.False(isLegal, "Black player must not be able to perform short castling after rook has been moved.");
        }
        [Fact]
        public void BlackLongCastlingRookMoved()
        {
            // Initializing game
            Game game = new Game(true);

            // Black rook
            game.AddPiece("a8br");
            // Black king
            game.AddPiece("e8bk");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player moves rook
            game.TryUciMove("a8a7");
            // Skipping white move
            game.TryUciMove("0000");
            // Black player moves rook back
            game.TryUciMove("a7a8");
            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes long castling
            bool isLegal = game.TryUciMove("e8c8");
            Assert.False(isLegal, "Black player must not be able to perform long castling after rook has been moved.");
        }
        [Theory]
        [InlineData("f8wb")]
        [InlineData("f8bb")]
        [InlineData("g8wb")]
        [InlineData("g8bb")]
        public void BlackShortCastlingBlocked(string blockPiece)
        {
            // Initializing game
            Game game = new Game(true);

            // Black rook
            game.AddPiece("h8br");
            // Black king
            game.AddPiece("e8bk");
            // Block
            game.AddPiece(blockPiece);

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes short castling
            bool isLegal = game.TryUciMove("e8g8");
            Assert.False(isLegal, "Black player must not be able to perform short castling while a piece blocks the way.");
        }
        [Theory]
        [InlineData("d8wb")]
        [InlineData("d8bb")]
        [InlineData("c8wb")]
        [InlineData("c8bb")]
        [InlineData("b8wb")]
        [InlineData("b8bb")]
        public void BlackLongCastlingBlocked(string blockPiece)
        {
            // Initializing game
            Game game = new Game(true);

            // Black rook
            game.AddPiece("a8br");
            // Black king
            game.AddPiece("e8bk");
            // Block
            game.AddPiece(blockPiece);

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes long castling
            bool isLegal = game.TryUciMove("e8c8");
            Assert.False(isLegal, "Black player must not be able to perform long castling while a piece blocks the way.");
        }
        [Theory]
        [InlineData("g6wb")]
        [InlineData("h6wb")]
        [InlineData("h7wb")]
        public void BlackShortCastlingUnderThreat(string threatPiece)
        {
            // Initializing game
            Game game = new Game(true);

            // Black rook
            game.AddPiece("h8br");
            // Black king
            game.AddPiece("e8bk");
            // Threat
            game.AddPiece(threatPiece);

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes short castling
            bool isLegal = game.TryUciMove("e8g8");
            Assert.False(isLegal, "Black player must not be able to perform short castling while the way of king is attacked by enemy pieces.");
        }
        [Theory]
        [InlineData("g6wb")]
        [InlineData("f6wb")]
        [InlineData("e6wb")]
        public void BlackLongCastlingUnderThreat(string threatPiece)
        {
            // Initializing game
            Game game = new Game(true);

            // Black rook
            game.AddPiece("a8br");
            // Black king
            game.AddPiece("e8bk");
            // Threat
            game.AddPiece(threatPiece);

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes long castling
            bool isLegal = game.TryUciMove("e8c8");
            Assert.False(isLegal, "Black player must not be able to perform long castling while the way of king is attacked by enemy pieces.");
        }
        [Fact]
        public void BlackShortNoRook()
        {
            // Initializing game
            Game game = new Game(true);

            // Black king
            game.AddPiece("e8bk");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes short castling
            bool isLegal = game.TryUciMove("e8g8");
            Assert.False(isLegal, "Black player must not be able to perform short castling when there is no rook.");
        }
        [Fact]
        public void BlackLongNoRook()
        {
            // Initializing game
            Game game = new Game(true);

            // Black king
            game.AddPiece("e8bk");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes long castling
            bool isLegal = game.TryUciMove("e8c8");
            Assert.False(isLegal, "Black player must not be able to perform long castling when there is no rook.");
        }
        [Theory]
        [InlineData("h8wp")]
        [InlineData("h8wb")]
        [InlineData("h8wn")]
        [InlineData("h8wr")]
        [InlineData("h8wq")]
        [InlineData("h8wk")]
        [InlineData("h8bp")]
        [InlineData("h8bb")]
        [InlineData("h8bn")]
        [InlineData("h8bq")]
        [InlineData("h8bk")]
        public void BlackShortWrongPiece(string wrongPiece)
        {
            // Initializing game
            Game game = new Game(true);

            // Black king
            game.AddPiece("e8bk");
            // Wrong piece
            game.AddPiece(wrongPiece);

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes short castling
            bool isLegal = game.TryUciMove("e8g8");
            Assert.False(isLegal, "Black player must not be able to perform short castling with piece that is not a black rook.");
        }
        [Theory]
        [InlineData("a8wp")]
        [InlineData("a8wb")]
        [InlineData("a8wn")]
        [InlineData("a8wr")]
        [InlineData("a8wq")]
        [InlineData("a8wk")]
        [InlineData("a8bp")]
        [InlineData("a8bb")]
        [InlineData("a8bn")]
        [InlineData("a8bq")]
        [InlineData("a8bk")]
        public void BlackLongWrongPiece(string wrongPiece)
        {
            // Initializing game
            Game game = new Game(true);

            // Black king
            game.AddPiece("e8bk");
            // Wrong piece
            game.AddPiece(wrongPiece);

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes long castling
            bool isLegal = game.TryUciMove("e8c8");
            Assert.False(isLegal, "Black player must not be able to perform long castling with piece that is not a black rook.");
        }
    }
}
