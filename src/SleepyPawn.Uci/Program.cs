using SleepyPawn.Core.Chess;
using SleepyPawn.Uci;

while (true) {
    if(Console.ReadLine() == "uci")
    {
        Console.WriteLine("id name Sleepy Pawn");
        Console.WriteLine("id author Voil");
        Console.WriteLine("uciok");
        SleepyChess engine = new SleepyChess();
        while (true)
        {
            string? input = Console.ReadLine();
            if (input == null) continue;
            if (CommandParser.IsQuitCommand(input)) break;
            CommandParser.ParseCommand(input).Execute(engine);
        }
        break;
    }
}