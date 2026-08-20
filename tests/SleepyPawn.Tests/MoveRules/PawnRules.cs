using SleepyPawn.Core.Chess;

namespace SleepyPawn.Tests.Rules
{
    public class PawnRules
    {
        // TO-DO: Make illegal horizontal movement tests and illegal double move after normal move test.
        
        //================== WHITE PAWN TESTS:
        //----- Simple moves:
        [Fact]
        public void WhitePawnOpenMove()
        {
            // Initializing game
            Game game = new Game(true);

            // White pawn
            game.AddPiece("e2wp");

            // White player makes legal move
            bool isLegal = game.TryUciMove("e2e3");
            Assert.True(isLegal, "Pawn should be able to move forward on empty tile.");
        }
        [Fact]
        public void WhitePawnClosedMove()
        {
            // Initializing game
            Game game = new Game(true);

            // White pawn
            game.AddPiece("e2wp");
            // Black pawn blocks the way
            game.AddPiece("e3bp");

            // White player makes illegal move
            bool isLegal = game.TryUciMove("e2e3");
            Assert.False(isLegal, "Pawn must not be able to move forward on occupied tile.");
        }
        [Fact]
        public void WhitePawnMoveBack()
        {
            // Initializing game
            Game game = new Game(true);

            // White pawn
            game.AddPiece("e2wp");

            // White player makes illegal move
            bool isLegal = game.TryUciMove("e2e1");
            Assert.False(isLegal, "Pawn must not be able to move backwards.");
        }
        [Fact]
        public void WhiteLongOpenMove()
        {
            // Initializing game
            Game game = new Game(true);

            // White pawn
            game.AddPiece("e2wp");

            // White player makes illegal move
            bool isLegal = game.TryUciMove("e2e5");
            Assert.False(isLegal, "Pawn must not be able to move forward that far.");
        }
        //----- Double moves:
        [Fact]
        public void WhitePawnDoubleOpenMove()
        {
            // Initializing game
            Game game = new Game(true);

            // White pawn
            game.AddPiece("e2wp");

            // White player makes legal move
            bool isLegal = game.TryUciMove("e2e4");
            Assert.True(isLegal, "Pawn should be able to perform double forward move one time.");
        }
        [Fact]
        public void WhitePawnDoubleOpenMoveBlocked()
        {
            // Initializing game
            Game game = new Game(true);

            // White pawn
            game.AddPiece("e2wp");
            // Black pawn blocks the way
            game.AddPiece("e3bp");

            // White player makes illegal move
            bool isLegal = game.TryUciMove("e2e4");
            Assert.False(isLegal, "Pawn must not be able to perform double forward move if piece blocks the way.");
        }
        [Fact]
        public void WhitePawnDoubleClosedMove()
        {
            // Initializing game
            Game game = new Game(true);

            // White pawn
            game.AddPiece("e2wp");
            // Black pawn occupies the tile
            game.AddPiece("e4bp");

            // White player makes illegal move
            bool isLegal = game.TryUciMove("e2e4");
            Assert.False(isLegal, "Pawn must not be able to perform double forward move on occupied tile.");
        }
        //----- Attacks:
        [Fact]
        public void WhitePawnTakesBlack()
        {
            // Initializing game
            Game rightGame = new Game(true);

            // White pawn
            rightGame.AddPiece("e2wp");
            // Black pawn to take
            rightGame.AddPiece("f3bp");

            // White player takes right pawn
            bool rightTake = rightGame.TryUciMove("e2f3");

            Assert.True(rightTake, "White Pawn should be able to take Black pieces diagonally right.");

            // Initializing game
            Game leftGame = new Game(true);

            // White pawn
            leftGame.AddPiece("e2wp");
            // Black pawn to take
            leftGame.AddPiece("d3bp");

            // White player takes left pawn
            bool leftTake = leftGame.TryUciMove("e2d3");

            Assert.True(leftTake, "White Pawn should be able to take Black pieces diagonally left.");
        }
        //----- Attacks:
        [Fact]
        public void WhitePawnTakesBlackBack()
        {
            // Initializing game
            Game rightGame = new Game(true);

            // White pawn
            rightGame.AddPiece("e2wp");
            // Black pawn to take
            rightGame.AddPiece("f1bp");

            // White player takes right pawn
            bool rightTake = rightGame.TryUciMove("e2f1");

            Assert.False(rightTake, "White Pawn must not be able to take Black pieces diagonally right from back.");

            // Initializing game
            Game leftGame = new Game(true);

            // White pawn
            leftGame.AddPiece("e2wp");
            // Black pawn to take
            leftGame.AddPiece("d1bp");

            // White player takes left pawn
            bool leftTake = leftGame.TryUciMove("e2d1");

            Assert.False(leftTake, "White Pawn must not be able to take Black pieces diagonally left from back.");
        }
        [Fact]
        public void WhitePawnTakesWhite()
        {
            // Initializing game
            Game rightGame = new Game(true);

            // White pawn
            rightGame.AddPiece("e2wp");
            // White pawn to take
            rightGame.AddPiece("f3wp");

            // White player takes right pawn
            bool rightTake = rightGame.TryUciMove("e2f3");

            Assert.False(rightTake, "White Pawn must not be able to take White pieces from right.");

            // Initializing game
            Game leftGame = new Game(true);

            // White pawn
            leftGame.AddPiece("e2wp");
            // White pawn to take
            leftGame.AddPiece("d3wp");

            // White player takes left pawn
            bool leftTake = leftGame.TryUciMove("e2d3");

            Assert.False(leftTake, "White Pawn must not be able to take White pieces from left.");
        }
        [Fact]
        public void WhitePawnTakesNothing()
        {
            // Initializing game
            Game rightGame = new Game(true);

            // White pawn
            rightGame.AddPiece("e2wp");

            // White player takes right
            bool rightTake = rightGame.TryUciMove("e2f3");

            Assert.False(rightTake, "White Pawn must not be able to move diagonally right without piece to capture.");

            // Initializing game
            Game leftGame = new Game(true);

            // White pawn
            leftGame.AddPiece("e2wp");

            // White player takes left
            bool leftTake = leftGame.TryUciMove("e2d3");

            Assert.False(leftTake, "White Pawn must not be able to move diagonally left without piece to capture.");
        }
        //================== BLACK PAWN TESTS:
        //----- Simple moves:
        [Fact]
        public void BlackPawnOpenMove()
        {
            // Initializing game
            Game game = new Game(true);

            // Black pawn
            game.AddPiece("e7bp");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes legal move
            bool isLegal = game.TryUciMove("e7e6");
            Assert.True(isLegal, "Pawn should be able to move forward on empty tile.");
        }
        [Fact]
        public void BlackPawnClosedMove()
        {
            // Initializing game
            Game game = new Game(true);

            // Black pawn
            game.AddPiece("e7bp");
            // White pawn blocks the way
            game.AddPiece("e6wp");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes illegal move
            bool isLegal = game.TryUciMove("e7e6");
            Assert.False(isLegal, "Pawn must not be able to move forward on occupied tile.");
        }
        [Fact]
        public void BlackPawnMoveBack()
        {
            // Initializing game
            Game game = new Game(true);

            // Black pawn
            game.AddPiece("e7bp");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes illegal move
            bool isLegal = game.TryUciMove("e7e8");
            Assert.False(isLegal, "Pawn must not be able to move backwards.");
        }
        [Fact]
        public void BlackLongOpenMove()
        {
            // Initializing game
            Game game = new Game(true);

            // Black pawn
            game.AddPiece("e7bp");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes illegal move
            bool isLegal = game.TryUciMove("e7e4");
            Assert.False(isLegal, "Pawn must not be able to move forward that far.");
        }
        //----- Double moves:
        [Fact]
        public void BlackPawnDoubleOpenMove()
        {
            // Initializing game
            Game game = new Game(true);

            // Black pawn
            game.AddPiece("e7bp");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes legal move
            bool isLegal = game.TryUciMove("e7e5");
            Assert.True(isLegal, "Pawn should be able to perform double forward move one time.");
        }
        [Fact]
        public void BlackPawnDoubleOpenMoveBlocked()
        {
            // Initializing game
            Game game = new Game(true);

            // Black pawn
            game.AddPiece("e7bp");
            // White pawn blocks the way
            game.AddPiece("e6wp");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes illegal move
            bool isLegal = game.TryUciMove("e7e5");
            Assert.False(isLegal, "Pawn must not be able to perform double forward move if piece blocks the way.");
        }
        [Fact]
        public void BlackPawnDoubleClosedMove()
        {
            // Initializing game
            Game game = new Game(true);

            // Black pawn
            game.AddPiece("e7bp");
            // White pawn occupies the tile
            game.AddPiece("e5wp");

            // Skipping white move
            game.TryUciMove("0000");
            // Black player makes illegal move
            bool isLegal = game.TryUciMove("e7e5");
            Assert.False(isLegal, "Pawn must not be able to perform double forward move on occupied tile.");
        }
        //----- Attacks:
        [Fact]
        public void BlackPawnTakesWhite()
        {
            // Initializing game
            Game rightGame = new Game(true);

            // Black pawn
            rightGame.AddPiece("e7bp");
            // White pawn to take
            rightGame.AddPiece("f6wp");

            // Skipping white move
            rightGame.TryUciMove("0000");
            // Black player takes right pawn
            bool rightTake = rightGame.TryUciMove("e7f6");

            Assert.True(rightTake, "Black Pawn should be able to take White pieces diagonally right.");

            // Initializing game
            Game leftGame = new Game(true);

            // Black pawn
            leftGame.AddPiece("e7bp");
            // White pawn to take
            leftGame.AddPiece("d6wp");

            // Skipping white move
            leftGame.TryUciMove("0000");
            // Black player takes left pawn
            bool leftTake = leftGame.TryUciMove("e7d6");

            Assert.True(leftTake, "Black Pawn should be able to take White pieces diagonally left.");
        }
        [Fact]
        public void BlackPawnTakesWhiteBack()
        {
            // Initializing game
            Game rightGame = new Game(true);

            // Black pawn
            rightGame.AddPiece("e7bp");
            // White pawn to take
            rightGame.AddPiece("f8wp");

            // Skipping white move
            rightGame.TryUciMove("0000");
            // Black player takes right pawn
            bool rightTake = rightGame.TryUciMove("e7f8");

            Assert.False(rightTake, "Black Pawn must not be able to take White pieces diagonally right from back.");

            // Initializing game
            Game leftGame = new Game(true);

            // Black pawn
            leftGame.AddPiece("e7bp");
            // White pawn to take
            leftGame.AddPiece("d8wp");

            // Skipping white move
            leftGame.TryUciMove("0000");
            // Black player takes left pawn
            bool leftTake = leftGame.TryUciMove("e7d8");

            Assert.False(leftTake, "Black Pawn must not be able to take White pieces diagonally left from back.");
        }
        [Fact]
        public void BlackPawnTakesBlack()
        {
            // Initializing game
            Game rightGame = new Game(true);

            // Black pawn
            rightGame.AddPiece("e7bp");
            // Black pawn to take
            rightGame.AddPiece("f6bp");

            // Skipping white move
            rightGame.TryUciMove("0000");
            // Black player takes right pawn
            bool rightTake = rightGame.TryUciMove("e7f6");

            Assert.False(rightTake, "Black Pawn must not be able to take Black pieces from right.");

            // Initializing game
            Game leftGame = new Game(true);

            // Black pawn
            leftGame.AddPiece("e7bp");
            // Black pawn to take
            leftGame.AddPiece("d6bp");

            // Skipping white move
            leftGame.TryUciMove("0000");
            // Black player takes left pawn
            bool leftTake = leftGame.TryUciMove("e7d6");

            Assert.False(leftTake, "Black Pawn must not be able to take Black pieces from left.");
        }
        [Fact]
        public void BlackPawnTakesNothing()
        {
            // Initializing game
            Game rightGame = new Game(true);

            // Black pawn
            rightGame.AddPiece("e7bp");

            // Skipping white move
            rightGame.TryUciMove("0000");
            // Black player takes right
            bool rightTake = rightGame.TryUciMove("e7f6");

            Assert.False(rightTake, "Black Pawn must not be able to move diagonally right without piece to capture.");

            // Initializing game
            Game leftGame = new Game(true);

            // Black pawn
            leftGame.AddPiece("e7bp");

            // Skipping white move
            leftGame.TryUciMove("0000");
            // Black player takes left
            bool leftTake = leftGame.TryUciMove("e7d6");

            Assert.False(leftTake, "Black Pawn must not be able to move diagonally left without piece to capture.");
        }
    }
}