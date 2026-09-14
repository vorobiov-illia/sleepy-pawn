using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Chess.Rules;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess
{
    public class SleepyChess
    {
        private ChessState currentState;
        private bool testGame = false;
        public SleepyChess(bool test = false)
        {
            testGame = test;
            currentState = new ChessState(testGame);
        }
        public SleepyChess(SleepyChess other)
        {
            testGame = other.testGame;
            currentState = new ChessState(other.currentState);
        }
        public SleepyChess(string fen, bool test = false)
        {
            testGame = test;
            currentState = new ChessState(fen);
        }

        public bool WhiteKingChecked()
        {
            return currentState.KingChecked(Color.White);
        }
        public bool BlackKingChecked()
        {
            return currentState.KingChecked(Color.Black);
        }

        public bool ActiveKingChecked()
        {
            return currentState.KingChecked(currentState.playerToMove);
        }
        public bool PassiveKingChecked()
        {
            return currentState.KingChecked(ColorUtils.Reverse(currentState.playerToMove));
        }

        public bool TryMove(Move move)
        {
            if(LegalMoveAnalyzer.IsLegal(move, currentState))
            {
                currentState.AppendMove(move);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ForceMove(Move move)
        {
            currentState.AppendMove(move);
        }
        public void UndoMove()
        {
            currentState.UndoMove();
        }
        public bool TryUciMove(string uciMove)
        {
            Move move = new Move();
            move.FromUci(uciMove);

            return TryMove(move);
        }

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
                    if (color == Color.White)
                    {
                        currentState.info.whiteKingPosition = position;
                    }
                    if (color == Color.Black)
                    {
                        currentState.info.blackKingPosition = position;
                    }
                    break;
                default:
                    return;
            }
            
            currentState.AddPiece(position, color, type);
        }
        public string GetPiece(string uciPosition)
        {
            string answer = "00";
            if (uciPosition.Length != 2) return answer;

            int x = UciUtils.UciToEngineChar(uciPosition[0]);
            int y = UciUtils.UciToEngineChar(uciPosition[1]);

            EnginePosition position = new EnginePosition(x, y);

            
            Piece piece = currentState.GetPiece(position);

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
        public string DebugLegalMoveCount()
        {
            Span<Move> moves = stackalloc Move[256];
            return "Legal moves for this position: " + currentState.GetLegalMoves(ref moves);
        }
        public string DebugWhiteCheck()
        {
            return currentState.DebugCheck(Color.White);
        }
        public string DebugBlackCheck()
        {
            return currentState.DebugCheck(Color.Black);
        }
        public int GetLegalMoves(ref Span<Move> moves)
        {
            return currentState.GetLegalMoves(ref moves);
        }
        public int GetPseudoMoves(ref Span<Move> moves)
        {
            return currentState.GetPseudoMoves(ref moves);
        }

        public void SetupInitialPosition()
        {
            currentState = new ChessState();
        }
    }
}
