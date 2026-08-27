using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class KingRules : PieceRule
    {
        internal override bool CanMove(Move move, GameState state)
        {
            if (!CommonCheck(move, state, PieceType.King)) return false;

            if (CastlingCheck(move, state)) return true;

            if (Math.Abs(move.secondPos.x - move.firstPos.x) > 1) return false;
            if (Math.Abs(move.secondPos.y - move.firstPos.y) > 1) return false;

            if (!otherPiece.isEmpty &&
                otherPiece.color == thisPiece.color) return false;

            return true;
        }

        private bool CastlingCheck(Move move, GameState state)
        {
            if(thisPiece.color == Color.White)
            {
                if (move.firstPos != PieceUtils.defaultWhiteKingPosition) return false;
                if (move.secondPos == PieceUtils.whiteKingShortCastle)
                {
                    if (!state.whiteCastlingRetained) return false;
                    foreach (EnginePosition pos in PieceUtils.whiteShortCastlingVunurableMap)
                    {
                        if (state.threats.GetThreat(Color.Black, pos)) return false;
                    }
                    foreach (EnginePosition pos in PieceUtils.whiteShortCastlingBlockMap)
                    {
                        Piece piece = state.boardState.GetPiece(pos);
                        if(piece != null)
                        {
                            if (!piece.isEmpty) return false;
                        }
                    }
                    return true;
                }
                if (move.secondPos == PieceUtils.whiteKingLongCastle)
                {
                    if (!state.whiteLongCastlingRetained) return false;
                    foreach (EnginePosition pos in PieceUtils.whiteLongCastlingVunurableMap)
                    {
                        if (state.threats.GetThreat(Color.Black, pos)) return false;
                    }
                    foreach (EnginePosition pos in PieceUtils.whiteLongCastlingBlockMap)
                    {
                        Piece piece = state.boardState.GetPiece(pos);
                        if (piece != null)
                        {
                            if (!piece.isEmpty) return false;
                        }
                    }
                    return true;
                }
                return false;
            }
            else if(thisPiece.color == Color.Black)
            {
                if (move.firstPos != PieceUtils.defaultBlackKingPosition) return false;
                if (move.secondPos == PieceUtils.blackKingShortCastle)
                {
                    if (!state.blackCastlingRetained) return false;
                    foreach (EnginePosition pos in PieceUtils.blackShortCastlingVunurableMap)
                    {
                        if (state.threats.GetThreat(Color.White, pos)) return false;
                    }
                    foreach (EnginePosition pos in PieceUtils.blackShortCastlingBlockMap)
                    {
                        Piece piece = state.boardState.GetPiece(pos);
                        if (piece != null)
                        {
                            if (!piece.isEmpty) return false;
                        }
                    }
                    return true;
                }
                if (move.secondPos == PieceUtils.blackKingLongCastle)
                {
                    if (!state.blackLongCastlingRetained) return false;
                    foreach (EnginePosition pos in PieceUtils.blackLongCastlingVunurableMap)
                    {
                        if (state.threats.GetThreat(Color.White, pos)) return false;
                    }
                    foreach (EnginePosition pos in PieceUtils.blackLongCastlingBlockMap)
                    {
                        Piece piece = state.boardState.GetPiece(pos);
                        if (piece != null)
                        {
                            if (!piece.isEmpty) return false;
                        }
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        internal override void GenerateThreat(EnginePosition piecePosition, Board board, ThreatBoard threats)
        {
            base.GenerateThreat(piecePosition, board, threats);

            if (thisPiece == null) return;
            if (thisPiece.isEmpty) return;
            if (thisPiece.type != PieceType.King) return;

            threats.AddThreat(new EnginePosition(piecePosition.x - 1, piecePosition.y + 1), thisPiece.color);
            threats.AddThreat(new EnginePosition(piecePosition.x, piecePosition.y + 1), thisPiece.color);
            threats.AddThreat(new EnginePosition(piecePosition.x + 1, piecePosition.y + 1), thisPiece.color);

            threats.AddThreat(new EnginePosition(piecePosition.x - 1, piecePosition.y), thisPiece.color);
            threats.AddThreat(new EnginePosition(piecePosition.x + 1, piecePosition.y), thisPiece.color);

            threats.AddThreat(new EnginePosition(piecePosition.x - 1, piecePosition.y - 1), thisPiece.color);
            threats.AddThreat(new EnginePosition(piecePosition.x, piecePosition.y - 1), thisPiece.color);
            threats.AddThreat(new EnginePosition(piecePosition.x + 1, piecePosition.y - 1), thisPiece.color);
        }
    }
}
