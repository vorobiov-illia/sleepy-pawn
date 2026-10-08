using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Search;

namespace SleepyPawn.Uci.Commands
{
    internal class NullCommand : Command
    {
        internal override void Execute(SleepyChess engine, SleepySearch? search = null)
        {
            // Nothing...
        }
    }
}
