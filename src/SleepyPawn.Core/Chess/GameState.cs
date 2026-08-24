using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess
{
    internal class GameState
    {
        internal Board boardState;
        internal Color playerToMove;
        internal bool whiteCastlingRetained = true;
        internal bool whiteLongCastlingRetained = true;
        internal bool blackCastlingRetained = true;
        internal bool blackLongCastlingRetained = true;

        internal GameState(bool emptyBoard = false)
        {
            boardState = new Board();
            if(!emptyBoard) boardState.SetupStandard();
            playerToMove = Color.White;
        }

        internal GameState(Board board, Color player)
        {
            boardState = board;
            playerToMove = player;
        }

        internal Piece GetPiece(EnginePosition position)
        {
            return boardState.GetPiece(position);
        }
        internal void AddPiece(EnginePosition position, Color color, PieceType type)
        {
            boardState.AddPiece(position, color, type);
        }
        internal GameState AppendMove(Move move)
        {
            if (!move.isValid) return this;
            if (move.nullMove)
            {
                return new GameState(boardState, ColorUtils.Reverse(playerToMove));
            }
            Board changedBoard = new Board(boardState);
            changedBoard.ReplacePiece(move.firstPos, move.secondPos);
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
