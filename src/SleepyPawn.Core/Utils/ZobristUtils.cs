using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Utils
{
    internal static class ZobristUtils
    {
        private static readonly Dictionary<PieceType, ulong[]> whitePieces;
        private static readonly Dictionary<PieceType, ulong[]> blackPieces;
        private static readonly ulong playerToMove;
        private static readonly ulong whiteShortCastling;
        private static readonly ulong whiteLongCastling;
        private static readonly ulong blackShortCastling;
        private static readonly ulong blackLongCastling;
        private static readonly ulong[] enPassantPositions;
        static ZobristUtils()
        {
            whitePieces = new Dictionary<PieceType, ulong[]>();
            blackPieces = new Dictionary<PieceType, ulong[]>();
            foreach (PieceType type in Enum.GetValues(typeof(PieceType)))
            {
                if (type == PieceType.None) continue;
                whitePieces.Add(type, new ulong[64]);
                blackPieces.Add(type, new ulong[64]);
                for (int i = 0; i < 64; i++)
                {
                    whitePieces[type][i] = XorShiftUtils.Next();
                    blackPieces[type][i] = XorShiftUtils.Next();
                }
            }

            playerToMove = XorShiftUtils.Next();

            whiteShortCastling = XorShiftUtils.Next();
            whiteLongCastling = XorShiftUtils.Next();
            blackShortCastling = XorShiftUtils.Next();
            blackLongCastling = XorShiftUtils.Next();

            enPassantPositions = new ulong[64];
            for (int i = 0; i < 64; i++)
            {
                enPassantPositions[i] = XorShiftUtils.Next();
            }

        }
        internal static ulong HashState(State state)
        {
            ulong result = 0;

            for(int i = 0; i < 64; i++)
            {
                Piece piece = state.boardState.pieces[i];
                if (piece.isEmpty) continue;
                if (piece.color == Color.White)
                {
                    result ^= whitePieces[piece.type][i];
                }
                else
                {
                    result ^= blackPieces[piece.type][i];
                }
                
            }
            if (state.info.whiteCastlingRights.CastlingRetained)
            {
                result ^= whiteShortCastling;
            }
            if (state.info.whiteCastlingRights.LongCastlingRetained)
            {
                result ^= whiteLongCastling;
            }
            if (state.info.blackCastlingRights.CastlingRetained)
            {
                result ^= blackShortCastling;
            }
            if (state.info.blackCastlingRights.LongCastlingRetained)
            {
                result ^= blackLongCastling;
            }

            if(state.playerToMove == Color.White)
            {
                if(state.info.blackEnPassantState.EnPassantVulnerability != null)
                {
                    result ^= enPassantPositions[BoardUtils.EnginePositionToIndex(state.info.blackEnPassantState.EnPassantVulnerability.Value)];
                }
            }
            else if(state.playerToMove == Color.Black)
            {
                result ^= playerToMove;
                if (state.info.whiteEnPassantState.EnPassantVulnerability != null)
                {
                    result ^= enPassantPositions[BoardUtils.EnginePositionToIndex(state.info.whiteEnPassantState.EnPassantVulnerability.Value)];
                }
            }

            return result;
        }
    }
}
