using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Chess.Rules.Figures;

namespace SleepyPawn.Core.Chess.Rules
{
    internal class FigureRuleset
    {
        private PawnRules pawns;
        private BishopRules bishops;
        private KnightRules knights;
        private RookRules rooks;
        private QueenRules queens;
        private KingRules kings;

        internal FigureRuleset()
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
            Figure figure = state.GetFigure(move.firstPos);
            if (figure == null || figure.isEmpty) return false;
            switch (figure.type)
            {
                case FigureType.Sleepy:
                    return pawns.CanMove(move, state);
                case FigureType.Bishop:
                    return bishops.CanMove(move, state);
                case FigureType.Knight:
                    return knights.CanMove(move, state);
                case FigureType.Rook:
                    return rooks.CanMove(move, state);
                case FigureType.Queen:
                    return queens.CanMove(move, state);
                case FigureType.King:
                    return kings.CanMove(move, state);
                default:
                    return false;
            }
        }
    }
}
