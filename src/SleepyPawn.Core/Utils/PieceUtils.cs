using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Utils
{
    internal static class PieceUtils
    {
        internal static bool CheckSlide(EnginePosition start, EnginePosition end, Board board)
        {
            int distance = Math.Max(Math.Abs(end.x - start.x), Math.Abs(end.y - start.y));
            
            int stepX = Math.Sign(end.x - start.x);
            int stepY = Math.Sign(end.y - start.y);

            int x = start.x + stepX;
            int y = start.y + stepY;

            for(int i = 1; i < distance; i++)
            {
                Piece piece = board.GetPiece(new EnginePosition(x, y));
                if (piece != null)
                {
                    if (!piece.isEmpty) return false;
                }
                x += stepX;
                y += stepY;
            }

            return true;
        }
        internal static void SlideThreat(EnginePosition piecePosition, Board board, ThreatBoard threats, int xDir, int yDir, Color color)
        {
            int stepX = Math.Sign(xDir);
            int stepY = Math.Sign(yDir);

            int x = piecePosition.x + stepX;
            int y = piecePosition.y + stepY;

            while (x < 8 && y < 8 && x > -1 && y > -1)
            {
                threats.AddThreat(new EnginePosition(x,y), color);

                Piece piece = board.GetPiece(new EnginePosition(x, y));
                if (piece != null)
                {
                    if (!piece.isEmpty) break;
                }

                x += stepX;
                y += stepY;
            }
        }
    }
}
