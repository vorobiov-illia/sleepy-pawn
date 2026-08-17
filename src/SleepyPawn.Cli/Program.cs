using SleepyPawn.Cli.Debug;
using SleepyPawn.Cli.Utils;

namespace SleepyPawn.Cli
{
    internal class Program
    {
        static string mainMenuTitle = "What do you want to do now?";
        static string[] mainMenuOptions = { "uci", "debug", "exit" };
        static string[] mainMenuComments = { "Enter UCI protocol mode (Usually for GUI's, not humans.)", "Play a debug game with yourself.", "Closes the program." };
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