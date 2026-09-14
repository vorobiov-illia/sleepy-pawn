using SleepyPawn.Core.Chess;

namespace SleepyPawn.Tests.MoveRules
{
    public class BishopRules
    {        
        //================== BISHOP TESTS:
        //----- Simple moves:
        [Theory]
        [InlineData(true, "e5wb", "e5f6")]
        [InlineData(true, "e5wb", "e5g7")]
        [InlineData(true, "e5wb", "e5h8")]
        [InlineData(true, "e5wb", "e5d4")]
        [InlineData(true, "e5wb", "e5c3")]
        [InlineData(true, "e5wb", "e5b2")]
        [InlineData(true, "e5wb", "e5a1")]
        [InlineData(true, "e5wb", "e5f4")]
        [InlineData(true, "e5wb", "e5g3")]
        [InlineData(true, "e5wb", "e5h2")]
        [InlineData(true, "e5wb", "e5d6")]
        [InlineData(true, "e5wb", "e5c7")]
        [InlineData(true, "e5wb", "e5b8")]
        [InlineData(false, "e5bb", "e5f6")]
        [InlineData(false, "e5bb", "e5g7")]
        [InlineData(false, "e5bb", "e5h8")]
        [InlineData(false, "e5bb", "e5d4")]
        [InlineData(false, "e5bb", "e5c3")]
        [InlineData(false, "e5bb", "e5b2")]
        [InlineData(false, "e5bb", "e5a1")]
        [InlineData(false, "e5bb", "e5f4")]
        [InlineData(false, "e5bb", "e5g3")]
        [InlineData(false, "e5bb", "e5h2")]
        [InlineData(false, "e5bb", "e5d6")]
        [InlineData(false, "e5bb", "e5c7")]
        [InlineData(false, "e5bb", "e5b8")]
        public void BishopOpenMove(bool white, string bishop, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Bishop
            game.AddPiece(bishop);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes legal move
            bool isLegal = game.TryUciMove(move);
            Assert.True(isLegal, "Bishop should be able to perform diagonal moves on open lines.");
        }
        [Theory]
        [InlineData(true, "e5wb", "e5e6")]
        [InlineData(true, "e5wb", "e5e7")]
        [InlineData(true, "e5wb", "e5e8")]
        [InlineData(true, "e5wb", "e5e4")]
        [InlineData(true, "e5wb", "e5e3")]
        [InlineData(true, "e5wb", "e5e2")]
        [InlineData(true, "e5wb", "e5e1")]
        [InlineData(true, "e5wb", "e5a5")]
        [InlineData(true, "e5wb", "e5b5")]
        [InlineData(true, "e5wb", "e5c5")]
        [InlineData(true, "e5wb", "e5d5")]
        [InlineData(true, "e5wb", "e5f5")]
        [InlineData(true, "e5wb", "e5g5")]
        [InlineData(true, "e5wb", "e5h5")]
        [InlineData(false, "e5bb", "e5e6")]
        [InlineData(false, "e5bb", "e5e7")]
        [InlineData(false, "e5bb", "e5e8")]
        [InlineData(false, "e5bb", "e5e4")]
        [InlineData(false, "e5bb", "e5e3")]
        [InlineData(false, "e5bb", "e5e2")]
        [InlineData(false, "e5bb", "e5e1")]
        [InlineData(false, "e5bb", "e5a5")]
        [InlineData(false, "e5bb", "e5b5")]
        [InlineData(false, "e5bb", "e5c5")]
        [InlineData(false, "e5bb", "e5d5")]
        [InlineData(false, "e5bb", "e5f5")]
        [InlineData(false, "e5bb", "e5g5")]
        [InlineData(false, "e5bb", "e5h5")]
        public void BishopStraightMove(bool white, string bishop, string illegalMove)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Bishop
            game.AddPiece(bishop);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(illegalMove);
            Assert.False(isLegal, "Bishop must not be able to move horizontally or vertically.");
        }
        [Theory]
        [InlineData(true, "e5wb", "e5f7")]
        [InlineData(true, "e5wb", "e5g6")]
        [InlineData(true, "e5wb", "e5d7")]
        [InlineData(true, "e5wb", "e5c6")]
        [InlineData(true, "e5wb", "e5d3")]
        [InlineData(true, "e5wb", "e5c4")]
        [InlineData(true, "e5wb", "e5f3")]
        [InlineData(true, "e5wb", "e5g4")]
        [InlineData(false, "e5bb", "e5f7")]
        [InlineData(false, "e5bb", "e5g6")]
        [InlineData(false, "e5bb", "e5d7")]
        [InlineData(false, "e5bb", "e5c6")]
        [InlineData(false, "e5bb", "e5d3")]
        [InlineData(false, "e5bb", "e5c4")]
        [InlineData(false, "e5bb", "e5f3")]
        [InlineData(false, "e5bb", "e5g4")]
        public void BishopLMove(bool white, string bishop, string illegalMove)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Bishop
            game.AddPiece(bishop);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(illegalMove);
            Assert.False(isLegal, "Bishop must not be able to perform L-shaped moves.");
        }
        [Theory]
        [InlineData(true, "e5wb", "f6wp", "e5g7")]
        [InlineData(true, "e5wb", "f6bp", "e5g7")]
        [InlineData(true, "e5wb", "d6wp", "e5c7")]
        [InlineData(true, "e5wb", "d6bp", "e5c7")]
        [InlineData(true, "e5wb", "f4wp", "e5g3")]
        [InlineData(true, "e5wb", "f4bp", "e5g3")]
        [InlineData(true, "e5wb", "d4wp", "e5c3")]
        [InlineData(true, "e5wb", "d4bp", "e5c3")]
        [InlineData(false, "e5bb", "f6wp", "e5g7")]
        [InlineData(false, "e5bb", "f6bp", "e5g7")]
        [InlineData(false, "e5bb", "d6wp", "e5c7")]
        [InlineData(false, "e5bb", "d6bp", "e5c7")]
        [InlineData(false, "e5bb", "f4wp", "e5g3")]
        [InlineData(false, "e5bb", "f4bp", "e5g3")]
        [InlineData(false, "e5bb", "d4wp", "e5c3")]
        [InlineData(false, "e5bb", "d4bp", "e5c3")]
        public void BishopClosedMove(bool white, string bishop, string blockPiece, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Bishop
            game.AddPiece(bishop);
            // Pawn blocks the way
            game.AddPiece(blockPiece);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(move);

            Assert.False(isLegal, "Bishop must not be able to jump over pieces.");
        }
        [Theory]
        [InlineData(true, "a7wb", "a7z8")]
        [InlineData(true, "a7wb", "a7z6")]
        [InlineData(true, "h2wb", "h2i3")]
        [InlineData(true, "h2wb", "h2i1")]
        [InlineData(false, "a7bb", "a7z8")]
        [InlineData(false, "a7bb", "a7z6")]
        [InlineData(false, "h2bb", "h2i3")]
        [InlineData(false, "h2bb", "h2i1")]
        public void BishopOutOfBoundsMove(bool white, string bishop, string illegalMove)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Bishop
            game.AddPiece(bishop);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(illegalMove);
            Assert.False(isLegal, "Bishop must not be able to move out of game board.");
        }
        //----- Attacks:
        [Theory]
        [InlineData(true, "e5wb", "g7bp", "e5g7")]
        [InlineData(true, "e5wb", "g3bp", "e5g3")]
        [InlineData(true, "e5wb", "c7bp", "e5c7")]
        [InlineData(true, "e5wb", "c3bp", "e5c3")]
        [InlineData(false, "e5bb", "g7wp", "e5g7")]
        [InlineData(false, "e5bb", "g3wp", "e5g3")]
        [InlineData(false, "e5bb", "c7wp", "e5c7")]
        [InlineData(false, "e5bb", "c3wp", "e5c3")]
        public void BishopTakesOpponentPiece(bool white, string bishop, string captureTarget, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Bishop
            game.AddPiece(bishop);
            // Opponent pawn to take
            game.AddPiece(captureTarget);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player takes opponent pawn
            bool isLegal = game.TryUciMove(move);

            Assert.True(isLegal, "Bishop should be able to take Opponent pieces.");
        }
        [Theory]
        [InlineData(true, "e5wb", "g7wp", "e5g7")]
        [InlineData(true, "e5wb", "g3wp", "e5g3")]
        [InlineData(true, "e5wb", "c7wp", "e5c7")]
        [InlineData(true, "e5wb", "c3wp", "e5c3")]
        [InlineData(false, "e5bb", "g7bp", "e5g7")]
        [InlineData(false, "e5bb", "g3bp", "e5g3")]
        [InlineData(false, "e5bb", "c7bp", "e5c7")]
        [InlineData(false, "e5bb", "c3bp", "e5c3")]
        public void BishopTakesAllyPiece(bool white, string bishop, string captureTarget, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Bishop
            game.AddPiece(bishop);
            // Ally pawn to take
            game.AddPiece(captureTarget);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player takes ally pawn
            bool isLegal = game.TryUciMove(move);

            Assert.False(isLegal, "Bishop must not be able to take Ally pieces.");
        }
    }
}