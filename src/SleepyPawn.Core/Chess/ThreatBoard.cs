using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Chess.Rules;

namespace SleepyPawn.Core.Chess
{
    internal class ThreatBoard
    {
        private static PieceRuleset pieceRules = new PieceRuleset();
        internal bool[,] whiteThreats = new bool[8, 8];
        internal bool[,] blackThreats = new bool[8, 8];

        internal ThreatBoard()
        {
            Clear();
        }
        internal ThreatBoard(ThreatBoard other)
        {
            Clear();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    whiteThreats[i, j] = other.whiteThreats[i, j];
                    blackThreats[i, j] = other.blackThreats[i, j];
                }
            }
        }
        internal bool GetThreat(Color color, EnginePosition position)
        {
            if (color == Color.White)
            {
                return whiteThreats[position.y,position.x];
            }
            if (color == Color.Black)
            {
                return blackThreats[position.y, position.x];
            }
            return false;
        }
        internal void Clear()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    whiteThreats[i, j] = false;
                    blackThreats[i, j] = false;
                }
            }
        }
        internal void GenerateThreats(Board gameBoard)
        {
            EnginePosition position = new EnginePosition();
            for(int i = 0; i < 8; i++)
            {
                position.y = i;
                for (int j = 0; j < 8; j++)
                {
                    position.x = j;

                    pieceRules.GenerateThreat(position, gameBoard, this);
                }
            }
        }
        internal void AddThreat(EnginePosition position, Color color)
        {
            if (position.x > 7 || position.x < 0) return;
            if (position.y > 7 || position.y < 0) return;

            if (color == Color.White)
            {
                whiteThreats[position.y, position.x] = true;
            }
            if (color == Color.Black)
            {
                blackThreats[position.y, position.x] = true;
            }
        }
    }
}
