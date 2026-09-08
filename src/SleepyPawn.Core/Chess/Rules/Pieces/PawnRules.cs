using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class PawnRules : PieceRule
    {
        internal override bool CanMove(Move move, GameState state)
        {
            Piece thisPiece = state.GetPiece(move.firstPos);
            Piece otherPiece = state.GetPiece(move.secondPos);

            if (!CommonCheck(move, state, PieceType.Sleepy, thisPiece, otherPiece)) return false;

            // TO-DO: Refactor this mess, double color checks and code duplication
            
            if(thisPiece.color == Color.White)
            {
                if(move.secondPos == state.blackEnPassantState.EnPassantVulnerability)
                {
                    if (state.blackEnPassantState.EnPassantLink == null) return false;
                    Piece target = state.GetPiece(state.blackEnPassantState.EnPassantLink.Value);
                    if (target.color != thisPiece.color)
                    {
                        if (move.secondPos == new EnginePosition(move.firstPos.x + 1, move.firstPos.y + 1) ||
                            move.secondPos == new EnginePosition(move.firstPos.x - 1, move.firstPos.y + 1))
                        {
                            return true;
                        }
                    }
                }
            }
            else if (thisPiece.color == Color.Black)
            {
                if (move.secondPos == state.whiteEnPassantState.EnPassantVulnerability)
                {
                    if (state.whiteEnPassantState.EnPassantLink == null) return false;
                    Piece target = state.GetPiece(state.whiteEnPassantState.EnPassantLink.Value);
                    if (target.color != thisPiece.color)
                    {
                        if (move.secondPos == new EnginePosition(move.firstPos.x + 1, move.firstPos.y - 1) ||
                            move.secondPos == new EnginePosition(move.firstPos.x - 1, move.firstPos.y - 1))
                        {
                            return true;
                        }
                    }
                }
            }

            if (otherPiece.isEmpty)
            {
                if (move.firstPos.x != move.secondPos.x) return false;
                if(thisPiece.color == Color.White)
                {
                    if (move.promotionPiece == PieceType.None && move.secondPos.y == 7) return false;
                    if (move.secondPos.y == move.firstPos.y + 1) return true;
                    if (move.firstPos.y != 1) return false;
                    Piece pathway = state.GetPiece(new EnginePosition(move.firstPos.x, move.firstPos.y + 1));
                    if(pathway == null)
                    {
                        pathway = new Piece();
                    }
                    if (move.secondPos.y == move.firstPos.y + 2 &&
                        pathway.isEmpty) return true;
                    return false;
                }
                if(thisPiece.color == Color.Black)
                {
                    if (move.promotionPiece == PieceType.None && move.secondPos.y == 0) return false;
                    if (move.secondPos.y == move.firstPos.y - 1) return true;
                    if (move.firstPos.y != 6) return false;
                    Piece pathway = state.GetPiece(new EnginePosition(move.firstPos.x, move.firstPos.y - 1));
                    if (pathway == null)
                    {
                        pathway = new Piece();
                    }
                    if (move.secondPos.y == move.firstPos.y - 2 &&
                        pathway.isEmpty) return true;
                    return false;
                }
                return false;
            }
            else
            {
                if(otherPiece.color == thisPiece.color) return false;
                if (thisPiece.color == Color.White)
                {
                    if (move.promotionPiece == PieceType.None && move.secondPos.y == 7) return false;
                    if (move.secondPos != new EnginePosition(move.firstPos.x + 1, move.firstPos.y + 1) &&
                        move.secondPos != new EnginePosition(move.firstPos.x - 1, move.firstPos.y + 1)) return false;
                    return true;
                }
                if (thisPiece.color == Color.Black)
                {
                    if (move.promotionPiece == PieceType.None && move.secondPos.y == 0) return false;
                    if (move.secondPos != new EnginePosition(move.firstPos.x + 1, move.firstPos.y - 1) &&
                        move.secondPos != new EnginePosition(move.firstPos.x - 1, move.firstPos.y - 1)) return false;
                    return true;
                }
                return false;
            }
        }

        internal override List<Move> GeneratePseudoMoves(EnginePosition piecePosition, GameState state)
        {
            List<Move> moves = new List<Move>();

            Piece pawn = state.GetPiece(piecePosition);
            if (pawn == null) return moves;
            if (pawn.isEmpty) return moves;
            if (pawn.type != PieceType.Sleepy) return moves;

            if (pawn.color == Color.White)
            {
                EnginePosition singleMovePosition = new EnginePosition(piecePosition.x, piecePosition.y + 1);
                Move singleMove = new Move(piecePosition, singleMovePosition);
                moves.Add(singleMove);
                if(singleMovePosition.y == 7)
                {
                    Move singleMoveBishop = new Move(piecePosition, singleMovePosition, PieceType.Bishop);
                    Move singleMoveKnight = new Move(piecePosition, singleMovePosition, PieceType.Knight);
                    Move singleMoveRook = new Move(piecePosition, singleMovePosition, PieceType.Rook);
                    Move singleMoveQueen = new Move(piecePosition, singleMovePosition, PieceType.Queen);

                    moves.Add(singleMoveBishop);
                    moves.Add(singleMoveKnight);
                    moves.Add(singleMoveRook);
                    moves.Add(singleMoveQueen);
                }
                if (piecePosition.y == 1)
                {
                    EnginePosition doubleMovePosition = new EnginePosition(piecePosition.x, piecePosition.y + 2);
                    Move doubleMove = new Move(piecePosition, doubleMovePosition);
                    moves.Add(doubleMove);
                }
                EnginePosition leftMovePosition = new EnginePosition(piecePosition.x - 1, piecePosition.y + 1);
                Move leftMove = new Move(piecePosition, leftMovePosition);
                moves.Add(leftMove);
                if (leftMovePosition.y == 7)
                {
                    Move leftMoveBishop = new Move(piecePosition, leftMovePosition, PieceType.Bishop);
                    Move leftMoveKnight = new Move(piecePosition, leftMovePosition, PieceType.Knight);
                    Move leftMoveRook = new Move(piecePosition, leftMovePosition, PieceType.Rook);
                    Move leftMoveQueen = new Move(piecePosition, leftMovePosition, PieceType.Queen);

                    moves.Add(leftMoveBishop);
                    moves.Add(leftMoveKnight);
                    moves.Add(leftMoveRook);
                    moves.Add(leftMoveQueen);
                }
                EnginePosition rightMovePosition = new EnginePosition(piecePosition.x + 1, piecePosition.y + 1);
                Move rightMove = new Move(piecePosition, rightMovePosition);
                moves.Add(rightMove);
                if (rightMovePosition.y == 7)
                {
                    Move rightMoveBishop = new Move(piecePosition, rightMovePosition, PieceType.Bishop);
                    Move rightMoveKnight = new Move(piecePosition, rightMovePosition, PieceType.Knight);
                    Move rightMoveRook = new Move(piecePosition, rightMovePosition, PieceType.Rook);
                    Move rightMoveQueen = new Move(piecePosition, rightMovePosition, PieceType.Queen);

                    moves.Add(rightMoveBishop);
                    moves.Add(rightMoveKnight);
                    moves.Add(rightMoveRook);
                    moves.Add(rightMoveQueen);
                }
            }
            if (pawn.color == Color.Black)
            {
                EnginePosition singleMovePosition = new EnginePosition(piecePosition.x, piecePosition.y - 1);
                Move singleMove = new Move(piecePosition, singleMovePosition);
                moves.Add(singleMove);
                if (singleMovePosition.y == 0)
                {
                    Move singleMoveBishop = new Move(piecePosition, singleMovePosition, PieceType.Bishop);
                    Move singleMoveKnight = new Move(piecePosition, singleMovePosition, PieceType.Knight);
                    Move singleMoveRook = new Move(piecePosition, singleMovePosition, PieceType.Rook);
                    Move singleMoveQueen = new Move(piecePosition, singleMovePosition, PieceType.Queen);

                    moves.Add(singleMoveBishop);
                    moves.Add(singleMoveKnight);
                    moves.Add(singleMoveRook);
                    moves.Add(singleMoveQueen);
                }
                if (piecePosition.y == 6)
                {
                    EnginePosition doubleMovePosition = new EnginePosition(piecePosition.x, piecePosition.y - 2);
                    Move doubleMove = new Move(piecePosition, doubleMovePosition);
                    moves.Add(doubleMove);
                }
                EnginePosition leftMovePosition = new EnginePosition(piecePosition.x - 1, piecePosition.y - 1);
                Move leftMove = new Move(piecePosition, leftMovePosition);
                moves.Add(leftMove);
                if (leftMovePosition.y == 0)
                {
                    Move leftMoveBishop = new Move(piecePosition, leftMovePosition, PieceType.Bishop);
                    Move leftMoveKnight = new Move(piecePosition, leftMovePosition, PieceType.Knight);
                    Move leftMoveRook = new Move(piecePosition, leftMovePosition, PieceType.Rook);
                    Move leftMoveQueen = new Move(piecePosition, leftMovePosition, PieceType.Queen);

                    moves.Add(leftMoveBishop);
                    moves.Add(leftMoveKnight);
                    moves.Add(leftMoveRook);
                    moves.Add(leftMoveQueen);
                }
                EnginePosition rightMovePosition = new EnginePosition(piecePosition.x + 1, piecePosition.y - 1);
                Move rightMove = new Move(piecePosition, rightMovePosition);
                moves.Add(rightMove);
                if (rightMovePosition.y == 0)
                {
                    Move rightMoveBishop = new Move(piecePosition, rightMovePosition, PieceType.Bishop);
                    Move rightMoveKnight = new Move(piecePosition, rightMovePosition, PieceType.Knight);
                    Move rightMoveRook = new Move(piecePosition, rightMovePosition, PieceType.Rook);
                    Move rightMoveQueen = new Move(piecePosition, rightMovePosition, PieceType.Queen);

                    moves.Add(rightMoveBishop);
                    moves.Add(rightMoveKnight);
                    moves.Add(rightMoveRook);
                    moves.Add(rightMoveQueen);
                }
            }

            return moves;
        }

        internal override void GenerateThreat(EnginePosition piecePosition, Board board, ThreatBoard threats)
        {
            Piece thisPiece = board.GetPiece(piecePosition);

            if (thisPiece == null) return;
            if (thisPiece.isEmpty) return;
            if (thisPiece.type != PieceType.Sleepy) return;

            if(thisPiece.color == Color.White)
            {
                threats.AddThreat(new EnginePosition(piecePosition.x - 1, piecePosition.y + 1), Color.White);
                threats.AddThreat(new EnginePosition(piecePosition.x + 1, piecePosition.y + 1), Color.White);
            }
            else if (thisPiece.color == Color.Black)
            {
                threats.AddThreat(new EnginePosition(piecePosition.x - 1, piecePosition.y - 1), Color.Black);
                threats.AddThreat(new EnginePosition(piecePosition.x + 1, piecePosition.y - 1), Color.Black);
            }
        }
    }
}
