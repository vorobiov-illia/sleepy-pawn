using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Search;

namespace SleepyPawn.Uci.Commands
{
    internal class NewGameCommand : Command
    {
        internal override void Execute(SleepyChess engine, SleepySearch? search = null)
        {
            engine.NewGame();
        }
    }
}
