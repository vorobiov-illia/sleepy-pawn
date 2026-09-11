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

        public static bool IsValid(Move move)
        {
            if (move.firstPos.x < 0 || move.firstPos.x > 7) return false;
            if (move.firstPos.y < 0 || move.firstPos.y > 7) return false;
            if (move.secondPos.x < 0 || move.secondPos.x > 7) return false;
            if (move.secondPos.y < 0 || move.secondPos.y > 7) return false;
            return true;
        }

        public static List<Move> GetLegalMoves(GameState state)
        {
            List<Move> legalMoves = new List<Move>();

            List<Move> pseudoMoves = new List<Move>();
            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    EnginePosition position = new EnginePosition(i,j);
                    Piece piece = state.GetPiece(position);

                    if (piece.isEmpty) continue;
                    if (piece.color != state.playerToMove) continue;

                    pseudoMoves.AddRange(pieceRules.GetPseudoMoves(position, state));
                }
            }

            foreach(Move move in pseudoMoves)
            {
                if (IsLegal(move, state))
                {
                    legalMoves.Add(move);
                }
            }

            return legalMoves;
        }
    }
}
