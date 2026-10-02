using SleepyPawn.Cli.Utils;

namespace SleepyPawn.Cli
{
    internal class Program
    {
        static string mainMenuTitle = "What do you want to do now?";
        static string[] mainMenuOptions = { 
            "fen", 
            "perft",
            "debug", 
            "exit"
        };
        static string[] mainMenuComments = { 
            "View some FEN positions.",
            "Run some Perft tests.",
            "Play a debug game against yourself.",
            "Close the program." };
        static void Main(string[] args)
        {
            string? lastMessage = null;
            while (true)
            {
                UI.Clear();
                UI.WriteDivider();
                UI.WriteHeader("SLEEPY PAWN");
                if (lastMessage == null)
                {
                    UI.WriteMessage("Welcome to Sleepy Pawn CLI!");
                }
                else
                {
                    UI.WriteMessage(lastMessage, ConsoleColor.Yellow);
                }
                lastMessage = null;
                string mainMenuInput = UI.AskInput(mainMenuTitle, mainMenuOptions, mainMenuComments);
                bool exit = false;
                switch (mainMenuInput)
                {
                    case "debug":
                        DebugGame testGame = new DebugGame();
                        testGame.Play();
                        break;
                    case "fen":
                        FenViewer fen = new FenViewer();
                        fen.Play();
                        break;
                    case "perft":
                        PerftViewer perft = new PerftViewer();
                        perft.Play();
                        break;
                    case "exit":
                        UI.Clear();
                        UI.WriteDivider();
                        UI.WriteHeader("Closing program...");
                        UI.WriteDivider();
                        exit = true;
                        break;
                    default:
                        lastMessage = "Sorry, not implemented yet...";
                        UI.WriteMessage("Sorry, not implemented yet...");
                        break;
                }
                if (exit)
                {
                    break;
                }
            }
        }
    }
}