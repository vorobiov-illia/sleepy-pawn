using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Utils
{
    internal static class PieceUtils
    {
        internal static readonly EnginePosition defaultWhiteKingPosition = new EnginePosition(4, 0);
        internal static readonly EnginePosition whiteKingShortCastle = new EnginePosition(6, 0);
        internal static readonly List<EnginePosition> whiteShortCastlingVunurableMap = new List<EnginePosition>() {
            new EnginePosition(4,0), new EnginePosition(5, 0), new EnginePosition(6, 0),
        };
        internal static readonly List<EnginePosition> whiteShortCastlingBlockMap = new List<EnginePosition>() {
            new EnginePosition(5, 0), new EnginePosition(6, 0),
        };
        internal static readonly EnginePosition whiteKingLongCastle = new EnginePosition(2, 0);
        internal static readonly List<EnginePosition> whiteLongCastlingVunurableMap = new List<EnginePosition>() {
            new EnginePosition(4,0), new EnginePosition(3, 0), new EnginePosition(2, 0),
        };
        internal static readonly List<EnginePosition> whiteLongCastlingBlockMap = new List<EnginePosition>() {
            new EnginePosition(3, 0), new EnginePosition(2, 0), new EnginePosition(1, 0),
        };
        internal static readonly EnginePosition defaultBlackKingPosition = new EnginePosition(4, 7);
        internal static readonly EnginePosition blackKingShortCastle = new EnginePosition(6, 7);
        internal static readonly List<EnginePosition> blackShortCastlingVunurableMap = new List<EnginePosition>() {
            new EnginePosition(4,7), new EnginePosition(5, 7), new EnginePosition(6, 7),
        };
        internal static readonly List<EnginePosition> blackShortCastlingBlockMap = new List<EnginePosition>() {
            new EnginePosition(5, 7), new EnginePosition(6, 7),
        };
        internal static readonly EnginePosition blackKingLongCastle = new EnginePosition(2, 7);
        internal static readonly List<EnginePosition> blackLongCastlingVunurableMap = new List<EnginePosition>() {
            new EnginePosition(4,7), new EnginePosition(3, 7), new EnginePosition(2, 7),
        };
        internal static readonly List<EnginePosition> blackLongCastlingBlockMap = new List<EnginePosition>() {
            new EnginePosition(3, 7), new EnginePosition(2, 7), new EnginePosition(1, 7),
        };

        internal static readonly EnginePosition[] rookVectors = { 
            new EnginePosition(0, 1), new EnginePosition(0, -1),
            new EnginePosition(1, 0), new EnginePosition(-1, 0)
        };
        internal static readonly EnginePosition[] bishopVectors = { 
            new EnginePosition(1, 1), new EnginePosition(1, -1),
            new EnginePosition(-1, 1), new EnginePosition(-1, -1)
        };
        internal static readonly EnginePosition[] knightOffsets = {
                new EnginePosition(1, 2),
                new EnginePosition(2, 1),
                new EnginePosition(2, -1),
                new EnginePosition(1, -2),
                new EnginePosition(-1, 2),
                new EnginePosition(-2, 1),
                new EnginePosition(-2, -1),
                new EnginePosition(-1, -2) 
        };
        internal static readonly EnginePosition[] kingOffsets = {
                new EnginePosition(0, 1),
                new EnginePosition(1, 1),
                new EnginePosition(1, 0),
                new EnginePosition(1, -1),
                new EnginePosition(0, -1),
                new EnginePosition(-1, -1),
                new EnginePosition(-1, 0),
                new EnginePosition(-1, 1)
        };

        internal static readonly EnginePosition whiteShortRook = new EnginePosition(7, 0);
        internal static readonly EnginePosition whiteShortRookAfterCastle = new EnginePosition(5, 0);
        internal static readonly EnginePosition whiteLongRook = new EnginePosition(0, 0);
        internal static readonly EnginePosition whiteLongRookAfterCastle = new EnginePosition(3, 0);
        internal static readonly EnginePosition blackShortRook = new EnginePosition(7, 7);
        internal static readonly EnginePosition blackShortRookAfterCastle = new EnginePosition(5, 7);
        internal static readonly EnginePosition blackLongRook = new EnginePosition(0, 7);
        internal static readonly EnginePosition blackLongRookAfterCastle = new EnginePosition(3, 7);

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
                if (!piece.isEmpty) return false;
                x += stepX;
                y += stepY;
            }

            return true;
        }
        internal static int GetSlideMoves(EnginePosition piecePosition, Board board, int xDir, int yDir, ref Span<Move> pseudoMoves, int count)
        {
            int newCount = count;
            
            int stepX = Math.Sign(xDir);
            int stepY = Math.Sign(yDir);

            int x = piecePosition.x + stepX;
            int y = piecePosition.y + stepY;

            while (x < 8 && y < 8 && x > -1 && y > -1)
            {
                EnginePosition currentPosition = new EnginePosition(x, y);
                Move newMove = new Move(piecePosition, currentPosition);
                pseudoMoves[newCount++] = newMove;

                Piece piece = board.GetPiece(currentPosition);
                if (!piece.isEmpty) break;

                x += stepX;
                y += stepY;
            }
            return newCount;
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
                if (!piece.isEmpty) break;

                x += stepX;
                y += stepY;
            }
        }
        internal static Piece GetPieceSlide(EnginePosition piecePosition, Board board, int xDir, int yDir)
        {
            int stepX = Math.Sign(xDir);
            int stepY = Math.Sign(yDir);

            int x = piecePosition.x + stepX;
            int y = piecePosition.y + stepY;

            while (x < 8 && y < 8 && x > -1 && y > -1)
            {
                Piece piece = board.GetPiece(new EnginePosition(x, y));
                if (!piece.isEmpty) return piece;

                x += stepX;
                y += stepY;
            }
            return new Piece();
        }
    }
}
