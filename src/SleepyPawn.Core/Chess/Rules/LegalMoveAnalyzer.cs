using SleepyPawn.Core.Chess.Rules.Figures;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess.Rules
{
    internal static class LegalMoveAnalyzer
    {
        private static FigureRuleset figureRules = new FigureRuleset();
        public static bool IsLegal(Move move, GameState state)
        {
            Figure figure = state.GetFigure(move.firstPos);
            if (state.playerToMove == ColorUtils.Reverse(figure.color)) return false;
            return figureRules.CheckRules(move, state);
        }
    }
}
