using SleepyPawn.Core.Chess;

namespace SleepyPawn.Tests.MoveRules
{
    public class PromotionRules
    {
        //================== PROMOTION TESTS:
        //----- Legal cases:
        [Theory]
        [InlineData(true, "a7wp", "a7a8b")]
        [InlineData(true, "a7wp", "a7a8n")]
        [InlineData(true, "a7wp", "a7a8r")]
        [InlineData(true, "a7wp", "a7a8q")]
        [InlineData(true, "b7wp", "b7b8b")]
        [InlineData(true, "b7wp", "b7b8n")]
        [InlineData(true, "b7wp", "b7b8r")]
        [InlineData(true, "b7wp", "b7b8q")]
        [InlineData(true, "c7wp", "c7c8b")]
        [InlineData(true, "c7wp", "c7c8n")]
        [InlineData(true, "c7wp", "c7c8r")]
        [InlineData(true, "c7wp", "c7c8q")]
        [InlineData(true, "d7wp", "d7d8b")]
        [InlineData(true, "d7wp", "d7d8n")]
        [InlineData(true, "d7wp", "d7d8r")]
        [InlineData(true, "d7wp", "d7d8q")]
        [InlineData(true, "e7wp", "e7e8b")]
        [InlineData(true, "e7wp", "e7e8n")]
        [InlineData(true, "e7wp", "e7e8r")]
        [InlineData(true, "e7wp", "e7e8q")]
        [InlineData(true, "f7wp", "f7f8b")]
        [InlineData(true, "f7wp", "f7f8n")]
        [InlineData(true, "f7wp", "f7f8r")]
        [InlineData(true, "f7wp", "f7f8q")]
        [InlineData(true, "g7wp", "g7g8b")]
        [InlineData(true, "g7wp", "g7g8n")]
        [InlineData(true, "g7wp", "g7g8r")]
        [InlineData(true, "g7wp", "g7g8q")]
        [InlineData(true, "h7wp", "h7h8b")]
        [InlineData(true, "h7wp", "h7h8n")]
        [InlineData(true, "h7wp", "h7h8r")]
        [InlineData(true, "h7wp", "h7h8q")]
        [InlineData(false, "a2bp", "a2a1b")]
        [InlineData(false, "a2bp", "a2a1n")]
        [InlineData(false, "a2bp", "a2a1r")]
        [InlineData(false, "a2bp", "a2a1q")]
        [InlineData(false, "b2bp", "b2b1b")]
        [InlineData(false, "b2bp", "b2b1n")]
        [InlineData(false, "b2bp", "b2b1r")]
        [InlineData(false, "b2bp", "b2b1q")]
        [InlineData(false, "c2bp", "c2c1b")]
        [InlineData(false, "c2bp", "c2c1n")]
        [InlineData(false, "c2bp", "c2c1r")]
        [InlineData(false, "c2bp", "c2c1q")]
        [InlineData(false, "d2bp", "d2d1b")]
        [InlineData(false, "d2bp", "d2d1n")]
        [InlineData(false, "d2bp", "d2d1r")]
        [InlineData(false, "d2bp", "d2d1q")]
        [InlineData(false, "e2bp", "e2e1b")]
        [InlineData(false, "e2bp", "e2e1n")]
        [InlineData(false, "e2bp", "e2e1r")]
        [InlineData(false, "e2bp", "e2e1q")]
        [InlineData(false, "f2bp", "f2f1b")]
        [InlineData(false, "f2bp", "f2f1n")]
        [InlineData(false, "f2bp", "f2f1r")]
        [InlineData(false, "f2bp", "f2f1q")]
        [InlineData(false, "g2bp", "g2g1b")]
        [InlineData(false, "g2bp", "g2g1n")]
        [InlineData(false, "g2bp", "g2g1r")]
        [InlineData(false, "g2bp", "g2g1q")]
        [InlineData(false, "h2bp", "h2h1b")]
        [InlineData(false, "h2bp", "h2h1n")]
        [InlineData(false, "h2bp", "h2h1r")]
        [InlineData(false, "h2bp", "h2h1q")]
        public void LegalPromotion(bool white, string pawn, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Pawn
            game.AddPiece(pawn);

            if (!white)
            {
                // Skip white move
                game.TryLanMove("0000");
            }
            // Player makes pawn promotion
            bool isLegal = game.TryLanMove(move);
            Assert.True(isLegal, "Player should be able to perform pawn promotion.");
        }
        [Theory]
        [InlineData(true, "a7wp", "b8br", "a7b8b")]
        [InlineData(true, "a7wp", "b8br", "a7b8n")]
        [InlineData(true, "a7wp", "b8br", "a7b8r")]
        [InlineData(true, "a7wp", "b8br", "a7b8q")]
        [InlineData(false, "a2bp", "b1wr", "a2b1b")]
        [InlineData(false, "a2bp", "b1wr", "a2b1n")]
        [InlineData(false, "a2bp", "b1wr", "a2b1r")]
        [InlineData(false, "a2bp", "b1wr", "a2b1q")]
        public void LegalCapturePromotion(bool white, string pawn, string pieceToCapture, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Pawn
            game.AddPiece(pawn);
            // Piece to capture
            game.AddPiece(pieceToCapture);

            if (!white)
            {
                // Skip white move
                game.TryLanMove("0000");
            }
            // Player makes pawn promotion
            bool isLegal = game.TryLanMove(move);
            Assert.True(isLegal, "Player should be able to perform pawn promotion on capture.");
        }
        //----- Illegal cases:
        [Theory]
        [InlineData(true, "a7wp", "a7a8p")]
        [InlineData(true, "a7wp", "a7a8k")]
        [InlineData(true, "b7wp", "b7b8p")]
        [InlineData(true, "b7wp", "b7b8k")]
        [InlineData(true, "c7wp", "c7c8p")]
        [InlineData(true, "c7wp", "c7c8k")]
        [InlineData(true, "d7wp", "d7d8p")]
        [InlineData(true, "d7wp", "d7d8k")]
        [InlineData(true, "e7wp", "e7e8p")]
        [InlineData(true, "e7wp", "e7e8k")]
        [InlineData(true, "f7wp", "f7f8p")]
        [InlineData(true, "f7wp", "f7f8k")]
        [InlineData(true, "g7wp", "g7g8p")]
        [InlineData(true, "g7wp", "g7g8k")]
        [InlineData(true, "h7wp", "h7h8p")]
        [InlineData(true, "h7wp", "h7h8k")]
        [InlineData(false, "a2bp", "a2a1p")]
        [InlineData(false, "a2bp", "a2a1k")]
        [InlineData(false, "b2bp", "b2b1p")]
        [InlineData(false, "b2bp", "b2b1k")]
        [InlineData(false, "c2bp", "c2c1p")]
        [InlineData(false, "c2bp", "c2c1k")]
        [InlineData(false, "d2bp", "d2d1p")]
        [InlineData(false, "d2bp", "d2d1k")]
        [InlineData(false, "e2bp", "e2e1p")]
        [InlineData(false, "e2bp", "e2e1k")]
        [InlineData(false, "f2bp", "f2f1p")]
        [InlineData(false, "f2bp", "f2f1k")]
        [InlineData(false, "g2bp", "g2g1p")]
        [InlineData(false, "g2bp", "g2g1k")]
        [InlineData(false, "h2bp", "h2h1p")]
        [InlineData(false, "h2bp", "h2h1k")]
        public void WrongPromotion(bool white, string pawn, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Pawn
            game.AddPiece(pawn);

            if (!white)
            {
                // Skip white move
                game.TryLanMove("0000");
            }
            // Player makes illegal pawn promotion
            bool isLegal = game.TryLanMove(move);
            Assert.False(isLegal, "Player must not be able to promote pawn to king or pawn.");
        }
        [Theory]
        [InlineData(true, "a7wp", "a7a8")]
        [InlineData(true, "b7wp", "b7b8")]
        [InlineData(true, "c7wp", "c7c8")]
        [InlineData(true, "d7wp", "d7d8")]
        [InlineData(true, "e7wp", "e7e8")]
        [InlineData(true, "f7wp", "f7f8")]
        [InlineData(true, "g7wp", "g7g8")]
        [InlineData(true, "h7wp", "h7h8")]
        [InlineData(false, "a2bp", "a2a1")]
        [InlineData(false, "b2bp", "b2b1")]
        [InlineData(false, "c2bp", "c2c1")]
        [InlineData(false, "d2bp", "d2d1")]
        [InlineData(false, "e2bp", "e2e1")]
        [InlineData(false, "f2bp", "f2f1")]
        [InlineData(false, "g2bp", "g2g1")]
        [InlineData(false, "h2bp", "h2h1")]
        public void NoPromotion(bool white, string pawn, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Pawn
            game.AddPiece(pawn);

            if (!white)
            {
                // Skip white move
                game.TryLanMove("0000");
            }
            // Player makes illegal pawn move
            bool isLegal = game.TryLanMove(move);
            Assert.False(isLegal, "Player must not be able to move pawn on last rank without promotion.");
        }
        [Theory]
        [InlineData(true, "a7wb", "a7b8b")]
        [InlineData(true, "a7wb", "a7b8n")]
        [InlineData(true, "a7wb", "a7b8r")]
        [InlineData(true, "a7wb", "a7b8q")]
        [InlineData(true, "a7wn", "a7c8b")]
        [InlineData(true, "a7wn", "a7c8n")]
        [InlineData(true, "a7wn", "a7c8r")]
        [InlineData(true, "a7wn", "a7c8q")]
        [InlineData(true, "a7wr", "a7a8b")]
        [InlineData(true, "a7wr", "a7a8n")]
        [InlineData(true, "a7wr", "a7a8r")]
        [InlineData(true, "a7wr", "a7a8q")]
        [InlineData(true, "a7wq", "a7a8b")]
        [InlineData(true, "a7wq", "a7a8n")]
        [InlineData(true, "a7wq", "a7a8r")]
        [InlineData(true, "a7wq", "a7a8q")]
        [InlineData(true, "a7wk", "a7a8b")]
        [InlineData(true, "a7wk", "a7a8n")]
        [InlineData(true, "a7wk", "a7a8r")]
        [InlineData(true, "a7wk", "a7a8q")]
        [InlineData(false, "a2wb", "a2b1b")]
        [InlineData(false, "a2wb", "a2b1n")]
        [InlineData(false, "a2wb", "a2b1r")]
        [InlineData(false, "a2wb", "a2b1q")]
        [InlineData(false, "a2wn", "a2c1b")]
        [InlineData(false, "a2wn", "a2c1n")]
        [InlineData(false, "a2wn", "a2c1r")]
        [InlineData(false, "a2wn", "a2c1q")]
        [InlineData(false, "a2wr", "a2a1b")]
        [InlineData(false, "a2wr", "a2a1n")]
        [InlineData(false, "a2wr", "a2a1r")]
        [InlineData(false, "a2wr", "a2a1q")]
        [InlineData(false, "a2wq", "a2a1b")]
        [InlineData(false, "a2wq", "a2a1n")]
        [InlineData(false, "a2wq", "a2a1r")]
        [InlineData(false, "a2wq", "a2a1q")]
        [InlineData(false, "a2wk", "a2a1b")]
        [InlineData(false, "a2wk", "a2a1n")]
        [InlineData(false, "a2wk", "a2a1r")]
        [InlineData(false, "a2wk", "a2a1q")]

        public void WrongPiecePromotion(bool white, string pawn, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Piece to promote
            game.AddPiece(pawn);

            if (!white)
            {
                // Skip white move
                game.TryLanMove("0000");
            }
            // Player makes piece promotion
            bool isLegal = game.TryLanMove(move);
            Assert.False(isLegal, "Player must not be able to perform promotion with piece that is not a pawn.");
        }
        [Theory]
        [InlineData(true, "a2wp", "a2a3b")]
        [InlineData(true, "a2wp", "a2a3n")]
        [InlineData(true, "a2wp", "a2a3r")]
        [InlineData(true, "a2wp", "a2a3q")]
        [InlineData(true, "b2wp", "b2b3b")]
        [InlineData(true, "b2wp", "b2b3n")]
        [InlineData(true, "b2wp", "b2b3r")]
        [InlineData(true, "b2wp", "b2b3q")]
        [InlineData(true, "c2wp", "c2c3b")]
        [InlineData(true, "c2wp", "c2c3n")]
        [InlineData(true, "c2wp", "c2c3r")]
        [InlineData(true, "c2wp", "c2c3q")]
        [InlineData(true, "d2wp", "d2d3b")]
        [InlineData(true, "d2wp", "d2d3n")]
        [InlineData(true, "d2wp", "d2d3r")]
        [InlineData(true, "d2wp", "d2d3q")]
        [InlineData(true, "e2wp", "e2e3b")]
        [InlineData(true, "e2wp", "e2e3n")]
        [InlineData(true, "e2wp", "e2e3r")]
        [InlineData(true, "e2wp", "e2e3q")]
        [InlineData(true, "f2wp", "f2f3b")]
        [InlineData(true, "f2wp", "f2f3n")]
        [InlineData(true, "f2wp", "f2f3r")]
        [InlineData(true, "f2wp", "f2f3q")]
        [InlineData(true, "g2wp", "g2g3b")]
        [InlineData(true, "g2wp", "g2g3n")]
        [InlineData(true, "g2wp", "g2g3r")]
        [InlineData(true, "g2wp", "g2g3q")]
        [InlineData(true, "h2wp", "h2h3b")]
        [InlineData(true, "h2wp", "h2h3n")]
        [InlineData(true, "h2wp", "h2h3r")]
        [InlineData(true, "h2wp", "h2h3q")]
        [InlineData(false, "a7bp", "a7a6b")]
        [InlineData(false, "a7bp", "a7a6n")]
        [InlineData(false, "a7bp", "a7a6r")]
        [InlineData(false, "a7bp", "a7a6q")]
        [InlineData(false, "b7bp", "b7b6b")]
        [InlineData(false, "b7bp", "b7b6n")]
        [InlineData(false, "b7bp", "b7b6r")]
        [InlineData(false, "b7bp", "b7b6q")]
        [InlineData(false, "c7bp", "c7c6b")]
        [InlineData(false, "c7bp", "c7c6n")]
        [InlineData(false, "c7bp", "c7c6r")]
        [InlineData(false, "c7bp", "c7c6q")]
        [InlineData(false, "d7bp", "d7d6b")]
        [InlineData(false, "d7bp", "d7d6n")]
        [InlineData(false, "d7bp", "d7d6r")]
        [InlineData(false, "d7bp", "d7d6q")]
        [InlineData(false, "e7bp", "e7e6b")]
        [InlineData(false, "e7bp", "e7e6n")]
        [InlineData(false, "e7bp", "e7e6r")]
        [InlineData(false, "e7bp", "e7e6q")]
        [InlineData(false, "f7bp", "f7f6b")]
        [InlineData(false, "f7bp", "f7f6n")]
        [InlineData(false, "f7bp", "f7f6r")]
        [InlineData(false, "f7bp", "f7f6q")]
        [InlineData(false, "g7bp", "g7g6b")]
        [InlineData(false, "g7bp", "g7g6n")]
        [InlineData(false, "g7bp", "g7g6r")]
        [InlineData(false, "g7bp", "g7g6q")]
        [InlineData(false, "h7bp", "h7h6b")]
        [InlineData(false, "h7bp", "h7h6n")]
        [InlineData(false, "h7bp", "h7h6r")]
        [InlineData(false, "h7bp", "h7h6q")]
        public void WrongPositionPromotion(bool white, string pawn, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Pawn
            game.AddPiece(pawn);

            if (!white)
            {
                // Skip white move
                game.TryLanMove("0000");
            }
            // Player makes pawn promotion on wrong position
            bool isLegal = game.TryLanMove(move);
            Assert.False(isLegal, "Player must not be able to perform pawn promotion on position that is not lying on last rank.");
        }
        [Theory]
        [InlineData(true, "a7wp", "a8bp", "a7a8b")]
        [InlineData(true, "a7wp", "a8bp", "a7a8n")]
        [InlineData(true, "a7wp", "a8bp", "a7a8r")]
        [InlineData(true, "a7wp", "a8bp", "a7a8q")]
        [InlineData(false, "a2bp", "a1wp", "a2a1b")]
        [InlineData(false, "a2bp", "a1wp", "a2a1n")]
        [InlineData(false, "a2bp", "a1wp", "a2a1r")]
        [InlineData(false, "a2bp", "a1wp", "a2a1q")]
        public void BlockedPromotion(bool white, string pawn, string block, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Pawn
            game.AddPiece(pawn);
            // Block piece
            game.AddPiece(block);

            if (!white)
            {
                // Skip white move
                game.TryLanMove("0000");
            }
            // Player makes pawn promotion
            bool isLegal = game.TryLanMove(move);
            Assert.False(isLegal, "Player must not be able to perform pawn promotion on blocked tile.");
        }
    }
}
