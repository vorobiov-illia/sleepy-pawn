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
        public void AddPiece(string command)
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

            PieceType type = PieceType.None;
            switch (command[3])
            {
                case 'p':
                    type = PieceType.Sleepy;
                    break;
                case 'b':
                    type = PieceType.Bishop;
                    break;
                case 'n':
                    type = PieceType.Knight;
                    break;
                case 'r':
                    type = PieceType.Rook;
                    break;
                case 'q':
                    type = PieceType.Queen;
                    break;
                case 'k':
                    type = PieceType.King;
                    break;
                default:
                    return;
            }
            
            currentState.AddPiece(position, color, type);
            currentState.GenerateThreats();
        }
        public string GetPiece(string uciPosition)
        {
            string answer = "00";
            if (uciPosition.Length != 2) return answer;

            int x = UciUtils.UciToEngineChar(uciPosition[0]);
            int y = UciUtils.UciToEngineChar(uciPosition[1]);

            EnginePosition position = new EnginePosition(x, y);

            
            Piece piece = currentState.GetPiece(position);

            if (piece == null) return answer;
            if (piece.isEmpty) return answer;
            if (piece.type == PieceType.None) return answer;
            if (piece.color == Color.None) return answer;

            switch (piece.color)
            {
                case Color.White:
                    answer = "w";
                    break;
                case Color.Black:
                    answer = "b";
                    break;
                default:
                    return answer;
            }
            switch (piece.type)
            {
                case PieceType.Sleepy:
                    answer += "s";
                    break;
                case PieceType.Bishop:
                    answer += "b";
                    break;
                case PieceType.Knight:
                    answer += "n";
                    break;
                case PieceType.Rook:
                    answer += "r";
                    break;
                case PieceType.Queen:
                    answer += "q";
                    break;
                case PieceType.King:
                    answer += "k";
                    break;
                default:
                    return answer;
            }

            return answer;
        }
        public string DebugCurrentBoard()
        {
            return currentState.DebugBoard();
        }
        public string DebugCurrentMoveOrder()
        {
            return currentState.DebugMoveOrder();
        }
        public string DebugWhiteCheck()
        {
            return currentState.DebugCheck(Color.White);
        }
        public string DebugBlackCheck()
        {
            return currentState.DebugCheck(Color.Black);
        }
    }
}
