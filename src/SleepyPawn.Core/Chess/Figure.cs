using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess
{
    internal class Figure
    {
        internal bool isEmpty { get; private set; } = true;
        internal bool Moved { get; set; } = false;
        internal Color color { get; private set; }
        internal FigureType type { get; set; }

        internal Figure()
        {
            color = Color.None;
            type = FigureType.None;
            isEmpty = true;
        }
        internal Figure(Color c, FigureType t, bool moved = false, bool empty = false)
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
                        case FigureType.Sleepy:
                            return "P";
                        case FigureType.Bishop:
                            return "B";
                        case FigureType.Knight:
                            return "N";
                        case FigureType.Rook:
                            return "R";
                        case FigureType.Queen:
                            return "Q";
                        case FigureType.King:
                            return "K";
                        default:
                            return "E";
                    }
                case Color.Black:
                    switch (type)
                    {
                        case FigureType.Sleepy:
                            return "p";
                        case FigureType.Bishop:
                            return "b";
                        case FigureType.Knight:
                            return "n";
                        case FigureType.Rook:
                            return "r";
                        case FigureType.Queen:
                            return "q";
                        case FigureType.King:
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
