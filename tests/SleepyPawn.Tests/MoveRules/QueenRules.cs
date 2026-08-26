using SleepyPawn.Core.Chess;

namespace SleepyPawn.Tests.Rules
{
    public class QueenRules
    {        
        //================== QUEEN TESTS:
        //----- Simple moves:
        [Theory]
        [InlineData(true, "e5wq", "e5f6")]
        [InlineData(true, "e5wq", "e5g7")]
        [InlineData(true, "e5wq", "e5h8")]
        [InlineData(true, "e5wq", "e5d4")]
        [InlineData(true, "e5wq", "e5c3")]
        [InlineData(true, "e5wq", "e5b2")]
        [InlineData(true, "e5wq", "e5a1")]
        [InlineData(true, "e5wq", "e5f4")]
        [InlineData(true, "e5wq", "e5g3")]
        [InlineData(true, "e5wq", "e5h2")]
        [InlineData(true, "e5wq", "e5d6")]
        [InlineData(true, "e5wq", "e5c7")]
        [InlineData(true, "e5wq", "e5b8")]
        [InlineData(true, "e5wq", "e5e6")]
        [InlineData(true, "e5wq", "e5e7")]
        [InlineData(true, "e5wq", "e5e8")]
        [InlineData(true, "e5wq", "e5e4")]
        [InlineData(true, "e5wq", "e5e3")]
        [InlineData(true, "e5wq", "e5e2")]
        [InlineData(true, "e5wq", "e5e1")]
        [InlineData(true, "e5wq", "e5a5")]
        [InlineData(true, "e5wq", "e5b5")]
        [InlineData(true, "e5wq", "e5c5")]
        [InlineData(true, "e5wq", "e5d5")]
        [InlineData(true, "e5wq", "e5f5")]
        [InlineData(true, "e5wq", "e5g5")]
        [InlineData(true, "e5wq", "e5h5")]
        [InlineData(false, "e5bq", "e5f6")]
        [InlineData(false, "e5bq", "e5g7")]
        [InlineData(false, "e5bq", "e5h8")]
        [InlineData(false, "e5bq", "e5d4")]
        [InlineData(false, "e5bq", "e5c3")]
        [InlineData(false, "e5bq", "e5b2")]
        [InlineData(false, "e5bq", "e5a1")]
        [InlineData(false, "e5bq", "e5f4")]
        [InlineData(false, "e5bq", "e5g3")]
        [InlineData(false, "e5bq", "e5h2")]
        [InlineData(false, "e5bq", "e5d6")]
        [InlineData(false, "e5bq", "e5c7")]
        [InlineData(false, "e5bq", "e5b8")]
        [InlineData(false, "e5bq", "e5e6")]
        [InlineData(false, "e5bq", "e5e7")]
        [InlineData(false, "e5bq", "e5e8")]
        [InlineData(false, "e5bq", "e5e4")]
        [InlineData(false, "e5bq", "e5e3")]
        [InlineData(false, "e5bq", "e5e2")]
        [InlineData(false, "e5bq", "e5e1")]
        [InlineData(false, "e5bq", "e5a5")]
        [InlineData(false, "e5bq", "e5b5")]
        [InlineData(false, "e5bq", "e5c5")]
        [InlineData(false, "e5bq", "e5d5")]
        [InlineData(false, "e5bq", "e5f5")]
        [InlineData(false, "e5bq", "e5g5")]
        [InlineData(false, "e5bq", "e5h5")]
        public void QueenOpenMove(bool white, string queen, string move)
        {
            // Initializing game
            Game game = new Game(true);

            // Queen
            game.AddPiece(queen);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes legal move
            bool isLegal = game.TryUciMove(move);
            Assert.True(isLegal, "Queen should be able to perform diagonal, horizontal and vertical moves on open lines.");
        }
        [Theory]
        [InlineData(true, "e5wq", "e5f7")]
        [InlineData(true, "e5wq", "e5g6")]
        [InlineData(true, "e5wq", "e5d7")]
        [InlineData(true, "e5wq", "e5c6")]
        [InlineData(true, "e5wq", "e5d3")]
        [InlineData(true, "e5wq", "e5c4")]
        [InlineData(true, "e5wq", "e5f3")]
        [InlineData(true, "e5wq", "e5g4")]
        [InlineData(false, "e5bq", "e5f7")]
        [InlineData(false, "e5bq", "e5g6")]
        [InlineData(false, "e5bq", "e5d7")]
        [InlineData(false, "e5bq", "e5c6")]
        [InlineData(false, "e5bq", "e5d3")]
        [InlineData(false, "e5bq", "e5c4")]
        [InlineData(false, "e5bq", "e5f3")]
        [InlineData(false, "e5bq", "e5g4")]
        public void QueenLMove(bool white, string queen, string illegalMove)
        {
            // Initializing game
            Game game = new Game(true);

            // Queen
            game.AddPiece(queen);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(illegalMove);
            Assert.False(isLegal, "Queen must not be able to perform L-shaped moves.");
        }
        [Theory]
        [InlineData(true, "e5wq", "f6wp", "e5g7")]
        [InlineData(true, "e5wq", "f6bp", "e5g7")]
        [InlineData(true, "e5wq", "d6wp", "e5c7")]
        [InlineData(true, "e5wq", "d6bp", "e5c7")]
        [InlineData(true, "e5wq", "f4wp", "e5g3")]
        [InlineData(true, "e5wq", "f4bp", "e5g3")]
        [InlineData(true, "e5wq", "d4wp", "e5c3")]
        [InlineData(true, "e5wq", "d4bp", "e5c3")]
        [InlineData(true, "e5wq", "e6wp", "e5e7")]
        [InlineData(true, "e5wq", "e6bp", "e5e7")]
        [InlineData(true, "e5wq", "f5wp", "e5g5")]
        [InlineData(true, "e5wq", "f5bp", "e5g5")]
        [InlineData(true, "e5wq", "e4wp", "e5e3")]
        [InlineData(true, "e5wq", "e4bp", "e5e3")]
        [InlineData(true, "e5wq", "d5wp", "e5c5")]
        [InlineData(true, "e5wq", "d5bp", "e5c5")]
        [InlineData(false, "e5bq", "f6wp", "e5g7")]
        [InlineData(false, "e5bq", "f6bp", "e5g7")]
        [InlineData(false, "e5bq", "d6wp", "e5c7")]
        [InlineData(false, "e5bq", "d6bp", "e5c7")]
        [InlineData(false, "e5bq", "f4wp", "e5g3")]
        [InlineData(false, "e5bq", "f4bp", "e5g3")]
        [InlineData(false, "e5bq", "d4wp", "e5c3")]
        [InlineData(false, "e5bq", "d4bp", "e5c3")]
        [InlineData(false, "e5bq", "e6wp", "e5e7")]
        [InlineData(false, "e5bq", "e6bp", "e5e7")]
        [InlineData(false, "e5bq", "f5wp", "e5g5")]
        [InlineData(false, "e5bq", "f5bp", "e5g5")]
        [InlineData(false, "e5bq", "e4wp", "e5e3")]
        [InlineData(false, "e5bq", "e4bp", "e5e3")]
        [InlineData(false, "e5bq", "d5wp", "e5c5")]
        [InlineData(false, "e5bq", "d5bp", "e5c5")]
        public void QueenClosedMove(bool white, string queen, string blockPiece, string move)
        {
            // Initializing game
            Game game = new Game(true);

            // Queen
            game.AddPiece(queen);
            // Pawn blocks the way
            game.AddPiece(blockPiece);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(move);

            Assert.False(isLegal, "Queen must not be able to jump over pieces.");
        }
        [Theory]
        [InlineData(true, "e5wq", "e5e9")]
        [InlineData(true, "e5wq", "e5i9")]
        [InlineData(true, "e5wq", "e5i5")]
        [InlineData(true, "e5wq", "e5e0")]
        [InlineData(true, "e5wq", "e5z0")]
        [InlineData(true, "e5wq", "e5z5")]
        [InlineData(true, "e5wq", "e5z9")]
        [InlineData(false, "e5bq", "e5e9")]
        [InlineData(false, "e5bq", "e5i9")]
        [InlineData(false, "e5bq", "e5i5")]
        [InlineData(false, "e5bq", "e5e0")]
        [InlineData(false, "e5bq", "e5z0")]
        [InlineData(false, "e5bq", "e5z5")]
        [InlineData(false, "e5bq", "e5z9")]
        public void QueenOutOfBoundsMove(bool white, string queen, string illegalMove)
        {
            // Initializing game
            Game game = new Game(true);

            // Queen
            game.AddPiece(queen);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(illegalMove);
            Assert.False(isLegal, "Queen must not be able to move out of game board.");
        }
        //----- Attacks:
        [Theory]
        [InlineData(true, "e5wq", "g7bp", "e5g7")]
        [InlineData(true, "e5wq", "g3bp", "e5g3")]
        [InlineData(true, "e5wq", "c7bp", "e5c7")]
        [InlineData(true, "e5wq", "c3bp", "e5c3")]
        [InlineData(true, "e5wq", "e8bp", "e5e8")]
        [InlineData(true, "e5wq", "h5bp", "e5h5")]
        [InlineData(true, "e5wq", "a5bp", "e5a5")]
        [InlineData(true, "e5wq", "e1bp", "e5e1")]
        [InlineData(false, "e5bq", "g7wp", "e5g7")]
        [InlineData(false, "e5bq", "g3wp", "e5g3")]
        [InlineData(false, "e5bq", "c7wp", "e5c7")]
        [InlineData(false, "e5bq", "c3wp", "e5c3")]
        [InlineData(false, "e5bq", "e8wp", "e5e8")]
        [InlineData(false, "e5bq", "h5wp", "e5h5")]
        [InlineData(false, "e5bq", "a5wp", "e5a5")]
        [InlineData(false, "e5bq", "e1wp", "e5e1")]
        public void QueenTakesOpponentPiece(bool white, string queen, string captureTarget, string move)
        {
            // Initializing game
            Game game = new Game(true);

            // Queen
            game.AddPiece(queen);
            // Opponent pawn to take
            game.AddPiece(captureTarget);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player takes opponent pawn
            bool isLegal = game.TryUciMove(move);

            Assert.True(isLegal, "Queen should be able to take Opponent pieces.");
        }
        [Theory]
        [InlineData(true, "e5wq", "g7wp", "e5g7")]
        [InlineData(true, "e5wq", "g3wp", "e5g3")]
        [InlineData(true, "e5wq", "c7wp", "e5c7")]
        [InlineData(true, "e5wq", "c3wp", "e5c3")]
        [InlineData(true, "e5wq", "e8wp", "e5e8")]
        [InlineData(true, "e5wq", "h5wp", "e5h5")]
        [InlineData(true, "e5wq", "a5wp", "e5a5")]
        [InlineData(true, "e5wq", "e1wp", "e5e1")]
        [InlineData(false, "e5bq", "g7bp", "e5g7")]
        [InlineData(false, "e5bq", "g3bp", "e5g3")]
        [InlineData(false, "e5bq", "c7bp", "e5c7")]
        [InlineData(false, "e5bq", "c3bp", "e5c3")]
        [InlineData(false, "e5bq", "e8bp", "e5e8")]
        [InlineData(false, "e5bq", "h5bp", "e5h5")]
        [InlineData(false, "e5bq", "a5bp", "e5a5")]
        [InlineData(false, "e5bq", "e1bp", "e5e1")]
        public void QueenTakesAllyPiece(bool white, string queen, string captureTarget, string move)
        {
            // Initializing game
            Game game = new Game(true);

            // Queen
            game.AddPiece(queen);
            // Ally pawn to take
            game.AddPiece(captureTarget);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player takes ally pawn
            bool isLegal = game.TryUciMove(move);

            Assert.False(isLegal, "Queen must not be able to take Ally pieces.");
        }
    }
}