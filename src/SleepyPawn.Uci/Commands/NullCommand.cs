using SleepyPawn.Core.Chess;

namespace SleepyPawn.Uci.Commands
{
    internal class NullCommand : Command
    {
        internal override void Execute(SleepyChess engine)
        {
            // Nothing...
        }
    }
}
