using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Search;

namespace SleepyPawn.Uci.Commands
{
    internal class IsReadyCommand : Command
    {
        internal override void Execute(SleepyChess engine, SleepySearch? search = null)
        {
            if (engine != null) Console.WriteLine("readyok");
        }
    }
}
