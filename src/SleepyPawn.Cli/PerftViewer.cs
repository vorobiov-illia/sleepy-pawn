using SleepyPawn.Cli.Utils;
using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Utils;
using System.Diagnostics;

namespace SleepyPawn.Cli
{
    
    internal class PerftViewer
    {
        string? lastFen = null;
        internal PerftViewer(string? fen = null)
        {
            if(fen != null)
            {
                lastFen = fen;
            }
        }
        static string mainMenuTitle = "What position you would like to test with Perft?";
        static string[] mainMenuOptions = { 
            "initial",
            "kiwipete",
            "pn3",
            "pn4",
            "custom",
            "return",
        };
        static string[] mainMenuComments = {
            "Default chess position that you can observe at the start of any game.",
            "Hardcore position designed to make engines sweat.",
            "Third position from chessprogramming.org.",
            "Fourth position from chessprogramming.org.",
            "Enter your own FEN position.",
            "Close perft tester.",
        };
        static string threadMenuTitle = "Do you like to run this test in Multithread mode?";
        static string[] threadMenuOptions = {
            "y",
            "n",
        };
        static string[] threadMenuComments = {
            "Run Perft test in Multithread mode.",
            "Run Perft test in Singlethread mode."
        };
        static string depthMenuTitle = "Select depth for this test.";
        static string[] depthMenuOptions = {
            "Depth 1",
            "Depth 2",
            "Depth 3",
            "Depth 4",
            "Depth 5",
            "Depth 6",
        };
        static string[] depthMenuComments = {
            "Run Perft test with depth 1.",
            "Run Perft test with depth 2.",
            "Run Perft test with depth 3.",
            "Run Perft test with depth 4.",
            "Run Perft test with depth 5.",
            "Run Perft test with depth 6.",
        };
        internal void Play()
        {
            string? lastMessage = null;

            Game? game = null;
            string positionName = "Custom";
            int depth = 0;

            while (true)
            {
                UI.Clear();
                UI.WriteDivider();
                UI.WriteHeader("PERFT TESTER");

                if (lastMessage == null)
                {
                    UI.WriteMessage("Here you can run Perft on various FEN positions.");
                }
                else
                {
                    UI.WriteMessage(lastMessage, ConsoleColor.Yellow);
                }
                lastMessage = null;

                if(lastFen != null)
                {
                    game = new Game(lastFen, true);
                    lastFen = null;
                }

                if (game == null)
                {
                    string fenMenuInput = UI.AskInput(mainMenuTitle, mainMenuOptions, mainMenuComments);
                    positionName = fenMenuInput;
                    switch (fenMenuInput)
                    {
                        case "initial":
                            game = new Game(true);
                            game.SetupInitialPosition();
                            break;
                        case "kiwipete":
                            game = new Game("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - ", true);
                            break;
                        case "pn3":
                            game = new Game("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 1 ", true);
                            break;
                        case "pn4":
                            game = new Game("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", true);
                            break;
                        case "custom":
                            string action = UI.AskStringInput("Write a FEN position (example: \"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1\") or type return.");
                            if (action == "return") return;
                            bool fenValid = FenUtils.IsValid(action);
                            if (!fenValid)
                            {
                                lastMessage = "Invalid FEN string: \"" + action + "\".";
                                continue;
                            }
                            lastFen = action;
                            game = new Game(action, true);
                            break;
                        case "return":
                            return;
                        default:
                            lastMessage = "Sorry, not implemented yet...";
                            UI.WriteMessage("Sorry, not implemented yet...");
                            break;
                    }
                }
                else
                {
                    UI.WriteDivider();
                    if(depth != 0)
                    {
                        UI.WriteHeader("Position: " + positionName + " (" + depth + ")");
                    }
                    else
                    {
                        UI.WriteHeader("Position: " + positionName);
                    }
                    UI.WriteMessage(game.DebugCurrentMoveOrder());
                    UI.WriteBoard(game.DebugCurrentBoard());

                    if(depth == 0)
                    {
                        string depthInput = UI.AskInput(depthMenuTitle, depthMenuOptions, depthMenuComments);
                        switch (depthInput)
                        {
                            case "Depth 1":
                                depth = 1;
                                continue;
                            case "Depth 2":
                                depth = 2;
                                continue;
                            case "Depth 3":
                                depth = 3;
                                continue;
                            case "Depth 4":
                                depth = 4;
                                continue;
                            case "Depth 5":
                                depth = 5;
                                continue;
                            case "Depth 6":
                                depth = 6;
                                continue;
                            default:
                                lastMessage = "Sorry, not implemented yet...";
                                UI.WriteMessage("Sorry, not implemented yet...");
                                continue;
                        }
                    }
                    else
                    {
                        string multithreadInput = UI.AskInput(threadMenuTitle, threadMenuOptions, threadMenuComments);
                        switch (multithreadInput)
                        {
                            case "y":
                                RunPerft(game, depth, true, positionName);
                                lastFen = null;
                                game = null;
                                depth = 0;
                                return;
                            case "n":
                                RunPerft(game, depth, false, positionName);
                                lastFen = null;
                                game = null;
                                depth = 0;
                                return;
                            default:
                                lastMessage = "Sorry, not implemented yet...";
                                UI.WriteMessage("Sorry, not implemented yet...");
                                continue;
                        }
                    }
                }
            }
        }
        private void RunPerft(Game game, int depth, bool multithread, string positionName)
        {
            UI.Clear();
            UI.WriteDivider();
            UI.WriteHeader("Loading...");
            UI.WriteMessage("This can take some time. Perft tests are computationally heavy.");
            UI.WriteDivider();

            Stopwatch sw = Stopwatch.StartNew();

            ulong count;
            if (multithread)
            {
                count = PerftUtils.Perft(game, depth);
            }
            else
            {
                count = PerftUtils.PerftSingleCore(game, depth);
            }
             
            sw.Stop();

            UI.Clear();
            UI.WriteDivider();
            UI.WriteHeader("PERFT TESTER");
            UI.WriteDivider();
            UI.WriteHeader("Position: " + positionName + " (" + depth + ")");
            if (multithread)
            {
                UI.WriteMessage("Threads: " + Environment.ProcessorCount);
            }
            else
            {
                UI.WriteMessage("Single thread mode.");
            }
            UI.WriteMessage(game.DebugCurrentMoveOrder());
            UI.WriteBoard(game.DebugCurrentBoard());

            UI.WriteMessage("Total nodes count: " + count);

            double seconds = sw.Elapsed.TotalSeconds;

            if (seconds < 0.001)
            {
                UI.WriteMessage("Est. time: < 1 ms");
                UI.WriteMessage("MNPS: N/A (too fast)");
            }
            else
            {
                double mnps = (count / seconds) / 1_000_000.0;

                UI.WriteMessage($"Est. time: {seconds:F3} seconds");
                UI.WriteMessage($"MNPS: {mnps:F2}");
            }


            UI.WaitUserInput();
        }
    }
}
