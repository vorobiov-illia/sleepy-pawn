using SleepyPawn.Cli.Utils;
using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Cli
{
    
    internal class FenViewer
    {
        static string mainMenuTitle = "What do you want to do now?";
        static string[] mainMenuOptions = { 
            "play",
            "perft",
            "change",
            "return"
        };
        static string[] mainMenuComments = {
            "Play a debug game against yourself from this position.",
            "Run a perft test for this position.",
            "Try another FEN position.",
            "Return to main menu." };
        internal void Play()
        {
            string? lastMessage = null;
            string lastFen = "";

            SleepyChess? game = null;
            while (true)
            {
                UI.Clear();
                UI.WriteDivider();
                UI.WriteHeader("FEN VIEWER");

                if (lastMessage == null)
                {
                    UI.WriteMessage("Here you can inspect some FEN positions.");
                }
                else
                {
                    UI.WriteMessage(lastMessage, ConsoleColor.Yellow);
                }
                lastMessage = null;

                if (game == null)
                {
                    string action = UI.AskStringInput("Enter a FEN position (example: \"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1\") or type return.");
                    if (action == "return") return;
                    bool fenValid = FenUtils.IsValid(action);
                    if (!fenValid)
                    {
                        lastMessage = "Invalid FEN string: \"" + action + "\".";
                        continue;
                    }
                    lastFen = action;
                    game = new SleepyChess(action, true);
                }
                else
                {
                    UI.WriteDivider();
                    UI.WriteMessage(game.DebugCurrentMoveOrder());
                    UI.WriteMessage(game.DebugLegalMoveCount());
                    UI.WriteMessage(game.DebugWhiteCheck());
                    UI.WriteMessage(game.DebugBlackCheck());
                    UI.WriteBoard(game.DebugCurrentBoard());

                    string fenMenuInput = UI.AskInput(mainMenuTitle, mainMenuOptions, mainMenuComments);
                    switch (fenMenuInput)
                    {
                        case "play":
                            DebugGame testGame = new DebugGame(lastFen);
                            testGame.Play();
                            return;
                        case "perft":
                            PerftViewer perft = new PerftViewer(lastFen);
                            perft.Play();
                            return;
                        case "change":
                            game = null;
                            break;
                        case "return":
                            return;
                        default:
                            lastMessage = "Sorry, not implemented yet...";
                            UI.WriteMessage("Sorry, not implemented yet...");
                            break;
                    }
                }
            }
        }
    }
}
