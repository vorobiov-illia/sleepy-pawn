using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Utils
{
    public static class FenUtils
    {
        internal static string GetPiecesField(string fen)
        {
            return fen.Split(' ')[0];
        }
        private static Tuple<Piece,int> GetPieceFromRank(string fenRank, int position)
        {
            char fenChar = fenRank[position];
            if (char.IsDigit(fenChar))
            {
                Piece nullPiece = new Piece();
                int num;
                int.TryParse(fenChar.ToString(), out num);
                return new Tuple<Piece, int>(nullPiece,num);
            }
            Piece piece = new Piece(FenPieceColorToEngine(fenChar), FenPieceTypeToEngine(fenChar));
            return new Tuple<Piece, int>(piece, 0);
        }
        private static PieceType FenPieceTypeToEngine(char piece)
        {
            char lowerPiece = char.ToLower(piece);

            switch (lowerPiece)
            {
                case 'p':
                    return PieceType.Sleepy;
                case 'b':
                    return PieceType.Bishop;
                case 'n':
                    return PieceType.Knight;
                case 'r':
                    return PieceType.Rook;
                case 'q':
                    return PieceType.Queen;
                case 'k':
                    return PieceType.King;
                default:
                    return PieceType.None;
            } 
        }
        private static Color FenPieceColorToEngine(char piece)
        {
            if (char.IsUpper(piece))
            {
                return Color.White;
            }
            else if (char.IsLower(piece))
            {
                return Color.Black;
            }
            return Color.None;
        }
        private static string GetRank(string fenPieces, int rank)
        {
            if (fenPieces.Split("/").Length != 8) throw new ArgumentOutOfRangeException(nameof(fenPieces), "FEN string is invalid.");
            if (rank > 8 || rank < 1) throw new ArgumentOutOfRangeException(nameof(rank), "This rank is not supported.");

            return fenPieces.Split("/")[8 - rank];
        }
        internal static Board GetBoard(string fen)
        {
            Board result = new Board();
            string pieces = GetPiecesField(fen);

            for (int rank = 8; rank >= 1; rank--)
            {
                string fenRank = GetRank(pieces, rank);
                int position = 0;
                for (int i = 0; i < fenRank.Length; i++)
                {
                    Tuple<Piece, int> fenPos = GetPieceFromRank(fenRank, i);
                    if (fenPos.Item2 > 0)
                    {
                        position += fenPos.Item2;
                        continue;
                    }
                    else
                    {
                        result.AddPiece(new EnginePosition(position, rank - 1), fenPos.Item1);
                    }
                    position++;
                }
            }
            return result;
        }
        internal static Color GetPlayerToMove(string fen)
        {
            if (fen.Split(' ').Length < 2) return Color.White;

            string player = fen.Split(' ')[1];
            if (player == "w") return Color.White;
            if (player == "b") return Color.Black;
            return Color.None;
        }
        internal static Tuple<CastlingRights, CastlingRights> GetCastlingRights(string fen)
        {
            CastlingRights whiteRights = new CastlingRights();
            CastlingRights blackRights = new CastlingRights();

            whiteRights.CastlingRetained = false;
            whiteRights.LongCastlingRetained = false;
            blackRights.CastlingRetained = false;
            blackRights.LongCastlingRetained = false;

            if (fen.Split(' ').Length < 3) return new Tuple<CastlingRights, CastlingRights>(whiteRights, blackRights);

            string fenRights = fen.Split(' ')[2];
            if(fenRights != "-")
            {
                for(int i = 0; i < fenRights.Length; i++)
                {
                    switch (fenRights[i])
                    {
                        case 'k':
                            blackRights.CastlingRetained = true;
                            break;
                        case 'q':
                            blackRights.LongCastlingRetained = true;
                            break;
                        case 'K':
                            whiteRights.CastlingRetained = true;
                            break;
                        case 'Q':
                            whiteRights.LongCastlingRetained = true;
                            break;
                    }
                }
            }
            return new Tuple<CastlingRights, CastlingRights>(whiteRights,blackRights);
        }
        internal static Tuple<EnPassantState, EnPassantState> GetEnPassantStates(string fen)
        {
            EnPassantState whiteEnPassantState = new EnPassantState();
            EnPassantState blackEnPassantState = new EnPassantState();

            if (fen.Split(' ').Length < 4) return new Tuple<EnPassantState, EnPassantState>(whiteEnPassantState, blackEnPassantState);

            string fenPassant = fen.Split(' ')[3];
            if (fenPassant != "-")
            {
                if(fenPassant.Length == 2)
                {
                    if (fenPassant[1] == '3') // White
                    {
                        switch (fenPassant[0])
                        {
                            case 'a':
                                whiteEnPassantState.EnPassantVulnerability = new EnginePosition(0,2);
                                whiteEnPassantState.EnPassantLink = new EnginePosition(0,3);
                                break;
                            case 'b':
                                whiteEnPassantState.EnPassantVulnerability = new EnginePosition(1, 2);
                                whiteEnPassantState.EnPassantLink = new EnginePosition(1, 3);
                                break;
                            case 'c':
                                whiteEnPassantState.EnPassantVulnerability = new EnginePosition(2, 2);
                                whiteEnPassantState.EnPassantLink = new EnginePosition(2, 3);
                                break;
                            case 'd':
                                whiteEnPassantState.EnPassantVulnerability = new EnginePosition(3, 2);
                                whiteEnPassantState.EnPassantLink = new EnginePosition(3, 3);
                                break;
                            case 'e':
                                whiteEnPassantState.EnPassantVulnerability = new EnginePosition(4, 2);
                                whiteEnPassantState.EnPassantLink = new EnginePosition(4, 3);
                                break;
                            case 'f':
                                whiteEnPassantState.EnPassantVulnerability = new EnginePosition(5, 2);
                                whiteEnPassantState.EnPassantLink = new EnginePosition(5, 3);
                                break;
                            case 'g':
                                whiteEnPassantState.EnPassantVulnerability = new EnginePosition(6, 2);
                                whiteEnPassantState.EnPassantLink = new EnginePosition(6, 3);
                                break;
                            case 'h':
                                whiteEnPassantState.EnPassantVulnerability = new EnginePosition(7, 2);
                                whiteEnPassantState.EnPassantLink = new EnginePosition(7, 3);
                                break;
                        }
                    }
                    else if (fenPassant[1] == '6') // Black
                    {
                        switch (fenPassant[0])
                        {
                            case 'a':
                                blackEnPassantState.EnPassantVulnerability = new EnginePosition(0, 5);
                                blackEnPassantState.EnPassantLink = new EnginePosition(0, 4);
                                break;
                            case 'b':
                                blackEnPassantState.EnPassantVulnerability = new EnginePosition(1, 5);
                                blackEnPassantState.EnPassantLink = new EnginePosition(1, 4);
                                break;
                            case 'c':
                                blackEnPassantState.EnPassantVulnerability = new EnginePosition(2, 5);
                                blackEnPassantState.EnPassantLink = new EnginePosition(2, 4);
                                break;
                            case 'd':
                                blackEnPassantState.EnPassantVulnerability = new EnginePosition(3, 5);
                                blackEnPassantState.EnPassantLink = new EnginePosition(3, 4);
                                break;
                            case 'e':
                                blackEnPassantState.EnPassantVulnerability = new EnginePosition(4, 5);
                                blackEnPassantState.EnPassantLink = new EnginePosition(4, 4);
                                break;
                            case 'f':
                                blackEnPassantState.EnPassantVulnerability = new EnginePosition(5, 5);
                                blackEnPassantState.EnPassantLink = new EnginePosition(5, 4);
                                break;
                            case 'g':
                                blackEnPassantState.EnPassantVulnerability = new EnginePosition(6, 5);
                                blackEnPassantState.EnPassantLink = new EnginePosition(6, 4);
                                break;
                            case 'h':
                                blackEnPassantState.EnPassantVulnerability = new EnginePosition(7, 5);
                                blackEnPassantState.EnPassantLink = new EnginePosition(7, 4);
                                break;
                        }
                    }
                }
            }

            return new Tuple<EnPassantState, EnPassantState>(whiteEnPassantState, blackEnPassantState);
        }
        internal static int GetHalfMoveCount(string fen)
        {
            if (fen.Split(' ').Length < 5) return 0;

            int res = 0;

            int.TryParse(fen.Split(' ')[4], out res);

            return res;
        }
        internal static int GetMoveCount(string fen)
        {
            if (fen.Split(' ').Length < 6) return 1;

            int res = 0;

            int.TryParse(fen.Split(' ')[5], out res);

            return res;
        }
        public static bool IsValid(string fen)
        {
            if (fen == null) return false;
            
            int count = 0;

            if (string.IsNullOrEmpty(fen.Split(' ')[0])) return false;

            foreach (string substring in fen.Split(' '))
            {
                if (!string.IsNullOrEmpty(substring))
                {
                    count++;
                }
            }

            if (count < 4) return false;

            string pieces = GetPiecesField(fen);
            int rankCount = pieces.Split('/').Length;

            if (rankCount != 8) return false;
            for (int i = 0; i < 8; i++)
            {
                string rank = GetRank(pieces, i + 1);

                if (!IsValidRank(rank)) return false;
            }

            return true;
        }
        private static bool IsValidRank(string rank)
        {
            int sum = 0;

            for(int i = 0; i < rank.Length; i++)
            {
                if (char.IsDigit(rank[i]))
                {
                    sum += int.Parse(rank[i].ToString());
                }
                else
                {
                    if (FenPieceTypeToEngine(rank[i]) == PieceType.None) return false;
                    sum++;
                }
            }

            return sum == 8;
        }

        internal static PieceCounter GetMaterial(string fen)
        {
            PieceCounter result = new PieceCounter();

            string pieces = GetPiecesField(fen);

            for (int rank = 8; rank >= 1; rank--)
            {
                string fenRank = GetRank(pieces, rank);
                int position = 0;
                for (int i = 0; i < fenRank.Length; i++)
                {
                    Tuple<Piece, int> fenPos = GetPieceFromRank(fenRank, i);
                    if (fenPos.Item2 > 0)
                    {
                        position += fenPos.Item2;
                        continue;
                    }
                    else
                    {
                        result.AddPiece(new EnginePosition(position, rank - 1), fenPos.Item1);
                    }
                    position++;
                }
            }

            return result;
        }
    }
}
