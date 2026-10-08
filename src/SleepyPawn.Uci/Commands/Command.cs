using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Search;

namespace SleepyPawn.Uci.Commands
{
    internal abstract class Command
    {
        internal abstract void Execute(SleepyChess engine, SleepySearch? search);
    }
}
