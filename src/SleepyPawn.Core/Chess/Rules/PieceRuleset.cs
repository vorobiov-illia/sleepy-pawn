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
            if (thisPiece == null) return false;
            if (thisPiece.isEmpty) return false;
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

        internal void GenerateThreat(EnginePosition position, Board board, ThreatBoard threats)
        {
            Piece thisPiece = board.GetPiece(position);
            if (thisPiece == null) return;
            if (thisPiece.isEmpty) return;
            switch (thisPiece.type)
            {
                case PieceType.Sleepy:
                    pawns.GenerateThreat(position, board, threats);
                    break;
                case PieceType.Bishop:
                    bishops.GenerateThreat(position, board, threats);
                    break;
                case PieceType.Knight:
                    knights.GenerateThreat(position, board, threats);
                    break;
                case PieceType.Rook:
                    rooks.GenerateThreat(position, board, threats);
                    break;
                case PieceType.Queen:
                    queens.GenerateThreat(position, board, threats);
                    break;
                case PieceType.King:
                    kings.GenerateThreat(position, board, threats);
                    break;
                default:
                    return;
            }
        }
    }
}
