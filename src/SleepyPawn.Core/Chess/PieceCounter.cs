using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess
{
    internal class PieceCounter
    {
        // White pieces
        private ushort whitePawns;
        private ushort whiteLightBishops;
        private ushort whiteDarkBishops;
        private ushort whiteKnights;
        private ushort whiteRooks;
        private ushort whiteQueens;
        private ushort whiteKings;

        // Black pieces
        private ushort blackPawns;
        private ushort blackLightBishops;
        private ushort blackDarkBishops;
        private ushort blackKnights;
        private ushort blackRooks;
        private ushort blackQueens;
        private ushort blackKings;

        internal PieceCounter()
        {
            whitePawns = 0;
            whiteLightBishops = 0;
            whiteDarkBishops = 0;
            whiteKnights = 0;
            whiteRooks = 0;
            whiteQueens = 0;
            whiteKings = 0;
            blackPawns = 0;
            blackLightBishops = 0;
            blackDarkBishops = 0;
            blackKnights = 0;
            blackRooks = 0;
            blackQueens = 0;
            blackKings = 0;
        }
        internal PieceCounter(PieceCounter other)
        {
            whitePawns = other.whitePawns;
            whiteLightBishops = other.whiteLightBishops;
            whiteDarkBishops = other.whiteDarkBishops;
            whiteKnights = other.whiteKnights;
            whiteRooks = other.whiteRooks;
            whiteQueens = other.whiteQueens;
            whiteKings = other.whiteKings;
            blackPawns = other.blackPawns;
            blackLightBishops = other.blackLightBishops;
            blackDarkBishops = other.blackDarkBishops;
            blackKnights = other.blackKnights;
            blackRooks = other.blackRooks;
            blackQueens = other.blackQueens;
            blackKings = other.blackKings;
        }
        
        internal void SetupInitial()
        {
            whitePawns = 8;
            whiteLightBishops = 1;
            whiteDarkBishops = 1;
            whiteKnights = 2;
            whiteRooks = 2;
            whiteQueens = 1;
            whiteKings = 1;

            blackPawns = 8;
            blackLightBishops = 1;
            blackDarkBishops = 1;
            blackKnights = 2;
            blackRooks = 2;
            blackQueens = 1;
            blackKings = 1;
        }

        internal int GetMaterial(Color player)
        {
            if (player == Color.White)
            {
                return whitePawns
                    + whiteLightBishops * 3
                    + whiteDarkBishops * 3
                    + whiteKnights * 3
                    + whiteRooks * 5
                    + whiteQueens * 9;
            }
            else if(player == Color.Black)
            {
                return blackPawns
                    + blackLightBishops * 3
                    + blackDarkBishops * 3
                    + blackKnights * 3
                    + blackRooks * 5
                    + blackQueens * 9;
            }
            return 0;
        }
        internal void CalculateManualy(Board board)
        {
            for(int i = 0; i < 64; i++)
            {
                Piece piece = board.pieces[i];
                if (piece.isEmpty) continue;
                if (piece.color == Color.White)
                {
                    switch (piece.type)
                    {
                        case PieceType.Sleepy:
                            whitePawns++;
                            break;
                        case PieceType.Bishop:
                            if (((i % 8) + (i / 8)) % 2 == 0)
                            {
                                whiteDarkBishops++;
                            }
                            else
                            {
                                whiteLightBishops++;
                            }
                            break;
                        case PieceType.Knight:
                            whiteKnights++;
                            break;
                        case PieceType.Rook:
                            whiteRooks++;
                            break;
                        case PieceType.Queen:
                            whiteQueens++;
                            break;
                        case PieceType.King:
                            whiteKings++;
                            break;
                    }
                }
                else if (piece.color == Color.Black)
                {
                    switch (piece.type)
                    {
                        case PieceType.Sleepy:
                            blackPawns++;
                            break;
                        case PieceType.Bishop:
                            if (((i % 8) + (i / 8)) % 2 == 0)
                            {
                                blackDarkBishops++;
                            }
                            else
                            {
                                blackLightBishops++;
                            }
                            break;
                        case PieceType.Knight:
                            blackKnights++;
                            break;
                        case PieceType.Rook:
                            blackRooks++;
                            break;
                        case PieceType.Queen:
                            blackQueens++;
                            break;
                        case PieceType.King:
                            blackKings++;
                            break;
                    }
                }
            }
        }
        internal void AddPiece(EnginePosition position, Piece piece)
        {
            if (piece.isEmpty) return;
            if (piece.color == Color.White)
            {
                switch (piece.type)
                {
                    case PieceType.Sleepy:
                        whitePawns++;
                        break;
                    case PieceType.Bishop:
                        if ((position.x + position.y) % 2 == 0)
                        {
                            whiteDarkBishops++;
                        }
                        else
                        {
                            whiteLightBishops++;
                        }
                        break;
                    case PieceType.Knight:
                        whiteKnights++;
                        break;
                    case PieceType.Rook:
                        whiteRooks++;
                        break;
                    case PieceType.Queen:
                        whiteQueens++;
                        break;
                    case PieceType.King:
                        whiteKings++;
                        break;
                }
            }
            else if (piece.color == Color.Black)
            {
                switch (piece.type)
                {
                    case PieceType.Sleepy:
                        blackPawns++;
                        break;
                    case PieceType.Bishop:
                        if ((position.x + position.y) % 2 == 0)
                        {
                            blackDarkBishops++;
                        }
                        else
                        {
                            blackLightBishops++;
                        }
                        break;
                    case PieceType.Knight:
                        blackKnights++;
                        break;
                    case PieceType.Rook:
                        blackRooks++;
                        break;
                    case PieceType.Queen:
                        blackQueens++;
                        break;
                    case PieceType.King:
                        blackKings++;
                        break;
                }
            }
        }
        internal void AddPiece(EnginePosition position, Color c, PieceType t)
        {
            if (c == Color.White)
            {
                switch (t)
                {
                    case PieceType.Sleepy:
                        whitePawns++;
                        break;
                    case PieceType.Bishop:
                        if ((position.x + position.y) % 2 == 0)
                        {
                            whiteDarkBishops++;
                        }
                        else
                        {
                            whiteLightBishops++;
                        }
                        break;
                    case PieceType.Knight:
                        whiteKnights++;
                        break;
                    case PieceType.Rook:
                        whiteRooks++;
                        break;
                    case PieceType.Queen:
                        whiteQueens++;
                        break;
                    case PieceType.King:
                        whiteKings++;
                        break;
                }
            }
            else if (c == Color.Black)
            {
                switch (t)
                {
                    case PieceType.Sleepy:
                        blackPawns++;
                        break;
                    case PieceType.Bishop:
                        if ((position.x + position.y) % 2 == 0)
                        {
                            blackDarkBishops++;
                        }
                        else
                        {
                            blackLightBishops++;
                        }
                        break;
                    case PieceType.Knight:
                        blackKnights++;
                        break;
                    case PieceType.Rook:
                        blackRooks++;
                        break;
                    case PieceType.Queen:
                        blackQueens++;
                        break;
                    case PieceType.King:
                        blackKings++;
                        break;
                }
            }
        }
        internal void RemovePiece(EnginePosition position, Piece piece)
        {
            if (piece.isEmpty) return;
            if (piece.color == Color.White)
            {
                switch (piece.type)
                {
                    case PieceType.Sleepy:
                        whitePawns--;
                        break;
                    case PieceType.Bishop:
                        if ((position.x + position.y) % 2 == 0)
                        {
                            whiteDarkBishops--;
                        }
                        else
                        {
                            whiteLightBishops--;
                        }
                        break;
                    case PieceType.Knight:
                        whiteKnights--;
                        break;
                    case PieceType.Rook:
                        whiteRooks--;
                        break;
                    case PieceType.Queen:
                        whiteQueens--;
                        break;
                    case PieceType.King:
                        whiteKings--;
                        break;
                }
            }
            else if (piece.color == Color.Black)
            {
                switch (piece.type)
                {
                    case PieceType.Sleepy:
                        blackPawns--;
                        break;
                    case PieceType.Bishop:
                        if ((position.x + position.y) % 2 == 0)
                        {
                            blackDarkBishops--;
                        }
                        else
                        {
                            blackLightBishops--;
                        }
                        break;
                    case PieceType.Knight:
                        blackKnights--;
                        break;
                    case PieceType.Rook:
                        blackRooks--;
                        break;
                    case PieceType.Queen:
                        blackQueens--;
                        break;
                    case PieceType.King:
                        blackKings--;
                        break;
                }
            }
        }
        internal bool InsufficientMaterialCheck()
        {
            // K vs k
            if(
                whiteKings == 1
                && blackKings == 1
                && whitePawns == 0
                && blackPawns == 0
                && whiteLightBishops == 0
                && whiteDarkBishops == 0
                && blackLightBishops == 0
                && blackDarkBishops == 0
                && whiteKnights == 0
                && blackKnights == 0
                && whiteRooks == 0
                && blackRooks == 0
                && whiteQueens == 0
                && blackQueens == 0
            ) return true;

            // KB vs k
            if (
                whiteKings == 1
                && blackKings == 1
                && whitePawns == 0
                && blackPawns == 0
                && whiteLightBishops == 1
                && whiteDarkBishops == 0
                && blackLightBishops == 0
                && blackDarkBishops == 0
                && whiteKnights == 0
                && blackKnights == 0
                && whiteRooks == 0
                && blackRooks == 0
                && whiteQueens == 0
                && blackQueens == 0
            ) return true;
            if (
                whiteKings == 1
                && blackKings == 1
                && whitePawns == 0
                && blackPawns == 0
                && whiteLightBishops == 0
                && whiteDarkBishops == 1
                && blackLightBishops == 0
                && blackDarkBishops == 0
                && whiteKnights == 0
                && blackKnights == 0
                && whiteRooks == 0
                && blackRooks == 0
                && whiteQueens == 0
                && blackQueens == 0
            ) return true;

            // K vs kb
            if (
                whiteKings == 1
                && blackKings == 1
                && whitePawns == 0
                && blackPawns == 0
                && whiteLightBishops == 0
                && whiteDarkBishops == 0
                && blackLightBishops == 1
                && blackDarkBishops == 0
                && whiteKnights == 0
                && blackKnights == 0
                && whiteRooks == 0
                && blackRooks == 0
                && whiteQueens == 0
                && blackQueens == 0
            ) return true;
            if (
                whiteKings == 1
                && blackKings == 1
                && whitePawns == 0
                && blackPawns == 0
                && whiteLightBishops == 0
                && whiteDarkBishops == 0
                && blackLightBishops == 0
                && blackDarkBishops == 1
                && whiteKnights == 0
                && blackKnights == 0
                && whiteRooks == 0
                && blackRooks == 0
                && whiteQueens == 0
                && blackQueens == 0
            ) return true;

            // KN vs k
            if (
                whiteKings == 1
                && blackKings == 1
                && whitePawns == 0
                && blackPawns == 0
                && whiteLightBishops == 0
                && whiteDarkBishops == 0
                && blackLightBishops == 0
                && blackDarkBishops == 0
                && whiteKnights == 1
                && blackKnights == 0
                && whiteRooks == 0
                && blackRooks == 0
                && whiteQueens == 0
                && blackQueens == 0
            ) return true;

            // K vs kn
            if (
                whiteKings == 1
                && blackKings == 1
                && whitePawns == 0
                && blackPawns == 0
                && whiteLightBishops == 0
                && whiteDarkBishops == 0
                && blackLightBishops == 0
                && blackDarkBishops == 0
                && whiteKnights == 0
                && blackKnights == 1
                && whiteRooks == 0
                && blackRooks == 0
                && whiteQueens == 0
                && blackQueens == 0
            ) return true;

            // KB vs kb, Light
            if (
                whiteKings == 1
                && blackKings == 1
                && whitePawns == 0
                && blackPawns == 0
                && whiteLightBishops == 1
                && whiteDarkBishops == 0
                && blackLightBishops == 1
                && blackDarkBishops == 0
                && whiteKnights == 0
                && blackKnights == 0
                && whiteRooks == 0
                && blackRooks == 0
                && whiteQueens == 0
                && blackQueens == 0
            ) return true;

            // KB vs kb, Dark
            if (
                whiteKings == 1
                && blackKings == 1
                && whitePawns == 0
                && blackPawns == 0
                && whiteLightBishops == 0
                && whiteDarkBishops == 1
                && blackLightBishops == 0
                && blackDarkBishops == 1
                && whiteKnights == 0
                && blackKnights == 0
                && whiteRooks == 0
                && blackRooks == 0
                && whiteQueens == 0
                && blackQueens == 0
            ) return true;

            return false;
        }
    }
}