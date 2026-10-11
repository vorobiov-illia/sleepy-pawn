using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess.Rules
{
    internal static class LegalMoveAnalyzer
    {
        internal static readonly PieceRuleset pieceRules = new PieceRuleset();
        public static bool IsLegal(Move move, State state)
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
                return state.KingSafetyPrecheck(move);
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

        public static int GetLegalMoves(State state, ref Span<Move> legalMoves)
        {
            int legalCount = 0;
            
            Span<Move> pseudoMoves = stackalloc Move[256];
            int pseudoCount = state.GetPseudoMoves(ref pseudoMoves);

            Span<Move> promotions = stackalloc Move[128];
            int promotionsCount = 0;
            
            Span<Move> captures = stackalloc Move[128];
            int capturesCount = 0;

            Span<Move> others = stackalloc Move[256];
            int othersCount = 0;

            for (int i = 0; i < pseudoCount; i++)
            {
                if (IsLegal(pseudoMoves[i], state))
                {
                    if (pseudoMoves[i].promotionPiece != PieceType.None)
                    {
                        promotions[promotionsCount++] = pseudoMoves[i];
                    }
                    else
                    {
                        if (!state.GetPiece(pseudoMoves[i].secondPos).isEmpty)
                        {
                            captures[capturesCount++] = pseudoMoves[i];
                        }
                        else
                        {
                            others[othersCount++] = pseudoMoves[i];
                        }
                    }
                }
            }

            for(int i = 0; i < promotionsCount; i++)
            {
                legalMoves[legalCount++] = promotions[i];
            }
            for (int i = 0; i < capturesCount; i++)
            {
                legalMoves[legalCount++] = captures[i];
            }
            for (int i = 0; i < othersCount; i++)
            {
                legalMoves[legalCount++] = others[i];
            }

            return legalCount;
        }
    }
}
