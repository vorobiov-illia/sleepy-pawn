using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class KingRules : PieceRule
    {
        internal override bool CanMove(Move move, ChessState state)
        {
            Piece thisPiece = state.GetPiece(move.firstPos);
            Piece otherPiece = state.GetPiece(move.secondPos);

            if (!CommonCheck(move, state, PieceType.King, thisPiece, otherPiece)) return false;

            if (CastlingCheck(move, state)) return true;

            if (Math.Abs(move.secondPos.x - move.firstPos.x) > 1) return false;
            if (Math.Abs(move.secondPos.y - move.firstPos.y) > 1) return false;

            if (!otherPiece.isEmpty &&
                otherPiece.color == thisPiece.color) return false;

            return true;
        }

        private bool CastlingCheck(Move move, ChessState state)
        {
            Piece thisPiece = state.GetPiece(move.firstPos);

            if (thisPiece.color == Color.White)
            {
                if (move.firstPos != PieceUtils.defaultWhiteKingPosition) return false;
                if (move.secondPos == PieceUtils.whiteKingShortCastle)
                {
                    Piece otherPiece = state.GetPiece(PieceUtils.whiteShortRook);
                    if (otherPiece.isEmpty) return false;
                    if (otherPiece.type != PieceType.Rook) return false;
                    if (otherPiece.color != Color.White) return false;

                    if (!state.info.whiteCastlingRights.CastlingRetained) return false;
                    foreach (EnginePosition pos in PieceUtils.whiteShortCastlingVunurableMap)
                    {
                        if (state.boardState.IsTileAttacked(pos, Color.Black)) return false;
                    }
                    foreach (EnginePosition pos in PieceUtils.whiteShortCastlingBlockMap)
                    {
                        Piece piece = state.boardState.GetPiece(pos);
                        if (!piece.isEmpty) return false;
                    }
                    return true;
                }
                if (move.secondPos == PieceUtils.whiteKingLongCastle)
                {
                    Piece otherPiece = state.GetPiece(PieceUtils.whiteLongRook);
                    if (otherPiece.isEmpty) return false;
                    if (otherPiece.type != PieceType.Rook) return false;
                    if (otherPiece.color != Color.White) return false;

                    if (!state.info.whiteCastlingRights.LongCastlingRetained) return false;
                    foreach (EnginePosition pos in PieceUtils.whiteLongCastlingVunurableMap)
                    {
                        if (state.boardState.IsTileAttacked(pos, Color.Black)) return false;
                    }
                    foreach (EnginePosition pos in PieceUtils.whiteLongCastlingBlockMap)
                    {
                        Piece piece = state.boardState.GetPiece(pos);
                        if (!piece.isEmpty) return false;
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
                    Piece otherPiece = state.GetPiece(PieceUtils.blackShortRook);
                    if (otherPiece.isEmpty) return false;
                    if (otherPiece.type != PieceType.Rook) return false;
                    if (otherPiece.color != Color.Black) return false;

                    if (!state.info.blackCastlingRights.CastlingRetained) return false;
                    foreach (EnginePosition pos in PieceUtils.blackShortCastlingVunurableMap)
                    {
                        if (state.boardState.IsTileAttacked(pos, Color.White)) return false;
                    }
                    foreach (EnginePosition pos in PieceUtils.blackShortCastlingBlockMap)
                    {
                        Piece piece = state.boardState.GetPiece(pos);
                        if (!piece.isEmpty) return false;
                    }
                    return true;
                }
                if (move.secondPos == PieceUtils.blackKingLongCastle)
                {
                    Piece otherPiece = state.GetPiece(PieceUtils.blackLongRook);
                    if (otherPiece.isEmpty) return false;
                    if (otherPiece.type != PieceType.Rook) return false;
                    if (otherPiece.color != Color.Black) return false;

                    if (!state.info.blackCastlingRights.LongCastlingRetained) return false;
                    foreach (EnginePosition pos in PieceUtils.blackLongCastlingVunurableMap)
                    {
                        if (state.boardState.IsTileAttacked(pos, Color.White)) return false;
                    }
                    foreach (EnginePosition pos in PieceUtils.blackLongCastlingBlockMap)
                    {
                        Piece piece = state.boardState.GetPiece(pos);
                        if (!piece.isEmpty) return false;
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        internal override int GeneratePseudoMoves(EnginePosition piecePosition, ChessState state, ref Span<Move> pseudoMoves, int count)
        {
            int newCount = count;

            Piece king = state.GetPiece(piecePosition);
            if (king.isEmpty) return newCount;
            if (king.type != PieceType.King) return newCount;

            Move upMove = new Move(piecePosition, new EnginePosition(piecePosition.x, piecePosition.y + 1));
            Move rightUpMove = new Move(piecePosition, new EnginePosition(piecePosition.x + 1, piecePosition.y + 1));
            Move rightMove = new Move(piecePosition, new EnginePosition(piecePosition.x + 1, piecePosition.y));
            Move rightDownMove = new Move(piecePosition, new EnginePosition(piecePosition.x + 1, piecePosition.y - 1));
            Move downMove = new Move(piecePosition, new EnginePosition(piecePosition.x, piecePosition.y - 1));
            Move leftDownMove = new Move(piecePosition, new EnginePosition(piecePosition.x - 1, piecePosition.y - 1));
            Move leftMove = new Move(piecePosition, new EnginePosition(piecePosition.x - 1, piecePosition.y));
            Move leftUpMove = new Move(piecePosition, new EnginePosition(piecePosition.x - 1, piecePosition.y + 1));

            pseudoMoves[newCount++] = upMove;
            pseudoMoves[newCount++] = rightUpMove;
            pseudoMoves[newCount++] = rightMove;
            pseudoMoves[newCount++] = rightDownMove;
            pseudoMoves[newCount++] = downMove;
            pseudoMoves[newCount++] = leftDownMove;
            pseudoMoves[newCount++] = leftMove;
            pseudoMoves[newCount++] = leftUpMove;

            if (king.color == Color.White && piecePosition == PieceUtils.defaultWhiteKingPosition)
            {
                Move castling = new Move(piecePosition, PieceUtils.whiteKingShortCastle);
                Move longCastling = new Move(piecePosition, PieceUtils.whiteKingLongCastle);

                pseudoMoves[newCount++] = castling;
                pseudoMoves[newCount++] = longCastling;
            }

            if (king.color == Color.Black && piecePosition == PieceUtils.defaultBlackKingPosition)
            {
                Move castling = new Move(piecePosition, PieceUtils.blackKingShortCastle);
                Move longCastling = new Move(piecePosition, PieceUtils.blackKingLongCastle);

                pseudoMoves[newCount++] = castling;
                pseudoMoves[newCount++] = longCastling;
            }

            return newCount;
        }

        internal override void GenerateThreat(EnginePosition piecePosition, Board board, ThreatBoard threats)
        {
            Piece thisPiece = board.GetPiece(piecePosition);

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
