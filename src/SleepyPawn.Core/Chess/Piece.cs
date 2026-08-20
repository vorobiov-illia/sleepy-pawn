using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess
{
    internal class Piece
    {
        internal bool isEmpty { get; private set; } = true;
        internal bool Moved { get; set; } = false;
        internal Color color { get; private set; }
        internal PieceType type { get; set; }

        internal Piece()
        {
            color = Color.None;
            type = PieceType.None;
            isEmpty = true;
        }
        internal Piece(Color c, PieceType t, bool moved = false, bool empty = false)
        {
            color = c;
            type = t;
            isEmpty = empty;
            Moved = moved;
        }

        public override string ToString()
        {
            if (isEmpty) return " ";
            switch (color)
            {
                case Color.White:
                    switch (type)
                    {
                        case PieceType.Sleepy:
                            return "P";
                        case PieceType.Bishop:
                            return "B";
                        case PieceType.Knight:
                            return "N";
                        case PieceType.Rook:
                            return "R";
                        case PieceType.Queen:
                            return "Q";
                        case PieceType.King:
                            return "K";
                        default:
                            return "E";
                    }
                case Color.Black:
                    switch (type)
                    {
                        case PieceType.Sleepy:
                            return "p";
                        case PieceType.Bishop:
                            return "b";
                        case PieceType.Knight:
                            return "n";
                        case PieceType.Rook:
                            return "r";
                        case PieceType.Queen:
                            return "q";
                        case PieceType.King:
                            return "k";
                        default:
                            return "e";
                    }
                default:
                    return "-";
            }
        }
    }
}
