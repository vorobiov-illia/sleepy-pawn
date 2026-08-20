using SleepyPawn.Core.Chess;

namespace SleepyPawn.Tests.MoveRules
{
    public class GeneralRules
    {
        [Fact]
        public void WhitePlayerMovesBlackPiece()
        {
            // Initializing game
            Game game = new Game(true);

            // Black pawn
            game.AddPiece("e7bp");

            // White player makes legal move
            bool isLegal = game.TryUciMove("e7e6");
            Assert.True(!isLegal, "A player must not be able to move the other player's pieces.");
        }
        [Fact]
        public void PlayerMovesEmptySquare()
        {
            // Initializing game
            Game game = new Game(true);

            // White player makes illegal move
            bool isLegal = game.TryUciMove("e2e4");
            Assert.True(!isLegal, "A player must not be able to make moves with empty square as piece.");
        }
    }
}
