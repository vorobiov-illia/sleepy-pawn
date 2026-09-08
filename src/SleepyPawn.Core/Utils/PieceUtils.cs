using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Utils
{
    internal static class PieceUtils
    {
        internal static EnginePosition defaultWhiteKingPosition = new EnginePosition(4, 0);
        internal static EnginePosition whiteKingShortCastle = new EnginePosition(6, 0);
        internal static List<EnginePosition> whiteShortCastlingVunurableMap = new List<EnginePosition>() {
            new EnginePosition(4,0), new EnginePosition(5, 0), new EnginePosition(6, 0),
        };
        internal static List<EnginePosition> whiteShortCastlingBlockMap = new List<EnginePosition>() {
            new EnginePosition(5, 0), new EnginePosition(6, 0),
        };
        internal static EnginePosition whiteKingLongCastle = new EnginePosition(2, 0);
        internal static List<EnginePosition> whiteLongCastlingVunurableMap = new List<EnginePosition>() {
            new EnginePosition(4,0), new EnginePosition(3, 0), new EnginePosition(2, 0),
        };
        internal static List<EnginePosition> whiteLongCastlingBlockMap = new List<EnginePosition>() {
            new EnginePosition(3, 0), new EnginePosition(2, 0), new EnginePosition(1, 0),
        };
        internal static EnginePosition defaultBlackKingPosition = new EnginePosition(4, 7);
        internal static EnginePosition blackKingShortCastle = new EnginePosition(6, 7);
        internal static List<EnginePosition> blackShortCastlingVunurableMap = new List<EnginePosition>() {
            new EnginePosition(4,7), new EnginePosition(5, 7), new EnginePosition(6, 7),
        };
        internal static List<EnginePosition> blackShortCastlingBlockMap = new List<EnginePosition>() {
            new EnginePosition(5, 7), new EnginePosition(6, 7),
        };
        internal static EnginePosition blackKingLongCastle = new EnginePosition(2, 7);
        internal static List<EnginePosition> blackLongCastlingVunurableMap = new List<EnginePosition>() {
            new EnginePosition(4,7), new EnginePosition(3, 7), new EnginePosition(2, 7),
        };
        internal static List<EnginePosition> blackLongCastlingBlockMap = new List<EnginePosition>() {
            new EnginePosition(3, 7), new EnginePosition(2, 7), new EnginePosition(1, 7),
        };

        internal static EnginePosition whiteShortRook = new EnginePosition(7, 0);
        internal static EnginePosition whiteShortRookAfterCastle = new EnginePosition(5, 0);
        internal static EnginePosition whiteLongRook = new EnginePosition(0, 0);
        internal static EnginePosition whiteLongRookAfterCastle = new EnginePosition(3, 0);
        internal static EnginePosition blackShortRook = new EnginePosition(7, 7);
        internal static EnginePosition blackShortRookAfterCastle = new EnginePosition(5, 7);
        internal static EnginePosition blackLongRook = new EnginePosition(0, 7);
        internal static EnginePosition blackLongRookAfterCastle = new EnginePosition(3, 7);

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
        internal static List<Move> GetSlideMoves(EnginePosition piecePosition, Board board, int xDir, int yDir)
        {
            List<Move> moves = new List<Move>();

            int stepX = Math.Sign(xDir);
            int stepY = Math.Sign(yDir);

            int x = piecePosition.x + stepX;
            int y = piecePosition.y + stepY;

            while (x < 8 && y < 8 && x > -1 && y > -1)
            {
                EnginePosition currentPosition = new EnginePosition(x, y);
                Move newMove = new Move(piecePosition, currentPosition);
                moves.Add(newMove);

                Piece piece = board.GetPiece(currentPosition);
                if (piece != null)
                {
                    if (!piece.isEmpty) break;
                }

                x += stepX;
                y += stepY;
            }
            return moves;
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
