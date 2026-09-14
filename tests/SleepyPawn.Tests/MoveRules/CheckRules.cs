using SleepyPawn.Core.Chess;

namespace SleepyPawn.Tests.MoveRules
{
    public class CheckRules
    {
        [Theory]
        [InlineData(true, "e5wk", "f7bp", "e5e6")]
        [InlineData(true, "e5wk", "f7bb", "e5e6")]
        [InlineData(true, "e5wk", "g7bn", "e5e6")]
        [InlineData(true, "e5wk", "f6br", "e5e6")]
        [InlineData(true, "e5wk", "h6bq", "e5e6")]
        [InlineData(true, "e5wk", "f7bk", "e5e6")]
        [InlineData(false, "e5bk", "f5wp", "e5e6")]
        [InlineData(false, "e5bk", "f7wb", "e5e6")]
        [InlineData(false, "e5bk", "g7wn", "e5e6")]
        [InlineData(false, "e5bk", "f6wr", "e5e6")]
        [InlineData(false, "e5bk", "h6wq", "e5e6")]
        [InlineData(false, "e5bk", "f7wk", "e5e6")]
        public void KingMoveToAttackedTile(bool white, string king, string attacker, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // King
            game.AddPiece(king);
            // Attacker
            game.AddPiece(attacker);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(move);
            Assert.False(isLegal, "A player must not be able to move the king to a tile that is attacked by an opponent piece.");
        }
        [Theory]
        [InlineData(true, "e5wk", "f5wp", "e5e6")]
        [InlineData(true, "e5wk", "f7wb", "e5e6")]
        [InlineData(true, "e5wk", "g7wn", "e5e6")]
        [InlineData(true, "e5wk", "f6wr", "e5e6")]
        [InlineData(true, "e5wk", "h6wq", "e5e6")]
        [InlineData(false, "e5bk", "f7bp", "e5e6")]
        [InlineData(false, "e5bk", "f7bb", "e5e6")]
        [InlineData(false, "e5bk", "g7bn", "e5e6")]
        [InlineData(false, "e5bk", "f6br", "e5e6")]
        [InlineData(false, "e5bk", "h6bq", "e5e6")]
        public void KingMoveToAllyTile(bool white, string king, string attacker, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // King
            game.AddPiece(king);
            // Attacker
            game.AddPiece(attacker);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes legal move
            bool isLegal = game.TryUciMove(move);
            Assert.True(isLegal, "A player should be able to move the king to a tile that is attacked by an ally piece.");
        }
        [Theory]
        [InlineData(true, "e5wk", "f5wp", "g5br", "f5f6")]
        [InlineData(true, "e5wk", "f4wp", "g3bb", "f4f5")]
        [InlineData(true, "e5wk", "e4wr", "e3br", "e4f4")]
        [InlineData(true, "e5wk", "d4wp", "c3bb", "d4d5")]
        [InlineData(true, "e5wk", "d5wp", "c5br", "d5d6")]
        [InlineData(true, "e5wk", "d6wp", "c7bb", "d6d7")]
        [InlineData(true, "e5wk", "e6wr", "e7br", "e6f7")]
        [InlineData(true, "e5wk", "f6wp", "g7bb", "f6f7")]
        [InlineData(false, "e5bk", "f5bp", "g5wr", "f5f6")]
        [InlineData(false, "e5bk", "f4bp", "g3wb", "f4f5")]
        [InlineData(false, "e5bk", "e4br", "e3wr", "e4f4")]
        [InlineData(false, "e5bk", "d4bp", "c3wb", "d4d5")]
        [InlineData(false, "e5bk", "d5bp", "c5wr", "d5d6")]
        [InlineData(false, "e5bk", "d6bp", "c7wb", "d6d7")]
        [InlineData(false, "e5bk", "e6br", "e7wr", "e6f7")]
        [InlineData(false, "e5bk", "f6bp", "g7wb", "f6f7")]
        public void LinkedPiece(bool white, string king, string defender, string attacker, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // King
            game.AddPiece(king);
            // Defender
            game.AddPiece(defender);
            // Attacker
            game.AddPiece(attacker);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(move);
            Assert.False(isLegal, "A player must not be able to move a piece that is protecting the king.");
        }
        [Theory]
        [InlineData(true, "e5wk", "a1wp", "g5br", "a1a2")]
        [InlineData(false, "e5bk", "a8bp", "g5wr", "a8a7")]
        public void KingNotMoveFromAttackedTile(bool white, string king, string otherPiece, string attacker, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // King
            game.AddPiece(king);
            // Other piece
            game.AddPiece(otherPiece);
            // Attacker
            game.AddPiece(attacker);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(move);
            Assert.False(isLegal, "A player must not be able to move other pieces while the king is threatened.");
        }
        [Theory]
        [InlineData(true, "e5wk", "g4wr", "g5br", "g4g5")]
        [InlineData(false, "e5bk", "g4br", "g5wr", "g4g5")]
        public void TakeAttackerPiece(bool white, string king, string defender, string attacker, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // King
            game.AddPiece(king);
            // Defender
            game.AddPiece(defender);
            // Attacker
            game.AddPiece(attacker);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes legal move
            bool isLegal = game.TryUciMove(move);
            Assert.True(isLegal, "A player should be able to take piece that is threatening the king.");
        }
        [Theory]
        [InlineData(true, "e5wk", "f4wr", "g5br", "f4f5")]
        [InlineData(false, "e5bk", "f4br", "g5wr", "f4f5")]
        public void BlockAttackerPiece(bool white, string king, string defender, string attacker, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // King
            game.AddPiece(king);
            // Defender
            game.AddPiece(defender);
            // Attacker
            game.AddPiece(attacker);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes legal move
            bool isLegal = game.TryUciMove(move);
            Assert.True(isLegal, "A player should be able to block an attacker piece that is threatening the king.");
        }
        [Theory]
        [InlineData(true, "e5wk", "g5br", "e5d6")]
        [InlineData(true, "e5wk", "g5br", "e5e6")]
        [InlineData(true, "e5wk", "g5br", "e5f6")]
        [InlineData(true, "e5wk", "g5br", "e5d4")]
        [InlineData(true, "e5wk", "g5br", "e5e4")]
        [InlineData(true, "e5wk", "g5br", "e5f4")]
        [InlineData(false, "e5bk", "g5wr", "e5d6")]
        [InlineData(false, "e5bk", "g5wr", "e5e6")]
        [InlineData(false, "e5bk", "g5wr", "e5f6")]
        [InlineData(false, "e5bk", "g5wr", "e5d4")]
        [InlineData(false, "e5bk", "g5wr", "e5e4")]
        [InlineData(false, "e5bk", "g5wr", "e5f4")]
        public void KingEscape(bool white, string king, string attacker, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // King
            game.AddPiece(king);
            // Attacker
            game.AddPiece(attacker);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes legal move
            bool isLegal = game.TryUciMove(move);
            Assert.True(isLegal, "A player should be able to move the king to avoid check.");
        }
        [Theory]
        [InlineData(true, "e5wk", "g5br", "e5d5")]
        [InlineData(true, "e5wk", "g5br", "e5f5")]
        [InlineData(true, "e5wk", "g7bb", "e5f6")]
        [InlineData(true, "e5wk", "g7bb", "e5d4")]
        [InlineData(true, "e5wk", "c7bb", "e5d6")]
        [InlineData(true, "e5wk", "c7bb", "e5f4")]
        [InlineData(false, "e5bk", "g5wr", "e5d5")]
        [InlineData(false, "e5bk", "g5wr", "e5f5")]
        [InlineData(false, "e5bk", "g7wb", "e5f6")]
        [InlineData(false, "e5bk", "g7wb", "e5d4")]
        [InlineData(false, "e5bk", "c7wb", "e5d6")]
        [InlineData(false, "e5bk", "c7wb", "e5f4")]
        public void KingWrongEscape(bool white, string king, string attacker, string move)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // King
            game.AddPiece(king);
            // Attacker
            game.AddPiece(attacker);

            if (!white)
            {
                // Skip white move
                game.TryUciMove("0000");
            }
            // Player makes illegal move
            bool isLegal = game.TryUciMove(move);
            Assert.False(isLegal, "A player must not be able to move the king along attacked line to avoid check.");
        }
    }
}