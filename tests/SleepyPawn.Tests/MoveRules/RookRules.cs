using SleepyPawn.Core.Chess;

namespace SleepyPawn.Tests.Rules
{
    public class RookRules
    {        
        //================== ROOK TESTS:
        //----- Simple moves:
        [Theory]
        [InlineData(true, "e5wr", "e5e6")]
        [InlineData(true, "e5wr", "e5e7")]
        [InlineData(true, "e5wr", "e5e8")]
        [InlineData(true, "e5wr", "e5e4")]
        [InlineData(true, "e5wr", "e5e3")]
        [InlineData(true, "e5wr", "e5e2")]
        [InlineData(true, "e5wr", "e5e1")]
        [InlineData(true, "e5wr", "e5a5")]
        [InlineData(true, "e5wr", "e5b5")]
        [InlineData(true, "e5wr", "e5c5")]
        [InlineData(true, "e5wr", "e5d5")]
        [InlineData(true, "e5wr", "e5f5")]
        [InlineData(true, "e5wr", "e5g5")]
        [InlineData(true, "e5wr", "e5h5")]
        [InlineData(false, "e5br", "e5e6")]
        [InlineData(false, "e5br", "e5e7")]
        [InlineData(false, "e5br", "e5e8")]
        [InlineData(false, "e5br", "e5e4")]
        [InlineData(false, "e5br", "e5e3")]
        [InlineData(false, "e5br", "e5e2")]
        [InlineData(false, "e5br", "e5e1")]
        [InlineData(false, "e5br", "e5a5")]
        [InlineData(false, "e5br", "e5b5")]
        [InlineData(false, "e5br", "e5c5")]
        [InlineData(false, "e5br", "e5d5")]
        [InlineData(false, "e5br", "e5f5")]
        [InlineData(false, "e5br", "e5g5")]
        [InlineData(false, "e5br", "e5h5")]
        public void RookOpenMove(bool white, string rook, string move)
        {
            // Initializing game
            Game game = new Game(true);

            // Rook
            game.AddPiece(rook);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes legal move
            bool isLegal = game.TryUciMove(move);
            Assert.True(isLegal, "Rook should be able to perform move on free straight line.");
        }
        [Theory]
        [InlineData(true, "e5wr", "e5f6")]
        [InlineData(true, "e5wr", "e5g7")]
        [InlineData(true, "e5wr", "e5h8")]
        [InlineData(true, "e5wr", "e5d4")]
        [InlineData(true, "e5wr", "e5c3")]
        [InlineData(true, "e5wr", "e5b2")]
        [InlineData(true, "e5wr", "e5a1")]
        [InlineData(true, "e5wr", "e5f4")]
        [InlineData(true, "e5wr", "e5g3")]
        [InlineData(true, "e5wr", "e5h2")]
        [InlineData(true, "e5wr", "e5d6")]
        [InlineData(true, "e5wr", "e5c7")]
        [InlineData(true, "e5wr", "e5b8")]
        [InlineData(false, "e5br", "e5f6")]
        [InlineData(false, "e5br", "e5g7")]
        [InlineData(false, "e5br", "e5h8")]
        [InlineData(false, "e5br", "e5d4")]
        [InlineData(false, "e5br", "e5c3")]
        [InlineData(false, "e5br", "e5b2")]
        [InlineData(false, "e5br", "e5a1")]
        [InlineData(false, "e5br", "e5f4")]
        [InlineData(false, "e5br", "e5g3")]
        [InlineData(false, "e5br", "e5h2")]
        [InlineData(false, "e5br", "e5d6")]
        [InlineData(false, "e5br", "e5c7")]
        [InlineData(false, "e5br", "e5b8")]
        public void RookDiagonalMove(bool white, string rook, string illegalMove)
        {
            // Initializing game
            Game game = new Game(true);

            // Rook
            game.AddPiece(rook);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(illegalMove);
            Assert.False(isLegal, "Rook must not be able to move diagonally.");
        }
        [Theory]
        [InlineData(true, "e5wr", "e5f7")]
        [InlineData(true, "e5wr", "e5g6")]
        [InlineData(true, "e5wr", "e5d7")]
        [InlineData(true, "e5wr", "e5c6")]
        [InlineData(true, "e5wr", "e5d3")]
        [InlineData(true, "e5wr", "e5c4")]
        [InlineData(true, "e5wr", "e5f3")]
        [InlineData(true, "e5wr", "e5g4")]
        [InlineData(false, "e5br", "e5f7")]
        [InlineData(false, "e5br", "e5g6")]
        [InlineData(false, "e5br", "e5d7")]
        [InlineData(false, "e5br", "e5c6")]
        [InlineData(false, "e5br", "e5d3")]
        [InlineData(false, "e5br", "e5c4")]
        [InlineData(false, "e5br", "e5f3")]
        [InlineData(false, "e5br", "e5g4")]
        public void RookLMove(bool white, string rook, string illegalMove)
        {
            // Initializing game
            Game game = new Game(true);

            // Rook
            game.AddPiece(rook);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(illegalMove);
            Assert.False(isLegal, "Rook must not be able to perform L-shaped moves.");
        }
        [Theory]
        [InlineData(true, "e5wr", "e6wp", "e5e7")]
        [InlineData(true, "e5wr", "e6bp", "e5e7")]
        [InlineData(true, "e5wr", "f5wp", "e5g5")]
        [InlineData(true, "e5wr", "f5bp", "e5g5")]
        [InlineData(true, "e5wr", "e4wp", "e5e3")]
        [InlineData(true, "e5wr", "e4bp", "e5e3")]
        [InlineData(true, "e5wr", "d5wp", "e5c5")]
        [InlineData(true, "e5wr", "d5bp", "e5c5")]
        [InlineData(false, "e5br", "e6wp", "e5e7")]
        [InlineData(false, "e5br", "e6bp", "e5e7")]
        [InlineData(false, "e5br", "f5wp", "e5g5")]
        [InlineData(false, "e5br", "f5bp", "e5g5")]
        [InlineData(false, "e5br", "e4wp", "e5e3")]
        [InlineData(false, "e5br", "e4bp", "e5e3")]
        [InlineData(false, "e5br", "d5wp", "e5c5")]
        [InlineData(false, "e5br", "d5bp", "e5c5")]
        public void RookClosedMove(bool white, string rook, string blockPiece, string move)
        {
            // Initializing game
            Game game = new Game(true);

            // Rook
            game.AddPiece(rook);
            // Pawn blocks the way
            game.AddPiece(blockPiece);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(move);

            Assert.False(isLegal, "Rook must not be able to jump over pieces.");
        }
        [Theory]
        [InlineData(true, "a8wr", "a8z8")]
        [InlineData(true, "a8wr", "a8a9")]
        [InlineData(true, "h1wr", "h1i1")]
        [InlineData(true, "h1wr", "h1h0")]
        [InlineData(false, "a8br", "a8z8")]
        [InlineData(false, "a8br", "a8a9")]
        [InlineData(false, "h1br", "h1i1")]
        [InlineData(false, "h1br", "h1h0")]
        public void RookOutOfBoundsMove(bool white, string rook, string illegalMove)
        {
            // Initializing game
            Game game = new Game(true);

            // Rook
            game.AddPiece(rook);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(illegalMove);
            Assert.False(isLegal, "Rook must not be able to move out of game board.");
        }
        //----- Attacks:
        [Theory]
        [InlineData(true, "e5wr", "e8bp", "e5e8")]
        [InlineData(true, "e5wr", "h5bp", "e5h5")]
        [InlineData(true, "e5wr", "a5bp", "e5a5")]
        [InlineData(true, "e5wr", "e1bp", "e5e1")]
        [InlineData(false, "e5br", "e8wp", "e5e8")]
        [InlineData(false, "e5br", "h5wp", "e5h5")]
        [InlineData(false, "e5br", "a5wp", "e5a5")]
        [InlineData(false, "e5br", "e1wp", "e5e1")]
        public void RookTakesOpponentPiece(bool white, string rook, string captureTarget, string move)
        {
            // Initializing game
            Game game = new Game(true);

            // Rook
            game.AddPiece(rook);
            // Opponent pawn to take
            game.AddPiece(captureTarget);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player takes opponent pawn
            bool isLegal = game.TryUciMove(move);

            Assert.True(isLegal, "Rook should be able to take Opponent pieces.");
        }
        [Theory]
        [InlineData(true, "e5wr", "e8wp", "e5e8")]
        [InlineData(true, "e5wr", "h5wp", "e5h5")]
        [InlineData(true, "e5wr", "a5wp", "e5a5")]
        [InlineData(true, "e5wr", "e1wp", "e5e1")]
        [InlineData(false, "e5br", "e8bp", "e5e8")]
        [InlineData(false, "e5br", "h5bp", "e5h5")]
        [InlineData(false, "e5br", "a5bp", "e5a5")]
        [InlineData(false, "e5br", "e1bp", "e5e1")]
        public void RookTakesAllyPiece(bool white, string rook, string captureTarget, string move)
        {
            // Initializing game
            Game game = new Game(true);

            // Rook
            game.AddPiece(rook);
            // Ally pawn to take
            game.AddPiece(captureTarget);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player takes ally pawn
            bool isLegal = game.TryUciMove(move);

            Assert.False(isLegal, "Rook must not be able to take Ally pieces.");
        }
    }
}