using SleepyPawn.Core.Chess;

namespace SleepyPawn.Tests.MoveRules
{
    public class KingRules
    {        
        //================== KING TESTS:
        //----- Simple moves:
        [Theory]
        [InlineData(true, "e5wk", "e5d6")]
        [InlineData(true, "e5wk", "e5e6")]
        [InlineData(true, "e5wk", "e5f6")]
        [InlineData(true, "e5wk", "e5d5")]
        [InlineData(true, "e5wk", "e5f5")]
        [InlineData(true, "e5wk", "e5d4")]
        [InlineData(true, "e5wk", "e5e4")]
        [InlineData(true, "e5wk", "e5f4")]
        [InlineData(false, "e5bk", "e5d6")]
        [InlineData(false, "e5bk", "e5e6")]
        [InlineData(false, "e5bk", "e5f6")]
        [InlineData(false, "e5bk", "e5d5")]
        [InlineData(false, "e5bk", "e5f5")]
        [InlineData(false, "e5bk", "e5d4")]
        [InlineData(false, "e5bk", "e5e4")]
        [InlineData(false, "e5bk", "e5f4")]
        public void KingOpenMove(bool white, string king, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // King
            game.AddPiece(king);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes legal move
            bool isLegal = game.TryUciMove(move);
            Assert.True(isLegal, "King should be able to perform move on near empty tiles.");
        }
        [Theory]
        [InlineData(true, "e5wk", "e5g5")]
        [InlineData(true, "e5wk", "e5c5")]
        [InlineData(true, "e5wk", "e5e7")]
        [InlineData(true, "e5wk", "e5e3")]
        [InlineData(true, "e5wk", "e5g7")]
        [InlineData(true, "e5wk", "e5c7")]
        [InlineData(true, "e5wk", "e5g3")]
        [InlineData(true, "e5wk", "e5c3")]
        [InlineData(true, "e5wk", "e5g6")]
        [InlineData(true, "e5wk", "e5f7")]
        [InlineData(true, "e5wk", "e5c6")]
        [InlineData(true, "e5wk", "e5d7")]
        [InlineData(true, "e5wk", "e5c4")]
        [InlineData(true, "e5wk", "e5d3")]
        [InlineData(true, "e5wk", "e5g4")]
        [InlineData(true, "e5wk", "e5f3")]
        [InlineData(false, "e5bk", "e5g5")]
        [InlineData(false, "e5bk", "e5c5")]
        [InlineData(false, "e5bk", "e5e7")]
        [InlineData(false, "e5bk", "e5e3")]
        [InlineData(false, "e5bk", "e5g7")]
        [InlineData(false, "e5bk", "e5c7")]
        [InlineData(false, "e5bk", "e5g3")]
        [InlineData(false, "e5bk", "e5c3")]
        [InlineData(false, "e5bk", "e5g6")]
        [InlineData(false, "e5bk", "e5f7")]
        [InlineData(false, "e5bk", "e5c6")]
        [InlineData(false, "e5bk", "e5d7")]
        [InlineData(false, "e5bk", "e5c4")]
        [InlineData(false, "e5bk", "e5d3")]
        [InlineData(false, "e5bk", "e5g4")]
        [InlineData(false, "e5bk", "e5f3")]
        public void KingIllegalMove(bool white, string king, string illegalMove)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // King
            game.AddPiece(king);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(illegalMove);
            Assert.False(isLegal, "King must not be able to move that far.");
        }
        [Theory]
        [InlineData(true, "a5wk", "a5z6")]
        [InlineData(true, "a5wk", "a5z5")]
        [InlineData(true, "a5wk", "a5z4")]
        [InlineData(true, "h5wk", "h5i6")]
        [InlineData(true, "h5wk", "h5i5")]
        [InlineData(true, "h5wk", "h5i4")]
        [InlineData(false, "a5bk", "a5z6")]
        [InlineData(false, "a5bk", "a5z5")]
        [InlineData(false, "a5bk", "a5z4")]
        [InlineData(false, "h5bk", "h5i6")]
        [InlineData(false, "h5bk", "h5i5")]
        [InlineData(false, "h5bk", "h5i4")]
        public void KingOutOfBoundsMove(bool white, string king, string illegalMove)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // King
            game.AddPiece(king);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(illegalMove);
            Assert.False(isLegal, "King must not be able to move out of game board.");
        }
        //----- Attacks:
        [Theory]
        [InlineData(true, "e5wk", "d6bp", "e5d6")]
        [InlineData(true, "e5wk", "e6bp", "e5e6")]
        [InlineData(true, "e5wk", "f6bp", "e5f6")]
        [InlineData(true, "e5wk", "d5bp", "e5d5")]
        [InlineData(true, "e5wk", "f5bp", "e5f5")]
        [InlineData(true, "e5wk", "d4bp", "e5d4")]
        [InlineData(true, "e5wk", "e4bp", "e5e4")]
        [InlineData(true, "e5wk", "f4bp", "e5f4")]
        [InlineData(false, "e5bk", "d6wp", "e5d6")]
        [InlineData(false, "e5bk", "e6wp", "e5e6")]
        [InlineData(false, "e5bk", "f6wp", "e5f6")]
        [InlineData(false, "e5bk", "d5wp", "e5d5")]
        [InlineData(false, "e5bk", "f5wp", "e5f5")]
        [InlineData(false, "e5bk", "d4wp", "e5d4")]
        [InlineData(false, "e5bk", "e4wp", "e5e4")]
        [InlineData(false, "e5bk", "f4wp", "e5f4")]
        public void KingTakesOpponentPiece(bool white, string king, string captureTarget, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // King
            game.AddPiece(king);
            // Opponent pawn to take
            game.AddPiece(captureTarget);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player takes opponent pawn
            bool isLegal = game.TryUciMove(move);

            Assert.True(isLegal, "King should be able to take Opponent pieces.");
        }
        [Theory]
        [InlineData(true, "e5wk", "d6wp", "e5d6")]
        [InlineData(true, "e5wk", "e6wp", "e5e6")]
        [InlineData(true, "e5wk", "f6wp", "e5f6")]
        [InlineData(true, "e5wk", "d5wp", "e5d5")]
        [InlineData(true, "e5wk", "f5wp", "e5f5")]
        [InlineData(true, "e5wk", "d4wp", "e5d4")]
        [InlineData(true, "e5wk", "e4wp", "e5e4")]
        [InlineData(true, "e5wk", "f4wp", "e5f4")]
        [InlineData(false, "e5bk", "d6bp", "e5d6")]
        [InlineData(false, "e5bk", "e6bp", "e5e6")]
        [InlineData(false, "e5bk", "f6bp", "e5f6")]
        [InlineData(false, "e5bk", "d5bp", "e5d5")]
        [InlineData(false, "e5bk", "f5bp", "e5f5")]
        [InlineData(false, "e5bk", "d4bp", "e5d4")]
        [InlineData(false, "e5bk", "e4bp", "e5e4")]
        [InlineData(false, "e5bk", "f4bp", "e5f4")]
        public void KingTakesAllyPiece(bool white, string king, string captureTarget, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // King
            game.AddPiece(king);
            // Ally pawn to take
            game.AddPiece(captureTarget);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player takes ally pawn
            bool isLegal = game.TryUciMove(move);

            Assert.False(isLegal, "King must not be able to take Ally pieces.");
        }
    }
}