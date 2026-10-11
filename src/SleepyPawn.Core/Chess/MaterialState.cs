using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess
{
    internal class MaterialState
    {
        // Weight tables
        private static readonly int[] whitePawnsTable = { 
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            1,1,1,2,2,1,1,1,
            3,3,3,3,3,3,3,3,
            4,4,4,4,4,4,4,4,
            5,5,5,5,5,5,5,5,
            9,9,9,9,9,9,9,9,
        };
        private static readonly int[] blackPawnsTable = {
            9,9,9,9,9,9,9,9,
            5,5,5,5,5,5,5,5,
            4,4,4,4,4,4,4,4,
            3,3,3,3,3,3,3,3,
            1,1,1,2,2,1,1,1,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
        };
        private static readonly int[] whiteKingTable = {
            0,0,10,0,2,0,10,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
        };
        private static readonly int[] blackKingTable = {
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,10,0,2,0,10,0,
        };
        private static readonly int[] commonTable = {
            0,0,0,0,0,0,0,0,
            1,3,3,3,3,3,3,1,
            2,3,4,4,4,4,3,2,
            2,3,4,5,5,4,3,2,
            2,3,4,5,5,4,3,2,
            2,3,4,4,4,4,3,2,
            1,3,3,3,3,3,3,1,
            0,0,0,0,0,0,0,0,
        };

        // White pieces
        private ushort whitePawns;
        private ushort whiteLightBishops;
        private ushort whiteDarkBishops;
        private ushort whiteKnights;
        private ushort whiteRooks;
        private ushort whiteQueens;
        private ushort whiteKings;

        private int overallWhiteMaterial;

        // Black pieces
        private ushort blackPawns;
        private ushort blackLightBishops;
        private ushort blackDarkBishops;
        private ushort blackKnights;
        private ushort blackRooks;
        private ushort blackQueens;
        private ushort blackKings;

        private int overallBlackMaterial;
        internal MaterialState()
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

            overallWhiteMaterial = 0;
            overallBlackMaterial = 0;
        }
        internal MaterialState(MaterialState other)
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

            overallWhiteMaterial = other.overallWhiteMaterial;
            overallBlackMaterial = other.overallBlackMaterial;
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

            overallWhiteMaterial = 392;
            overallBlackMaterial = 392;
        }

        internal int GetMaterial(Color player)
        {
            if (player == Color.White)
            {
                return overallWhiteMaterial;
            }
            else if(player == Color.Black)
            {
                return overallBlackMaterial;
            }
            return 0;
        }
        internal void CalculateManually(Board board)
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

            overallWhiteMaterial = 0;
            overallBlackMaterial = 0;

            for (int i = 0; i < 64; i++)
            {
                Piece piece = board.pieces[i];
                if (piece.isEmpty) continue;
                if (piece.color == Color.White)
                {
                    switch (piece.type)
                    {
                        case PieceType.Sleepy:
                            
                            whitePawns++;
                            overallWhiteMaterial += 10 + whitePawnsTable[i];
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
                            overallWhiteMaterial += 30 + commonTable[i];
                            break;
                        case PieceType.Knight:
                            whiteKnights++;
                            overallWhiteMaterial += 30 + commonTable[i];
                            break;
                        case PieceType.Rook:
                            whiteRooks++;
                            overallWhiteMaterial += 50 + commonTable[i];
                            break;
                        case PieceType.Queen:
                            whiteQueens++;
                            overallWhiteMaterial += 90 + commonTable[i];
                            break;
                        case PieceType.King:
                            whiteKings++;
                            overallWhiteMaterial += whiteKingTable[i];
                            break;
                    }
                }
                else if (piece.color == Color.Black)
                {
                    switch (piece.type)
                    {
                        case PieceType.Sleepy:
                            blackPawns++;
                            overallBlackMaterial += 10 + blackPawnsTable[i];
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
                            overallBlackMaterial += 30 + commonTable[i];
                            break;
                        case PieceType.Knight:
                            blackKnights++;
                            overallBlackMaterial += 30 + commonTable[i];
                            break;
                        case PieceType.Rook:
                            blackRooks++;
                            overallBlackMaterial += 50 + commonTable[i];
                            break;
                        case PieceType.Queen:
                            blackQueens++;
                            overallBlackMaterial += 90 + commonTable[i];
                            break;
                        case PieceType.King:
                            blackKings++;
                            overallBlackMaterial += blackKingTable[i];
                            break;
                    }
                }
            }
        }
        internal void MovePiece(EnginePosition firstPosition, EnginePosition secondPosition, Piece piece)
        {
            RemovePiece(firstPosition, piece);
            AddPiece(secondPosition, piece);
        }
        internal void AddPiece(EnginePosition position, Piece piece)
        {
            if (piece.isEmpty) return;
            if (piece.color == Color.White)
            {
                switch (piece.type)
                {
                    case PieceType.Sleepy:
                        overallWhiteMaterial += 10 + whitePawnsTable[position.x + (position.y * 8)];
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
                        overallWhiteMaterial += 30 + commonTable[position.x + (position.y * 8)];
                        break;
                    case PieceType.Knight:
                        whiteKnights++;
                        overallWhiteMaterial += 30 + commonTable[position.x + (position.y * 8)];
                        break;
                    case PieceType.Rook:
                        whiteRooks++;
                        overallWhiteMaterial += 50 + commonTable[position.x + (position.y * 8)];
                        break;
                    case PieceType.Queen:
                        whiteQueens++;
                        overallWhiteMaterial += 90 + commonTable[position.x + (position.y * 8)];
                        break;
                    case PieceType.King:
                        whiteKings++;
                        overallWhiteMaterial += whiteKingTable[position.x + (position.y * 8)];
                        break;
                }
            }
            else if (piece.color == Color.Black)
            {
                switch (piece.type)
                {
                    case PieceType.Sleepy:
                        blackPawns++;
                        overallBlackMaterial += 10 + blackPawnsTable[position.x + (position.y * 8)];
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
                        overallBlackMaterial += 30 + commonTable[position.x + (position.y * 8)];
                        break;
                    case PieceType.Knight:
                        blackKnights++;
                        overallBlackMaterial += 30 + commonTable[position.x + (position.y * 8)];
                        break;
                    case PieceType.Rook:
                        blackRooks++;
                        overallBlackMaterial += 50 + commonTable[position.x + (position.y * 8)];
                        break;
                    case PieceType.Queen:
                        blackQueens++;
                        overallBlackMaterial += 90 + commonTable[position.x + (position.y * 8)];
                        break;
                    case PieceType.King:
                        blackKings++;
                        overallBlackMaterial += blackKingTable[position.x + (position.y * 8)];
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
                        overallWhiteMaterial -= 10 + whitePawnsTable[position.x + (position.y * 8)];
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
                        overallWhiteMaterial -= 30 + commonTable[position.x + (position.y * 8)];
                        break;
                    case PieceType.Knight:
                        whiteKnights--;
                        overallWhiteMaterial -= 30 + commonTable[position.x + (position.y * 8)];
                        break;
                    case PieceType.Rook:
                        whiteRooks--;
                        overallWhiteMaterial -= 50 + commonTable[position.x + (position.y * 8)];
                        break;
                    case PieceType.Queen:
                        whiteQueens--;
                        overallWhiteMaterial -= 90 + commonTable[position.x + (position.y * 8)];
                        break;
                    case PieceType.King:
                        whiteKings--;
                        overallWhiteMaterial -= whiteKingTable[position.x + (position.y * 8)];
                        break;
                }
            }
            else if (piece.color == Color.Black)
            {
                switch (piece.type)
                {
                    case PieceType.Sleepy:
                        blackPawns--;
                        overallBlackMaterial -= 10 + blackPawnsTable[position.x + (position.y * 8)];
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
                        overallBlackMaterial -= 30 + commonTable[position.x + (position.y * 8)];
                        break;
                    case PieceType.Knight:
                        blackKnights--;
                        overallBlackMaterial -= 30 + commonTable[position.x + (position.y * 8)];
                        break;
                    case PieceType.Rook:
                        blackRooks--;
                        overallBlackMaterial -= 50 + commonTable[position.x + (position.y * 8)];
                        break;
                    case PieceType.Queen:
                        blackQueens--;
                        overallBlackMaterial -= 90 + commonTable[position.x + (position.y * 8)];
                        break;
                    case PieceType.King:
                        blackKings--;
                        overallBlackMaterial -= blackKingTable[position.x + (position.y * 8)];
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