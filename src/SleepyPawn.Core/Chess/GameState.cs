using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Chess.Rules;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess
{
    internal class GameState
    {
        private Board boardState;
        internal Color playerToMove;

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

        internal GameState AppendMove(Move move)
        {
            if (!move.isValid) return this;
            if (move.nullMove)
            {
                return new GameState(boardState, ColorUtils.Reverse(playerToMove));
            }
            Board changedBoard = new Board(boardState);
            changedBoard.ReplaceFigure(move.firstPos, move.secondPos);
            return new GameState(changedBoard, ColorUtils.Reverse(playerToMove));
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
