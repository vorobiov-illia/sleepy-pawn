using SleepyPawn.Core.Utils;
using SleepyPawn.Uci.Commands;

namespace SleepyPawn.Uci
{
    internal static class CommandParser
    {
        internal static Command ParseCommand(string input)
        {
            input = input.Trim();
            string[] tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < tokens.Length; i++)
            {
                switch (tokens[i])
                {
                    case "isready":
                        return new isReadyCommand();
                    case "position":
                        if (i + 1 >= tokens.Length) return new NullCommand();
                        string fen = "";
                        List<string> moves = new List<string>();
                        if (tokens[i+1] == "startpos")
                        {
                            if (i + 2 < tokens.Length)
                            {
                                if (tokens[i + 2] == "moves")
                                {
                                    for (int j = i + 3; j < tokens.Length; j++)
                                    {
                                        moves.Add(tokens[j]);
                                    }
                                }
                            }
                        }
                        else if (tokens[i + 1] == "fen")
                        {
                            for (int j = i + 2; j < tokens.Length; j++)
                            {
                                if (tokens[j] == "moves")
                                {
                                    for (int k = j + 1; k < tokens.Length; k++)
                                    {
                                        moves.Add(tokens[k]);
                                    }
                                    break;
                                }
                                if(j == i + 2)
                                {
                                    fen += tokens[j];
                                }
                                else
                                {
                                    fen += " " + tokens[j];
                                }
                            }
                            if (!FenUtils.IsValid(fen)) return new NullCommand();
                        }
                        else
                        {
                            return new NullCommand();
                        }
                        
                        return new PositionCommand(fen, moves.ToArray());
                    case "ucinewgame":
                        return new NewGameCommand();
                    case "go":
                        return new GoCommand();
                }
            }

            return new NullCommand();
        }
        internal static bool isQuitCommand(string input)
        {
            input = input.Trim();
            string[] operands = input.Split(' ');
            for (int i = 0; i < operands.Length; i++)
            {
                switch (operands[i])
                {
                    case "quit":
                        return true;
                }
            }

            return false;
        }
    }
}
