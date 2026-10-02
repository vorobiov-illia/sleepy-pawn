using SleepyPawn.Core.Chess;

namespace SleepyPawn.Tests.MoveRules
{
    public class EnPassantRules
    {
        //================== ENPASSANT TESTS:
        //----- Legal cases:
        [Theory]
        [InlineData(false, "b5wp", "a7bp", "a7a5", "b5a6")]
        [InlineData(false, "c5wp", "b7bp", "b7b5", "c5b6")]
        [InlineData(false, "d5wp", "c7bp", "c7c5", "d5c6")]
        [InlineData(false, "e5wp", "d7bp", "d7d5", "e5d6")]
        [InlineData(false, "f5wp", "e7bp", "e7e5", "f5e6")]
        [InlineData(false, "g5wp", "f7bp", "f7f5", "g5f6")]
        [InlineData(false, "h5wp", "g7bp", "g7g5", "h5g6")]
        [InlineData(false, "g5wp", "h7bp", "h7h5", "g5h6")]
        [InlineData(true, "b4bp", "a2wp", "a2a4", "b4a3")]
        [InlineData(true, "c4bp", "b2wp", "b2b4", "c4b3")]
        [InlineData(true, "d4bp", "c2wp", "c2c4", "d4c3")]
        [InlineData(true, "e4bp", "d2wp", "d2d4", "e4d3")]
        [InlineData(true, "f4bp", "e2wp", "e2e4", "f4e3")]
        [InlineData(true, "g4bp", "f2wp", "f2f4", "g4f3")]
        [InlineData(true, "h4bp", "g2wp", "g2g4", "h4g3")]
        [InlineData(true, "g4bp", "h2wp", "h2h4", "g4h3")]
        public void LegalEnPassant(
            bool white,
            string attackerPawn,
            string vulnerablePawn,
            string firstMove,
            string secondMove)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Attacker pawn
            game.AddPiece(attackerPawn);
            // Vulnerable pawn
            game.AddPiece(vulnerablePawn);

            if (!white)
            {
                // Skip white move
                game.TryLanMove("0000");
            }
            // Player makes pawn vulnerable to en passant
            game.TryLanMove(firstMove);
            // Another player makes en passant
            bool isLegal = game.TryLanMove(secondMove);
            Assert.True(isLegal, "Player should be able to perform en passant.");

            bool pawnGone = game.GetPiece(firstMove.Substring(2, 2)) == "00";
            Assert.True(pawnGone, "Vulnerable pawn must not be able to survive en passant.");
        }
        //----- Illegal cases:
        [Theory]
        [InlineData(false, "b5wp", "a7bp", "a7a5", "b5a6")]
        [InlineData(false, "c5wp", "b7bp", "b7b5", "c5b6")]
        [InlineData(false, "d5wp", "c7bp", "c7c5", "d5c6")]
        [InlineData(false, "e5wp", "d7bp", "d7d5", "e5d6")]
        [InlineData(false, "f5wp", "e7bp", "e7e5", "f5e6")]
        [InlineData(false, "g5wp", "f7bp", "f7f5", "g5f6")]
        [InlineData(false, "h5wp", "g7bp", "g7g5", "h5g6")]
        [InlineData(false, "g5wp", "h7bp", "h7h5", "g5h6")]
        [InlineData(true, "b4bp", "a2wp", "a2a4", "b4a3")]
        [InlineData(true, "c4bp", "b2wp", "b2b4", "c4b3")]
        [InlineData(true, "d4bp", "c2wp", "c2c4", "d4c3")]
        [InlineData(true, "e4bp", "d2wp", "d2d4", "e4d3")]
        [InlineData(true, "f4bp", "e2wp", "e2e4", "f4e3")]
        [InlineData(true, "g4bp", "f2wp", "f2f4", "g4f3")]
        [InlineData(true, "h4bp", "g2wp", "g2g4", "h4g3")]
        [InlineData(true, "g4bp", "h2wp", "h2h4", "g4h3")]
        public void LateEnPassant(
            bool white,
            string attackerPawn,
            string vulnerablePawn,
            string firstMove,
            string secondMove)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Attacker pawn
            game.AddPiece(attackerPawn);
            // Vulnerable pawn
            game.AddPiece(vulnerablePawn);

            string attackerRook = "e1" + attackerPawn.Substring(2,1) + "r";
            string passiveRook = "e8" + vulnerablePawn.Substring(2, 1) + "r";
            // Rooks to waste moves
            game.AddPiece(attackerRook);
            game.AddPiece(passiveRook);

            if (!white)
            {
                // Skip white move
                game.TryLanMove("0000");
            }
            // Player makes pawn vulnerable to en passant
            game.TryLanMove(firstMove);

            // Skip some time
            game.TryLanMove("e1f1");
            game.TryLanMove("e8f8");

            // Another player makes en passant
            bool isLegal = game.TryLanMove(secondMove);
            Assert.False(isLegal, "Player must not be able to perform en passant late.");
        }
        [Theory]
        [InlineData(false, "b5wb", "a7bp", "a7a5", "b5a6")]
        [InlineData(false, "c5wn", "a7bp", "a7a5", "c5a6")]
        [InlineData(false, "b6wr", "a7bp", "a7a5", "b6a6")]
        [InlineData(false, "b5wq", "a7bp", "a7a5", "b5a6")]
        [InlineData(false, "b7wk", "a7bp", "a7a5", "b7a6")]
        [InlineData(true, "b4bb", "a2wp", "a2a4", "b4a3")]
        [InlineData(true, "c4bn", "a2wp", "a2a4", "c4a3")]
        [InlineData(true, "b3br", "a2wp", "a2a4", "b3a3")]
        [InlineData(true, "b4bq", "a2wp", "a2a4", "b4a3")]
        [InlineData(true, "b2bk", "a2wp", "a2a4", "b2a3")]
        public void WrongPieceEnPassant(
            bool white,
            string attackerPawn,
            string vulnerablePawn,
            string firstMove,
            string secondMove)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Attacker pawn
            game.AddPiece(attackerPawn);
            // Vulnerable pawn
            game.AddPiece(vulnerablePawn);

            if (!white)
            {
                // Skip white move
                game.TryLanMove("0000");
            }
            // Player makes pawn vulnerable to en passant
            game.TryLanMove(firstMove);
            // Another player makes legal move, but not en passant
            bool isLegal = game.TryLanMove(secondMove);
            Assert.True(isLegal, "Player should be able to perform move on en passant tile, even if piece is not a pawn.");

            bool pawnGone = game.GetPiece(firstMove.Substring(2, 2)) == "00";
            Assert.False(pawnGone, "Vulnerable pawn must not be gone after piece that is not a pawn moves on en passant tile.");
        }
        [Theory]
        [InlineData(false, "b2wp", "a7bp", "a7a5", "b2a6")]
        [InlineData(false, "c2wp", "b7bp", "b7b5", "c2b6")]
        [InlineData(false, "d2wp", "c7bp", "c7c5", "d2c6")]
        [InlineData(false, "e2wp", "d7bp", "d7d5", "e2d6")]
        [InlineData(false, "f2wp", "e7bp", "e7e5", "f2e6")]
        [InlineData(false, "g2wp", "f7bp", "f7f5", "g2f6")]
        [InlineData(false, "h2wp", "g7bp", "g7g5", "h2g6")]
        [InlineData(false, "g2wp", "h7bp", "h7h5", "g2h6")]
        [InlineData(true, "b7bp", "a2wp", "a2a4", "b7a3")]
        [InlineData(true, "c7bp", "b2wp", "b2b4", "c7b3")]
        [InlineData(true, "d7bp", "c2wp", "c2c4", "d7c3")]
        [InlineData(true, "e7bp", "d2wp", "d2d4", "e7d3")]
        [InlineData(true, "f7bp", "e2wp", "e2e4", "f7e3")]
        [InlineData(true, "g7bp", "f2wp", "f2f4", "g7f3")]
        [InlineData(true, "h7bp", "g2wp", "g2g4", "h7g3")]
        [InlineData(true, "g7bp", "h2wp", "h2h4", "g7h3")]
        public void WrongRankEnPassant(
            bool white,
            string attackerPawn,
            string vulnerablePawn,
            string firstMove,
            string secondMove)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Attacker pawn
            game.AddPiece(attackerPawn);
            // Vulnerable pawn
            game.AddPiece(vulnerablePawn);

            if (!white)
            {
                // Skip white move
                game.TryLanMove("0000");
            }
            // Player makes pawn vulnerable to en passant
            game.TryLanMove(firstMove);
            // Another player makes en passant
            bool isLegal = game.TryLanMove(secondMove);
            Assert.False(isLegal, "Player must not be able to perform en passant from far away.");
        }
        [Theory]
        [InlineData(false, "b5wp", "a6bp", "a6a5", "b5a6")]
        [InlineData(false, "c5wp", "b6bp", "b6b5", "c5b6")]
        [InlineData(false, "d5wp", "c6bp", "c6c5", "d5c6")]
        [InlineData(false, "e5wp", "d6bp", "d6d5", "e5d6")]
        [InlineData(false, "f5wp", "e6bp", "e6e5", "f5e6")]
        [InlineData(false, "g5wp", "f6bp", "f6f5", "g5f6")]
        [InlineData(false, "h5wp", "g6bp", "g6g5", "h5g6")]
        [InlineData(false, "g5wp", "h6bp", "h6h5", "g5h6")]
        [InlineData(true, "b4bp", "a3wp", "a3a4", "b4a3")]
        [InlineData(true, "c4bp", "b3wp", "b3b4", "c4b3")]
        [InlineData(true, "d4bp", "c3wp", "c3c4", "d4c3")]
        [InlineData(true, "e4bp", "d3wp", "d3d4", "e4d3")]
        [InlineData(true, "f4bp", "e3wp", "e3e4", "f4e3")]
        [InlineData(true, "g4bp", "f3wp", "f3f4", "g4f3")]
        [InlineData(true, "h4bp", "g3wp", "g3g4", "h4g3")]
        [InlineData(true, "g4bp", "h3wp", "h3h4", "g4h3")]
        public void FakeEnPassant(
            bool white,
            string attackerPawn,
            string vulnerablePawn,
            string firstMove,
            string secondMove)
        {
            // Initializing game
            SleepyChess game = new SleepyChess(true);

            // Attacker pawn
            game.AddPiece(attackerPawn);
            // Vulnerable pawn
            game.AddPiece(vulnerablePawn);

            if (!white)
            {
                // Skip white move
                game.TryLanMove("0000");
            }
            // Player makes pawn vulnerable to en passant
            game.TryLanMove(firstMove);
            // Another player makes en passant
            bool isLegal = game.TryLanMove(secondMove);
            Assert.False(isLegal, "Player must not be able to perform en passant when the opponent's pawn has not performed double move.");
        }
    }
}
