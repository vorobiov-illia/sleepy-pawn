using SleepyPawn.Cli.Utils;
using SleepyPawn.Core.Chess;

namespace SleepyPawn.Cli
{
    internal class DebugGame
    {
        SleepyChess game;
        internal DebugGame(string? fen = null)
        {
            if(fen != null)
            {
                game = new SleepyChess(fen);
            }
            else
            {
                game = new SleepyChess();
            }
        }
        internal void Play()
        {
            string? lastMessage = null;
            while (true)
            {
                UI.Clear();
                UI.WriteDivider();
                UI.WriteHeader("DEBUG GAME");

                if (lastMessage == null)
                {
                    UI.WriteMessage("Here you can play against yourself.");
                }
                else
                {
                    UI.WriteMessage(lastMessage, ConsoleColor.Yellow);
                }
                lastMessage = null;

                UI.WriteDivider();
                if(game.GetOutcome() == Outcome.InProgress)
                {
                    UI.WriteMessage(game.DebugCurrentMoveOrder());
                    UI.WriteMessage(game.DebugLegalMoveCount());
                    UI.WriteMessage(game.DebugWhiteCheck());
                    UI.WriteMessage(game.DebugBlackCheck());
                }
                else
                {
                    UI.WriteMessage(game.DebugOutcome());
                }
                UI.WriteBoard(game.DebugCurrentBoard());

                string action = UI.AskStringInput("Write a move in long algebraic notation (example: \"e2e4\") or type exit.");
                if (action == "exit") return;
                bool moveSuccess = game.TryUciMove(action);
                if (!moveSuccess)
                {
                    lastMessage = "Invalid command: \"" + action + "\".";
                }
            }
        }
    }
}
