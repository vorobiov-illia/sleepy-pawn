using SleepyPawn.Core.Chess.Rules;
using System.Security.AccessControl;

namespace SleepyPawn.Core.Chess
{
    public class Game
    {
        private GameState currentState;
        public Game()
        {
            currentState = new GameState();
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

            if (LegalMoveAnalyzer.IsLegal(move, currentState))
            {
                currentState = currentState.AppendMove(move);
                return true;
            }
            else
            {
                return false;
            }
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
