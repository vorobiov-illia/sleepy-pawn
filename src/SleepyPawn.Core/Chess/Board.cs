using SleepyPawn.Core.Chess.Enums;
using System.Text;

namespace SleepyPawn.Core.Chess
{
    internal class Board
    {
        internal Figure[,] figures = new Figure[8, 8];

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
}
