using SleepyPawn.Core.Chess;

namespace SleepyPawn.Uci.Commands
{
    internal class NewGameCommand : Command
    {
        internal override void Execute(SleepyChess engine)
        {
            engine.NewGame();
        }
    }
}
