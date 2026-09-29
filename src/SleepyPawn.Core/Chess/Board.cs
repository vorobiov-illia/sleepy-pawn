using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;
using System.Text;

namespace SleepyPawn.Core.Chess
{
    internal class Board
    {
        internal Piece[] pieces = new Piece[64];

        internal Board() { }
        internal Board(Board other)
        {
            Array.Copy(other.pieces, pieces, 64);
        }
        internal Piece GetPiece(EnginePosition position)
        {
            if (!PositionCheck(position)) return new Piece();
            return pieces[BoardUtils.EnginePositionToIndex(position)];
        }
        internal void Clear()
        {
            Array.Copy(BoardUtils.emptyPieces, pieces, 64);
        }
        internal void SetupInitial()
        {
            Clear();

            // Placing white pawns
            for (int i = 0; i < 8; i++)
            {
                pieces[BoardUtils.EnginePositionToIndex(i, 1)] = new Piece(Color.White, PieceType.Sleepy);
            }

            // Placing black pawns
            for (int i = 0; i < 8; i++)
            {
                pieces[BoardUtils.EnginePositionToIndex(i, 6)] = new Piece(Color.Black, PieceType.Sleepy);
            }

            // Placing white king row
            pieces[0] = new Piece(Color.White, PieceType.Rook);
            pieces[BoardUtils.EnginePositionToIndex(1, 0)] = new Piece(Color.White, PieceType.Knight);
            pieces[BoardUtils.EnginePositionToIndex(2, 0)] = new Piece(Color.White, PieceType.Bishop);
            pieces[BoardUtils.EnginePositionToIndex(3, 0)] = new Piece(Color.White, PieceType.Queen);
            pieces[BoardUtils.EnginePositionToIndex(4, 0)] = new Piece(Color.White, PieceType.King);
            pieces[BoardUtils.EnginePositionToIndex(5, 0)] = new Piece(Color.White, PieceType.Bishop);
            pieces[BoardUtils.EnginePositionToIndex(6, 0)] = new Piece(Color.White, PieceType.Knight);
            pieces[BoardUtils.EnginePositionToIndex(7, 0)] = new Piece(Color.White, PieceType.Rook);

            // Placing black king row
            pieces[BoardUtils.EnginePositionToIndex(0, 7)] = new Piece(Color.Black, PieceType.Rook);
            pieces[BoardUtils.EnginePositionToIndex(1, 7)] = new Piece(Color.Black, PieceType.Knight);
            pieces[BoardUtils.EnginePositionToIndex(2, 7)] = new Piece(Color.Black, PieceType.Bishop);
            pieces[BoardUtils.EnginePositionToIndex(3, 7)] = new Piece(Color.Black, PieceType.Queen);
            pieces[BoardUtils.EnginePositionToIndex(4, 7)] = new Piece(Color.Black, PieceType.King);
            pieces[BoardUtils.EnginePositionToIndex(5, 7)] = new Piece(Color.Black, PieceType.Bishop);
            pieces[BoardUtils.EnginePositionToIndex(6, 7)] = new Piece(Color.Black, PieceType.Knight);
            pieces[BoardUtils.EnginePositionToIndex(7, 7)] = new Piece(Color.Black, PieceType.Rook);
        }

        internal void ReplacePiece(EnginePosition piece, EnginePosition newPosition, PieceType promotion = PieceType.None)
        {
            if (!PositionCheck(piece)) return;
            if (!PositionCheck(newPosition)) return;
            int pieceIndex = BoardUtils.EnginePositionToIndex(piece);
            pieces[BoardUtils.EnginePositionToIndex(newPosition)] = pieces[pieceIndex];
            pieces[pieceIndex] = new Piece();
            if(promotion != PieceType.None)
            {
                pieces[(newPosition.y * 8) + newPosition.x].type = promotion;
            }
        }

        internal void RemovePiece(EnginePosition position)
        {
            if (!PositionCheck(position)) return;
            pieces[BoardUtils.EnginePositionToIndex(position)] = new Piece();
        }

        internal bool IsTileAttacked(EnginePosition position, Color attacker)
        {
            if (attacker == Color.Black)
            {
                Piece leftPawn = GetPiece(new EnginePosition(position.x - 1, position.y + 1));
                if (leftPawn.color == Color.Black && leftPawn.type == PieceType.Sleepy) return true;
                Piece rightPawn = GetPiece(new EnginePosition(position.x + 1, position.y + 1));
                if (rightPawn.color == Color.Black && rightPawn.type == PieceType.Sleepy) return true;
            }
            else if (attacker == Color.White)
            {
                Piece leftPawn = GetPiece(new EnginePosition(position.x - 1, position.y - 1));
                if (leftPawn.color == Color.White && leftPawn.type == PieceType.Sleepy) return true;
                Piece rightPawn = GetPiece(new EnginePosition(position.x + 1, position.y - 1));
                if (rightPawn.color == Color.White && rightPawn.type == PieceType.Sleepy) return true;
            }
            else
            {
                return true;
            }

            foreach (EnginePosition vector in PieceUtils.rookVectors)
            {
                Piece potentialRook = PieceUtils.GetPieceSlide(position, this, vector.x, vector.y);
                if (potentialRook.color == attacker && (potentialRook.type == PieceType.Rook || potentialRook.type == PieceType.Queen))
                {
                    return true;
                }
            }

            foreach (EnginePosition vector in PieceUtils.bishopVectors)
            {
                Piece potentialBishop = PieceUtils.GetPieceSlide(position, this, vector.x, vector.y);
                if (potentialBishop.color == attacker && (potentialBishop.type == PieceType.Bishop || potentialBishop.type == PieceType.Queen))
                {
                    return true;
                }
            }

            foreach (EnginePosition offset in PieceUtils.knightOffsets)
            {
                Piece potentialKnight = GetPiece(new EnginePosition(position.x + offset.x, position.y + offset.y));
                if (potentialKnight.color == attacker && potentialKnight.type == PieceType.Knight)
                {
                    return true;
                }
            }

            foreach (EnginePosition offset in PieceUtils.kingOffsets)
            {
                Piece potenrialKing = GetPiece(new EnginePosition(position.x + offset.x, position.y + offset.y));
                if (potenrialKing.color == attacker && potenrialKing.type == PieceType.King)
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();

            for (int i = 7; i >= 0; i--)
            {
                res.Append((i + 1) + ": ");
                for (int j = 0; j<8; j++)
                {
                    int index = BoardUtils.EnginePositionToIndex(j, i);
                    if (pieces[index].isEmpty)
                    {
                        res.Append("[ ]");
                    }
                    else
                    {
                        res.Append("[" + pieces[index].ToString() + "]");
                    }
                }
                res.Append('\n');
            }
            res.Append("   ");
            for (int i = 0; i < 8; i++)
            {
                res.Append(" " + (char)(97 + i) + " ");
            }
            res.Append("\n");
            return res.ToString();
        }

        internal void AddPiece(EnginePosition position, Color color, PieceType type)
        {
            if (!PositionCheck(position)) return;
            pieces[BoardUtils.EnginePositionToIndex(position)] = new Piece(color, type);
        }

        internal void AddPiece(EnginePosition position, Piece piece)
        {
            if (!PositionCheck(position)) return;
            pieces[BoardUtils.EnginePositionToIndex(position)] = new Piece(piece.color, piece.type);
        }

        internal bool PositionCheck(EnginePosition position)
        {
            if (position.x > 7 || position.x < 0) return false;
            if (position.y > 7 || position.y < 0) return false;

            return true;
        }

        internal EnginePosition FindKing(Color color)
        {
            EnginePosition kingPosition = BoardUtils.IllegalPosition;

            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int index = BoardUtils.EnginePositionToIndex(j, i);
                    if (pieces[index].type == PieceType.King && pieces[index].color == color)
                    {
                        kingPosition.x = j;
                        kingPosition.y = i;

                        return kingPosition;
                    }
                }
            }
            return kingPosition;
        }
    }

    public struct EnginePosition
    {
        public int x;
        public int y;
        public EnginePosition()
        {
            x = 0;
            y = 0;
        }
        public EnginePosition(EnginePosition other)
        {
            x = other.x;
            y = other.y;
        }
        public EnginePosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public static bool operator ==(EnginePosition left, EnginePosition right)
        {
            return left.x == right.x && left.y == right.y;
        }
        public static bool operator !=(EnginePosition left, EnginePosition right)
        {
            return !(left == right);
        }
        public override bool Equals(object? obj)
        {
            if (obj is EnginePosition other)
            {
                return this == other;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
    }
}
