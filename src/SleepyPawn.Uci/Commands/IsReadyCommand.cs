using SleepyPawn.Core.Chess;

namespace SleepyPawn.Uci.Commands
{
    internal class isReadyCommand : Command
    {
        internal override void Execute(SleepyChess engine)
        {
            if (engine != null) Console.WriteLine("readyok");
        }
    }
}
