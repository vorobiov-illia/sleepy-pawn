namespace SleepyPawn.Cli.Utils
{
    internal static class UI
    {
        internal static void Clear()
        {
            Console.ResetColor();
            Console.Clear();
        }
        internal static void WriteHeader(string text)
        {
            Console.WriteLine("====: " + text);
            Console.WriteLine();
        }
        internal static void WriteMessage(string text, ConsoleColor? consoleColor = null)
        {
            if (text == "") return;
            if(consoleColor != null)
            {
                Console.ForegroundColor = consoleColor.Value;
            }
            Console.WriteLine("CLI: " + text);
            if (consoleColor != null)
            {
                Console.ResetColor();
            }
        }
        private static void WriteUserInput()
        {
            Console.WriteLine();
            Console.Write("U: ");
        }
        internal static void WaitUserInput()
        {
            WriteMessage("Press anything to continue...");
            WriteUserInput();
            Console.ReadKey(true);
        }
        internal static void WriteOption(int n, string option, string? comment = null)
        {
            Console.Write(n + ". [" + option + "]");
            if (comment != null)
            {
                Console.Write("\t -- " + comment);
            }
            Console.Write("\n");
        }
        internal static void WriteDivider()
        {
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine();
        }
        internal static void WriteBoard(string board)
        {
            Console.WriteLine();
            for(int i = 0; i < board.Length; i++)
            {
                char c = board[i];
                if (char.IsLetter(c))
                {
                    if (char.IsUpper(c))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }

                Console.Write(c);
                Console.ResetColor();
            }
            Console.WriteLine();
        }
        internal static string AskInput(string text, string[] options, string[]? comments = null)
        {
            WriteDivider();
            WriteHeader(text);
            string[] comm = new string[options.Length];

            if(comments != null)
            {
                for(int i = 0; i < options.Length; i++)
                {
                    comm[i] = comments[i];
                }
            }
            for(int i = 0; i < options.Length; i++)
            {
                WriteOption(i + 1, options[i], comm[i]);
            }
            WriteUserInput();
            string? answer = Console.ReadLine();

            if(answer == null || answer == "")
            {
                return "falseInput";
            }

            for(int i = 0; i < options.Length; i++)
            {
                if(answer == options[i])
                {
                    return options[i];
                }
                else
                {
                    int p;
                    if (int.TryParse(answer, out p))
                    {
                        if(p == i + 1)
                        {
                            return options[i];
                        }
                    }
                }
            }
            return "falseInput";
        }
        internal static string AskStringInput(string text)
        {
            WriteDivider();
            WriteHeader(text);

            WriteUserInput();
            string? answer = Console.ReadLine();
            if (answer == null) answer = "";

            return answer;
        }
    }
}
