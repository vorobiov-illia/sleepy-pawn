using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess.Rules
{
    internal static class LegalMoveAnalyzer
    {
        private static PieceRuleset pieceRules = new PieceRuleset();
        public static bool IsLegal(Move move, GameState state)
        {
            if (!move.isValid) return false;
            if (move.nullMove) return true;
            Piece piece = state.GetPiece(move.firstPos);
            if (state.playerToMove == ColorUtils.Reverse(piece.color)) return false;
            return pieceRules.CheckRules(move, state);
        }
    }
}
