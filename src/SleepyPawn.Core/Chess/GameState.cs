using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess
{
    internal class GameState
    {
        Board boardState;
        Color playerToMove;

        internal GameState()
        {
            boardState = new Board();
            boardState.SetupStandard();
            playerToMove = Color.White;
        }

        internal GameState(Board board, Color player)
        {
            boardState = board;
            playerToMove = player;
        }

        internal string DebugPlayer()
        {
            string template = "'s time to move.";
            if(playerToMove == Color.White)
            {
                return "White" + template;
            }
            else if (playerToMove == Color.Black)
            {
                return "Black" + template;
            }
            else
            {
                return "Game is over.";
            }
        }

        internal string DebugBoard()
        {
            return boardState.ToString();
        }

        public override string ToString()
        {
            return boardState.ToString();
        }
    }
}
