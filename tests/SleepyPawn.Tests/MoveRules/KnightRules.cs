using SleepyPawn.Core.Chess;

namespace SleepyPawn.Tests.Rules
{
    public class KnightRules
    {        
        //================== KNIGHT TESTS:
        //----- Simple moves:
        [Theory]
        [InlineData(true, "e5wn", "e5f7")]
        [InlineData(true, "e5wn", "e5g6")]
        [InlineData(true, "e5wn", "e5g4")]
        [InlineData(true, "e5wn", "e5f3")]
        [InlineData(true, "e5wn", "e5d7")]
        [InlineData(true, "e5wn", "e5c6")]
        [InlineData(true, "e5wn", "e5d3")]
        [InlineData(true, "e5wn", "e5c4")]
        [InlineData(false, "e5bn", "e5f7")]
        [InlineData(false, "e5bn", "e5g6")]
        [InlineData(false, "e5bn", "e5g4")]
        [InlineData(false, "e5bn", "e5f3")]
        [InlineData(false, "e5bn", "e5d7")]
        [InlineData(false, "e5bn", "e5c6")]
        [InlineData(false, "e5bn", "e5d3")]
        [InlineData(false, "e5bn", "e5c4")]
        public void KnightOpenMove(bool white, string knight, string move)
        {
            // Initializing game
            Game game = new Game(true);

            // Knight
            game.AddPiece(knight);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes legal move
            bool isLegal = game.TryUciMove(move);
            Assert.True(isLegal, "Knight should be able to perform L-shaped move on empty tile.");
        }
        [Theory]
        [InlineData(true, "e5wn", "e5f7", "e6wp")]
        [InlineData(true, "e5wn", "e5f7", "f6wp")]
        [InlineData(true, "e5wn", "e5g6", "f6wp")]
        [InlineData(true, "e5wn", "e5g6", "f5wp")]
        [InlineData(true, "e5wn", "e5g4", "f5wp")]
        [InlineData(true, "e5wn", "e5g4", "f4wp")]
        [InlineData(true, "e5wn", "e5f3", "f4wp")]
        [InlineData(true, "e5wn", "e5f3", "e4wp")]
        [InlineData(true, "e5wn", "e5d7", "e6wp")]
        [InlineData(true, "e5wn", "e5d7", "d6wp")]
        [InlineData(true, "e5wn", "e5c6", "d6wp")]
        [InlineData(true, "e5wn", "e5c6", "d5wp")]
        [InlineData(true, "e5wn", "e5d3", "e4wp")]
        [InlineData(true, "e5wn", "e5d3", "d4wp")]
        [InlineData(true, "e5wn", "e5c4", "d4wp")]
        [InlineData(true, "e5wn", "e5c4", "d5wp")]
        [InlineData(true, "e5wn", "e5f7", "e6bp")]
        [InlineData(true, "e5wn", "e5f7", "f6bp")]
        [InlineData(true, "e5wn", "e5g6", "f6bp")]
        [InlineData(true, "e5wn", "e5g6", "f5bp")]
        [InlineData(true, "e5wn", "e5g4", "f5bp")]
        [InlineData(true, "e5wn", "e5g4", "f4bp")]
        [InlineData(true, "e5wn", "e5f3", "f4bp")]
        [InlineData(true, "e5wn", "e5f3", "e4bp")]
        [InlineData(true, "e5wn", "e5d7", "e6bp")]
        [InlineData(true, "e5wn", "e5d7", "d6bp")]
        [InlineData(true, "e5wn", "e5c6", "d6bp")]
        [InlineData(true, "e5wn", "e5c6", "d5bp")]
        [InlineData(true, "e5wn", "e5d3", "e4bp")]
        [InlineData(true, "e5wn", "e5d3", "d4bp")]
        [InlineData(true, "e5wn", "e5c4", "d4bp")]
        [InlineData(true, "e5wn", "e5c4", "d5bp")]
        [InlineData(false, "e5bn", "e5f7", "e6wp")]
        [InlineData(false, "e5bn", "e5f7", "f6wp")]
        [InlineData(false, "e5bn", "e5g6", "f6wp")]
        [InlineData(false, "e5bn", "e5g6", "f5wp")]
        [InlineData(false, "e5bn", "e5g4", "f5wp")]
        [InlineData(false, "e5bn", "e5g4", "f4wp")]
        [InlineData(false, "e5bn", "e5f3", "f4wp")]
        [InlineData(false, "e5bn", "e5f3", "e4wp")]
        [InlineData(false, "e5bn", "e5d7", "e6wp")]
        [InlineData(false, "e5bn", "e5d7", "d6wp")]
        [InlineData(false, "e5bn", "e5c6", "d6wp")]
        [InlineData(false, "e5bn", "e5c6", "d5wp")]
        [InlineData(false, "e5bn", "e5d3", "e4wp")]
        [InlineData(false, "e5bn", "e5d3", "d4wp")]
        [InlineData(false, "e5bn", "e5c4", "d4wp")]
        [InlineData(false, "e5bn", "e5c4", "d5wp")]
        [InlineData(false, "e5bn", "e5f7", "e6bp")]
        [InlineData(false, "e5bn", "e5f7", "f6bp")]
        [InlineData(false, "e5bn", "e5g6", "f6bp")]
        [InlineData(false, "e5bn", "e5g6", "f5bp")]
        [InlineData(false, "e5bn", "e5g4", "f5bp")]
        [InlineData(false, "e5bn", "e5g4", "f4bp")]
        [InlineData(false, "e5bn", "e5f3", "f4bp")]
        [InlineData(false, "e5bn", "e5f3", "e4bp")]
        [InlineData(false, "e5bn", "e5d7", "e6bp")]
        [InlineData(false, "e5bn", "e5d7", "d6bp")]
        [InlineData(false, "e5bn", "e5c6", "d6bp")]
        [InlineData(false, "e5bn", "e5c6", "d5bp")]
        [InlineData(false, "e5bn", "e5d3", "e4bp")]
        [InlineData(false, "e5bn", "e5d3", "d4bp")]
        [InlineData(false, "e5bn", "e5c4", "d4bp")]
        [InlineData(false, "e5bn", "e5c4", "d5bp")]
        public void KnightBlockedMove(bool white, string knight, string move, string blockPiece)
        {
            // Initializing game
            Game game = new Game(true);

            // Knight
            game.AddPiece(knight);
            // Piece blocks the way
            game.AddPiece(blockPiece);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes legal move
            bool isLegal = game.TryUciMove(move);
            Assert.True(isLegal, "Knight should be able to perform L-shaped move over other pieces.");
        }
        [Theory]
        [InlineData(true, "e5wn", "e5f5")]
        [InlineData(true, "e5wn", "e5g5")]
        [InlineData(true, "e5wn", "e5h5")]
        [InlineData(true, "e5wn", "e5d5")]
        [InlineData(true, "e5wn", "e5c5")]
        [InlineData(true, "e5wn", "e5b5")]
        [InlineData(false, "e5bn", "e5f5")]
        [InlineData(false, "e5bn", "e5g5")]
        [InlineData(false, "e5bn", "e5h5")]
        [InlineData(false, "e5bn", "e5d5")]
        [InlineData(false, "e5bn", "e5c5")]
        [InlineData(false, "e5bn", "e5b5")]
        public void KnightHorizontalMove(bool white, string knight, string illegalMove)
        {
            // Initializing game
            Game game = new Game(true);

            // Knight
            game.AddPiece(knight);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(illegalMove);
            Assert.False(isLegal, "Knight must not be able to move horizontally.");
        }
        [Theory]
        [InlineData(true, "e5wn", "e5e6")]
        [InlineData(true, "e5wn", "e5e7")]
        [InlineData(true, "e5wn", "e5e8")]
        [InlineData(true, "e5wn", "e5e4")]
        [InlineData(true, "e5wn", "e5e3")]
        [InlineData(true, "e5wn", "e5e2")]
        [InlineData(false, "e5bn", "e5e6")]
        [InlineData(false, "e5bn", "e5e7")]
        [InlineData(false, "e5bn", "e5e8")]
        [InlineData(false, "e5bn", "e5e4")]
        [InlineData(false, "e5bn", "e5e3")]
        [InlineData(false, "e5bn", "e5e2")]
        public void KnightVerticalMove(bool white, string knight, string illegalMove)
        {
            // Initializing game
            Game game = new Game(true);

            // Knight
            game.AddPiece(knight);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(illegalMove);
            Assert.False(isLegal, "Knight must not be able to move vertically.");
        }
        [Theory]
        [InlineData(true, "e5wn", "e5f6")]
        [InlineData(true, "e5wn", "e5g7")]
        [InlineData(true, "e5wn", "e5h8")]
        [InlineData(true, "e5wn", "e5d4")]
        [InlineData(true, "e5wn", "e5c3")]
        [InlineData(true, "e5wn", "e5b2")]
        [InlineData(true, "e5wn", "e5d6")]
        [InlineData(true, "e5wn", "e5c7")]
        [InlineData(true, "e5wn", "e5b8")]
        [InlineData(true, "e5wn", "e5f4")]
        [InlineData(true, "e5wn", "e5g3")]
        [InlineData(true, "e5wn", "e5h2")]
        [InlineData(false, "e5bn", "e5f6")]
        [InlineData(false, "e5bn", "e5g7")]
        [InlineData(false, "e5bn", "e5h8")]
        [InlineData(false, "e5bn", "e5d4")]
        [InlineData(false, "e5bn", "e5c3")]
        [InlineData(false, "e5bn", "e5b2")]
        [InlineData(false, "e5bn", "e5d6")]
        [InlineData(false, "e5bn", "e5c7")]
        [InlineData(false, "e5bn", "e5b8")]
        [InlineData(false, "e5bn", "e5f4")]
        [InlineData(false, "e5bn", "e5g3")]
        [InlineData(false, "e5bn", "e5h2")]
        public void KnightDiagonalMove(bool white, string knight, string illegalMove)
        {
            // Initializing game
            Game game = new Game(true);

            // Knight
            game.AddPiece(knight);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(illegalMove);
            Assert.False(isLegal, "Knight must not be able to move diagonally.");
        }
        [Theory]
        [InlineData(true, "a5wn", "a5z7")]
        [InlineData(true, "a5wn", "a5x6")]
        [InlineData(true, "a5wn", "a5z3")]
        [InlineData(true, "a5wn", "a5x4")]
        [InlineData(true, "h5wn", "h5i7")]
        [InlineData(true, "h5wn", "h5j6")]
        [InlineData(true, "h5wn", "h5i3")]
        [InlineData(true, "h5wn", "h5j4")]
        [InlineData(false, "a5bn", "a5z7")]
        [InlineData(false, "a5bn", "a5x6")]
        [InlineData(false, "a5bn", "a5z3")]
        [InlineData(false, "a5bn", "a5x4")]
        [InlineData(false, "h5bn", "h5i7")]
        [InlineData(false, "h5bn", "h5j6")]
        [InlineData(false, "h5bn", "h5i3")]
        [InlineData(false, "h5bn", "h5j4")]
        public void KnightOutOfBoundsMove(bool white, string knight, string illegalMove)
        {
            // Initializing game
            Game game = new Game(true);

            // Knight
            game.AddPiece(knight);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(illegalMove);
            Assert.False(isLegal, "Knight must not be able to move out of game board.");
        }
        //----- Attacks:
        [Theory]
        [InlineData(true, "e5wn", "f7bp", "e5f7")]
        [InlineData(true, "e5wn", "g6bp", "e5g6")]
        [InlineData(true, "e5wn", "g4bp", "e5g4")]
        [InlineData(true, "e5wn", "f3bp", "e5f3")]
        [InlineData(true, "e5wn", "d7bp", "e5d7")]
        [InlineData(true, "e5wn", "c6bp", "e5c6")]
        [InlineData(true, "e5wn", "d3bp", "e5d3")]
        [InlineData(true, "e5wn", "c4bp", "e5c4")]
        [InlineData(false, "e5bn", "f7wp", "e5f7")]
        [InlineData(false, "e5bn", "g6wp", "e5g6")]
        [InlineData(false, "e5bn", "g4wp", "e5g4")]
        [InlineData(false, "e5bn", "f3wp", "e5f3")]
        [InlineData(false, "e5bn", "d7wp", "e5d7")]
        [InlineData(false, "e5bn", "c6wp", "e5c6")]
        [InlineData(false, "e5bn", "d3wp", "e5d3")]
        [InlineData(false, "e5bn", "c4wp", "e5c4")]
        public void KnightTakesOpponentPiece(bool white, string knight, string captureTarget, string move)
        {
            // Initializing game
            Game game = new Game(true);

            // Knight
            game.AddPiece(knight);
            // Opponent pawn to take
            game.AddPiece(captureTarget);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player takes opponent pawn
            bool isLegal = game.TryUciMove(move);

            Assert.True(isLegal, "Knight should be able to take Opponent pieces.");
        }
        [Theory]
        [InlineData(true, "e5wn", "f7wp", "e5f7")]
        [InlineData(true, "e5wn", "g6wp", "e5g6")]
        [InlineData(true, "e5wn", "g4wp", "e5g4")]
        [InlineData(true, "e5wn", "f3wp", "e5f3")]
        [InlineData(true, "e5wn", "d7wp", "e5d7")]
        [InlineData(true, "e5wn", "c6wp", "e5c6")]
        [InlineData(true, "e5wn", "d3wp", "e5d3")]
        [InlineData(true, "e5wn", "c4wp", "e5c4")]
        [InlineData(false, "e5bn", "f7bp", "e5f7")]
        [InlineData(false, "e5bn", "g6bp", "e5g6")]
        [InlineData(false, "e5bn", "g4bp", "e5g4")]
        [InlineData(false, "e5bn", "f3bp", "e5f3")]
        [InlineData(false, "e5bn", "d7bp", "e5d7")]
        [InlineData(false, "e5bn", "c6bp", "e5c6")]
        [InlineData(false, "e5bn", "d3bp", "e5d3")]
        [InlineData(false, "e5bn", "c4bp", "e5c4")]
        public void KnightTakesAllyPiece(bool white, string knight, string captureTarget, string move)
        {
            // Initializing game
            Game game = new Game(true);

            // Knight
            game.AddPiece(knight);
            // Ally pawn to take
            game.AddPiece(captureTarget);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player takes ally pawn
            bool isLegal = game.TryUciMove(move);

            Assert.False(isLegal, "Knight must not be able to take Ally pieces.");
        }
    }
}