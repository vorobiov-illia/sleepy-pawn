namespace SleepyPawn.Core.Chess
{
    public class Game
    {
        private GameState currentState;
        public Game()
        {
            currentState = new GameState();
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
