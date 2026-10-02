using SleepyPawn.Core.Chess;

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
            if (input == "quit") break;
            string output = engine.CallUCI(input);
            Console.WriteLine(output);
        }
        break;
    }
}