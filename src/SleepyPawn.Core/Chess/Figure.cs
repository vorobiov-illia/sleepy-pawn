using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess
{
    internal class Figure
    {
        public bool isEmpty { get; private set; }
        public Color color { get; private set; }
        public FigureType type { get; protected set; }

        public Figure()
        {
            color = Color.None;
            type = FigureType.None;
            isEmpty = true;
        }
        public Figure(Color c, FigureType t)
        {
            color = c;
            type = t;
            isEmpty = false;
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
