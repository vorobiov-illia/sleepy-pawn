using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class KingRules : PieceRule
    {
        internal override bool CanMove(Move move, GameState state)
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

        private bool CastlingCheck(Move move, GameState state)
        {
            Piece thisPiece = state.GetPiece(move.firstPos);

            if (thisPiece.color == Color.White)
            {
                if (move.firstPos != PieceUtils.defaultWhiteKingPosition) return false;
                if (move.secondPos == PieceUtils.whiteKingShortCastle)
                {
                    Piece otherPiece = state.GetPiece(PieceUtils.whiteShortRook);
                    if (otherPiece == null) return false;
                    if (otherPiece.isEmpty) return false;
                    if (otherPiece.type != PieceType.Rook) return false;
                    if (otherPiece.color != Color.White) return false;

                    if (!state.whiteCastlingRights.CastlingRetained) return false;
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
                    Piece otherPiece = state.GetPiece(PieceUtils.whiteLongRook);
                    if (otherPiece == null) return false;
                    if (otherPiece.isEmpty) return false;
                    if (otherPiece.type != PieceType.Rook) return false;
                    if (otherPiece.color != Color.White) return false;

                    if (!state.whiteCastlingRights.LongCastlingRetained) return false;
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
                    Piece otherPiece = state.GetPiece(PieceUtils.blackShortRook);
                    if (otherPiece == null) return false;
                    if (otherPiece.isEmpty) return false;
                    if (otherPiece.type != PieceType.Rook) return false;
                    if (otherPiece.color != Color.Black) return false;

                    if (!state.blackCastlingRights.CastlingRetained) return false;
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
                    Piece otherPiece = state.GetPiece(PieceUtils.blackLongRook);
                    if (otherPiece == null) return false;
                    if (otherPiece.isEmpty) return false;
                    if (otherPiece.type != PieceType.Rook) return false;
                    if (otherPiece.color != Color.Black) return false;

                    if (!state.blackCastlingRights.LongCastlingRetained) return false;
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

        internal override List<Move> GeneratePseudoMoves(EnginePosition piecePosition, GameState state)
        {
            List<Move> moves = new List<Move>();

            Piece king = state.GetPiece(piecePosition);
            if (king == null) return moves;
            if (king.isEmpty) return moves;
            if (king.type != PieceType.King) return moves;

            Move upMove = new Move(piecePosition, new EnginePosition(piecePosition.x, piecePosition.y + 1));
            Move rightUpMove = new Move(piecePosition, new EnginePosition(piecePosition.x + 1, piecePosition.y + 1));
            Move rightMove = new Move(piecePosition, new EnginePosition(piecePosition.x + 1, piecePosition.y));
            Move rightDownMove = new Move(piecePosition, new EnginePosition(piecePosition.x + 1, piecePosition.y - 1));
            Move downMove = new Move(piecePosition, new EnginePosition(piecePosition.x, piecePosition.y - 1));
            Move leftDownMove = new Move(piecePosition, new EnginePosition(piecePosition.x - 1, piecePosition.y - 1));
            Move leftMove = new Move(piecePosition, new EnginePosition(piecePosition.x - 1, piecePosition.y));
            Move leftUpMove = new Move(piecePosition, new EnginePosition(piecePosition.x - 1, piecePosition.y + 1));

            moves.Add(upMove);
            moves.Add(rightUpMove);
            moves.Add(rightMove);
            moves.Add(rightDownMove);
            moves.Add(downMove);
            moves.Add(leftDownMove);
            moves.Add(leftMove);
            moves.Add(leftUpMove);

            if(king.color == Color.White && piecePosition == PieceUtils.defaultWhiteKingPosition)
            {
                Move castling = new Move(piecePosition, PieceUtils.whiteKingShortCastle);
                Move longCastling = new Move(piecePosition, PieceUtils.whiteKingLongCastle);

                moves.Add(castling);
                moves.Add(longCastling);
            }

            if (king.color == Color.Black && piecePosition == PieceUtils.defaultBlackKingPosition)
            {
                Move castling = new Move(piecePosition, PieceUtils.blackKingShortCastle);
                Move longCastling = new Move(piecePosition, PieceUtils.blackKingLongCastle);

                moves.Add(castling);
                moves.Add(longCastling);
            }

            return moves;
        }

        internal override void GenerateThreat(EnginePosition piecePosition, Board board, ThreatBoard threats)
        {
            Piece thisPiece = board.GetPiece(piecePosition);

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
