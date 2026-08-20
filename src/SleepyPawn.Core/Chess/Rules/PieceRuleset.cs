using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Chess.Rules.Pieces;

namespace SleepyPawn.Core.Chess.Rules
{
    internal class PieceRuleset
    {
        private PawnRules pawns;
        private BishopRules bishops;
        private KnightRules knights;
        private RookRules rooks;
        private QueenRules queens;
        private KingRules kings;

        internal PieceRuleset()
        {
            pawns = new PawnRules();
            bishops = new BishopRules();
            knights = new KnightRules();
            rooks = new RookRules();
            queens = new QueenRules();
            kings = new KingRules();
        }
        internal bool CheckRules(Move move, GameState state)
        {
            Piece thisPiece = state.GetPiece(move.firstPos);
            if (thisPiece == null || thisPiece.isEmpty) return false;
            switch (thisPiece.type)
            {
                case PieceType.Sleepy:
                    return pawns.CanMove(move, state);
                case PieceType.Bishop:
                    return bishops.CanMove(move, state);
                case PieceType.Knight:
                    return knights.CanMove(move, state);
                case PieceType.Rook:
                    return rooks.CanMove(move, state);
                case PieceType.Queen:
                    return queens.CanMove(move, state);
                case PieceType.King:
                    return kings.CanMove(move, state);
                default:
                    return false;
            }
        }
    }
}
