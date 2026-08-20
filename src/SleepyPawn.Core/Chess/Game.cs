using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Chess.Rules;
using SleepyPawn.Core.Utils;
using System.Security.AccessControl;

namespace SleepyPawn.Core.Chess
{
    public class Game
    {
        private GameState currentState;
        private bool testGame = false;
        public Game(bool test = false)
        {
            testGame = test;
            currentState = new GameState(testGame);
        }
        public bool TryMove(Move move)
        {
            if(LegalMoveAnalyzer.IsLegal(move, currentState))
            {
                currentState = currentState.AppendMove(move);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool TryUciMove(string uciMove)
        {
            Move move = new Move();
            move.FromUci(uciMove);

            return TryMove(move);
        }
        // For tests only
        public void AddFigure(string command)
        {
            if (command.Length != 4) return;

            int x = UciUtils.UciToEngineChar(command[0]);
            int y = UciUtils.UciToEngineChar(command[1]);

            EnginePosition position = new EnginePosition(x,y);

            Color color = Color.None;
            if(command[2] == 'w')
            {
                color = Color.White;
            }
            else if (command[2] == 'b')
            {
                color = Color.Black;
            }

            FigureType type = FigureType.None;
            switch (command[3])
            {
                case 'p':
                    type = FigureType.Sleepy;
                    break;
                case 'b':
                    type = FigureType.Bishop;
                    break;
                case 'n':
                    type = FigureType.Knight;
                    break;
                case 'r':
                    type = FigureType.Rook;
                    break;
                case 'q':
                    type = FigureType.Queen;
                    break;
                case 'k':
                    type = FigureType.King;
                    break;
                default:
                    return;
            }
            
            currentState.AddFigure(position, color, type);
        }
        public string DebugCurrentBoard()
        {
            return currentState.DebugBoard();
        }
        public string DebugCurrentMoveOrder()
        {
            return currentState.DebugPlayer();
        }
    }
}
