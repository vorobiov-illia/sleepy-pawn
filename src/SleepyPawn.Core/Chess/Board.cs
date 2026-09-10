using SleepyPawn.Core.Chess.Enums;
using System.Text;

namespace SleepyPawn.Core.Chess
{
    internal class Board
    {
        internal Piece[,] pieces = new Piece[8, 8];

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
                    if (other.pieces[i, j] == null) continue;
                    pieces[i, j] = new Piece(other.pieces[i, j].color, other.pieces[i, j].type, other.pieces[i, j].isEmpty);
                }
            }
        }
        internal Piece GetPiece(EnginePosition position)
        {
            return pieces[position.y, position.x];
        }
        internal void Clear()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    pieces[i, j] = new Piece();
                }
            }
        }
        internal void SetupStandard()
        {
            Clear();

            // Placing white pawns
            for (int i = 0; i < 8; i++)
            {
                pieces[1, i] = new Piece(Color.White, PieceType.Sleepy);
            }

            // Placing black pawns
            for (int i = 0; i < 8; i++)
            {
                pieces[6, i] = new Piece(Color.Black, PieceType.Sleepy);
            }

            // Placing white king row
            pieces[0, 0] = new Piece(Color.White, PieceType.Rook);
            pieces[0, 1] = new Piece(Color.White, PieceType.Knight);
            pieces[0, 2] = new Piece(Color.White, PieceType.Bishop);
            pieces[0, 3] = new Piece(Color.White, PieceType.Queen);
            pieces[0, 4] = new Piece(Color.White, PieceType.King);
            pieces[0, 5] = new Piece(Color.White, PieceType.Bishop);
            pieces[0, 6] = new Piece(Color.White, PieceType.Knight);
            pieces[0, 7] = new Piece(Color.White, PieceType.Rook);

            // Placing black king row
            pieces[7, 0] = new Piece(Color.Black, PieceType.Rook);
            pieces[7, 1] = new Piece(Color.Black, PieceType.Knight);
            pieces[7, 2] = new Piece(Color.Black, PieceType.Bishop);
            pieces[7, 3] = new Piece(Color.Black, PieceType.Queen);
            pieces[7, 4] = new Piece(Color.Black, PieceType.King);
            pieces[7, 5] = new Piece(Color.Black, PieceType.Bishop);
            pieces[7, 6] = new Piece(Color.Black, PieceType.Knight);
            pieces[7, 7] = new Piece(Color.Black, PieceType.Rook);
        }

        internal void ReplacePiece(EnginePosition piece, EnginePosition newPosition, PieceType promotion = PieceType.None)
        {
            if (!PositionCheck(piece)) return;
            if (!PositionCheck(newPosition)) return;
            pieces[newPosition.y, newPosition.x] = pieces[piece.y, piece.x];
            pieces[piece.y, piece.x] = new Piece();
            if(promotion != PieceType.None)
            {
                pieces[newPosition.y, newPosition.x].type = promotion;
            }
        }

        internal void RemovePiece(EnginePosition position)
        {
            if (!PositionCheck(position)) return;
            pieces[position.y, position.x] = new Piece();
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();

            for (int i = 7; i >= 0; i--)
            {
                for(int j = 0; j<8; j++)
                {
                    if (pieces[i,j] == null)
                    {
                        res.Append("[ ]");
                    }
                    else
                    {
                        res.Append("[" + pieces[i, j].ToString() + "]");
                    }
                }
                res.Append('\n');
            }

            return res.ToString();
        }

        internal void AddPiece(EnginePosition position, Color color, PieceType type)
        {
            if (!PositionCheck(position)) return;
            pieces[position.y, position.x] = new Piece(color, type);
        }

        internal void AddPiece(EnginePosition position, Piece piece)
        {
            if (!PositionCheck(position)) return;
            pieces[position.y, position.x] = new Piece(piece.color, piece.type);
        }

        internal bool PositionCheck(EnginePosition position)
        {
            if (position.x > 7 || position.x < 0) return false;
            if (position.y > 7 || position.y < 0) return false;

            return true;
        }

        internal EnginePosition FindKing(Color color)
        {
            EnginePosition kingPosition = new EnginePosition(-1, -1);

            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (pieces[i, j].type == PieceType.King && pieces[i,j].color == color)
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
