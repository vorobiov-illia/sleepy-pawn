using SleepyPawn.Core.Chess.Enums;
using System.Text;

namespace SleepyPawn.Core.Chess
{
    internal class Board
    {
        internal Figure[,] figures = new Figure[8, 8];

        internal Board()
        {
            Clear();
        }
        internal Board(Board other)
        {
            Clear();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (other.figures[i, j] == null) continue;
                    figures[i, j] = new Figure(other.figures[i, j].color, other.figures[i, j].type, other.figures[i, j].isEmpty);
                }
            }
        }

        internal void Clear()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    figures[i, j] = new Figure();
                }
            }
        }
        internal void SetupStandard()
        {
            Clear();

            // Placing white pawns
            for (int i = 0; i < 8; i++)
            {
                figures[1, i] = new Figure(Color.White, FigureType.Sleepy);
            }

            // Placing black pawns
            for (int i = 0; i < 8; i++)
            {
                figures[6, i] = new Figure(Color.Black, FigureType.Sleepy);
            }

            // Placing white king row
            figures[0, 0] = new Figure(Color.White, FigureType.Rook);
            figures[0, 1] = new Figure(Color.White, FigureType.Knight);
            figures[0, 2] = new Figure(Color.White, FigureType.Bishop);
            figures[0, 3] = new Figure(Color.White, FigureType.Queen);
            figures[0, 4] = new Figure(Color.White, FigureType.King);
            figures[0, 5] = new Figure(Color.White, FigureType.Bishop);
            figures[0, 6] = new Figure(Color.White, FigureType.Knight);
            figures[0, 7] = new Figure(Color.White, FigureType.Rook);

            // Placing black king row
            figures[7, 0] = new Figure(Color.Black, FigureType.Rook);
            figures[7, 1] = new Figure(Color.Black, FigureType.Knight);
            figures[7, 2] = new Figure(Color.Black, FigureType.Bishop);
            figures[7, 3] = new Figure(Color.Black, FigureType.Queen);
            figures[7, 4] = new Figure(Color.Black, FigureType.King);
            figures[7, 5] = new Figure(Color.Black, FigureType.Bishop);
            figures[7, 6] = new Figure(Color.Black, FigureType.Knight);
            figures[7, 7] = new Figure(Color.Black, FigureType.Rook);
        }

        internal void ReplaceFigure(EnginePosition figure, EnginePosition newPosition)
        {
            figures[newPosition.y, newPosition.x] = figures[figure.y, figure.x];
            figures[figure.y, figure.x] = new Figure();
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();

            for (int i = 7; i >= 0; i--)
            {
                for(int j = 0; j<8; j++)
                {
                    if (figures[i,j] == null)
                    {
                        res.Append("[ ]");
                    }
                    else
                    {
                        res.Append("[" + figures[i, j].ToString() + "]");
                    }
                }
                res.Append('\n');
            }

            return res.ToString();
        }
    }

    internal struct EnginePosition
    {
        public int x;
        public int y;
        public EnginePosition()
        {
            x = 0;
            y = 0;
        }
        public EnginePosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
