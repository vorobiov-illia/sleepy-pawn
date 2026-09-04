using SleepyPawn.Core.Chess.Enums;
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
            if (piece == null) return false;
            if (piece.isEmpty) return false;
            if (state.playerToMove == ColorUtils.Reverse(piece.color)) return false;
            if (move.promotionPiece != PieceType.None)
            {
                if (piece.type != PieceType.Sleepy) return false;
                if (piece.color == Color.White)
                {
                    if (move.secondPos.y != 7) return false;
                }
                else if (piece.color == Color.Black)
                {
                    if (move.secondPos.y != 0) return false;
                }
                else
                {
                    return false;
                }
            }
            if(pieceRules.CheckRules(move, state))
            {
                GameState pseudoState = state.AppendMove(move);
                if (pseudoState.KingChecked(state.playerToMove)) return false;
                return true;
            }
            return false;
        }
    }
}
