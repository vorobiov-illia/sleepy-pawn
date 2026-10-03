using SleepyPawn.Core.Chess;

namespace SleepyPawn.Uci.Commands
{
    internal abstract class Command
    {
        internal abstract void Execute(SleepyChess engine);
    }
}
