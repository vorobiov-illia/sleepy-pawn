using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Chess.Rules.Pieces
{
    internal class PawnRules : PieceRule
    {
        internal override bool CanMove(Move move, State state)
        {
            Piece thisPiece = state.GetPiece(move.firstPos);
            Piece otherPiece = state.GetPiece(move.secondPos);

            if (!CommonCheck(move, state, PieceType.Sleepy, thisPiece, otherPiece)) return false;

            if (thisPiece.color == Color.White)
            {
                if (move.secondPos == state.info.blackEnPassantState.EnPassantVulnerability)
                {
                    if (state.info.blackEnPassantState.EnPassantLink == null) return false;
                    Piece target = state.GetPiece(state.info.blackEnPassantState.EnPassantLink.Value);
                    if (target.color != thisPiece.color)
                    {
                        if (move.secondPos == new EnginePosition(move.firstPos.x + 1, move.firstPos.y + 1) ||
                            move.secondPos == new EnginePosition(move.firstPos.x - 1, move.firstPos.y + 1))
                        {
                            return true;
                        }
                    }
                }
                return CanMoveWhite(state,move,thisPiece, otherPiece);
            }
            else if (thisPiece.color == Color.Black)
            {
                if (move.secondPos == state.info.whiteEnPassantState.EnPassantVulnerability)
                {
                    if (state.info.whiteEnPassantState.EnPassantLink == null) return false;
                    Piece target = state.GetPiece(state.info.whiteEnPassantState.EnPassantLink.Value);
                    if (target.color != thisPiece.color)
                    {
                        if (move.secondPos == new EnginePosition(move.firstPos.x + 1, move.firstPos.y - 1) ||
                            move.secondPos == new EnginePosition(move.firstPos.x - 1, move.firstPos.y - 1))
                        {
                            return true;
                        }
                    }
                }
                return CanMoveBlack(state, move, thisPiece, otherPiece);
            }
            return false;
        }

        private bool CanMoveBlack(State state, Move move, Piece thisPiece, Piece otherPiece) 
        {
            if (otherPiece.isEmpty)
            {
                if (move.firstPos.x != move.secondPos.x) return false;
                if (move.promotionPiece == PieceType.None && move.secondPos.y == 0) return false;
                if (move.secondPos.y == move.firstPos.y - 1) return true;
                if (move.firstPos.y != 6) return false;
                Piece pathway = state.GetPiece(new EnginePosition(move.firstPos.x, move.firstPos.y - 1));
                if (move.secondPos.y == move.firstPos.y - 2 &&
                    pathway.isEmpty) return true;
                return false;
            }
            else
            {
                if (otherPiece.color == thisPiece.color) return false;
                if (move.promotionPiece == PieceType.None && move.secondPos.y == 0) return false;
                if (move.secondPos != new EnginePosition(move.firstPos.x + 1, move.firstPos.y - 1) &&
                    move.secondPos != new EnginePosition(move.firstPos.x - 1, move.firstPos.y - 1)) return false;
                return true;
            }
        }
        private bool CanMoveWhite(State state, Move move, Piece thisPiece, Piece otherPiece) 
        {
            if (otherPiece.isEmpty)
            {
                if (move.firstPos.x != move.secondPos.x) return false;
                if (move.promotionPiece == PieceType.None && move.secondPos.y == 7) return false;
                if (move.secondPos.y == move.firstPos.y + 1) return true;
                if (move.firstPos.y != 1) return false;
                Piece pathway = state.GetPiece(new EnginePosition(move.firstPos.x, move.firstPos.y + 1));
                if (move.secondPos.y == move.firstPos.y + 2 &&
                    pathway.isEmpty) return true;
                return false;
            }
            else
            {
                if (otherPiece.color == thisPiece.color) return false;
                if (move.promotionPiece == PieceType.None && move.secondPos.y == 7) return false;
                if (move.secondPos != new EnginePosition(move.firstPos.x + 1, move.firstPos.y + 1) &&
                    move.secondPos != new EnginePosition(move.firstPos.x - 1, move.firstPos.y + 1)) return false;
                return true;
            }
        }

        internal override int GeneratePseudoMoves(EnginePosition piecePosition, State state, ref Span<Move> pseudoMoves, int count)
        {
            int newCount = count;
            
            Piece pawn = state.GetPiece(piecePosition);
            if (pawn.isEmpty) return newCount;
            if (pawn.type != PieceType.Sleepy) return newCount;

            if (pawn.color == Color.White)
            {
                EnginePosition singleMovePosition = new EnginePosition(piecePosition.x, piecePosition.y + 1);
                Move singleMove = new Move(piecePosition, singleMovePosition);
                pseudoMoves[newCount++] = singleMove;
                if(singleMovePosition.y == 7)
                {
                    Move singleMoveBishop = new Move(piecePosition, singleMovePosition, PieceType.Bishop);
                    Move singleMoveKnight = new Move(piecePosition, singleMovePosition, PieceType.Knight);
                    Move singleMoveRook = new Move(piecePosition, singleMovePosition, PieceType.Rook);
                    Move singleMoveQueen = new Move(piecePosition, singleMovePosition, PieceType.Queen);

                    pseudoMoves[newCount++] = singleMoveBishop;
                    pseudoMoves[newCount++] = singleMoveKnight;
                    pseudoMoves[newCount++] = singleMoveRook;
                    pseudoMoves[newCount++] = singleMoveQueen;
                }
                if (piecePosition.y == 1)
                {
                    EnginePosition doubleMovePosition = new EnginePosition(piecePosition.x, piecePosition.y + 2);
                    Move doubleMove = new Move(piecePosition, doubleMovePosition);
                    pseudoMoves[newCount++] = doubleMove;
                }
                EnginePosition leftMovePosition = new EnginePosition(piecePosition.x - 1, piecePosition.y + 1);
                Move leftMove = new Move(piecePosition, leftMovePosition);
                pseudoMoves[newCount++] = leftMove;
                if (leftMovePosition.y == 7)
                {
                    Move leftMoveBishop = new Move(piecePosition, leftMovePosition, PieceType.Bishop);
                    Move leftMoveKnight = new Move(piecePosition, leftMovePosition, PieceType.Knight);
                    Move leftMoveRook = new Move(piecePosition, leftMovePosition, PieceType.Rook);
                    Move leftMoveQueen = new Move(piecePosition, leftMovePosition, PieceType.Queen);

                    pseudoMoves[newCount++] = leftMoveBishop;
                    pseudoMoves[newCount++] = leftMoveKnight;
                    pseudoMoves[newCount++] = leftMoveRook;
                    pseudoMoves[newCount++] = leftMoveQueen;
                }
                EnginePosition rightMovePosition = new EnginePosition(piecePosition.x + 1, piecePosition.y + 1);
                Move rightMove = new Move(piecePosition, rightMovePosition);
                pseudoMoves[newCount++] = rightMove;
                if (rightMovePosition.y == 7)
                {
                    Move rightMoveBishop = new Move(piecePosition, rightMovePosition, PieceType.Bishop);
                    Move rightMoveKnight = new Move(piecePosition, rightMovePosition, PieceType.Knight);
                    Move rightMoveRook = new Move(piecePosition, rightMovePosition, PieceType.Rook);
                    Move rightMoveQueen = new Move(piecePosition, rightMovePosition, PieceType.Queen);

                    pseudoMoves[newCount++] = rightMoveBishop;
                    pseudoMoves[newCount++] = rightMoveKnight;
                    pseudoMoves[newCount++] = rightMoveRook;
                    pseudoMoves[newCount++] = rightMoveQueen;
                }
            }
            if (pawn.color == Color.Black)
            {
                EnginePosition singleMovePosition = new EnginePosition(piecePosition.x, piecePosition.y - 1);
                Move singleMove = new Move(piecePosition, singleMovePosition);
                pseudoMoves[newCount++] = singleMove;
                if (singleMovePosition.y == 0)
                {
                    Move singleMoveBishop = new Move(piecePosition, singleMovePosition, PieceType.Bishop);
                    Move singleMoveKnight = new Move(piecePosition, singleMovePosition, PieceType.Knight);
                    Move singleMoveRook = new Move(piecePosition, singleMovePosition, PieceType.Rook);
                    Move singleMoveQueen = new Move(piecePosition, singleMovePosition, PieceType.Queen);

                    pseudoMoves[newCount++] = singleMoveBishop;
                    pseudoMoves[newCount++] = singleMoveKnight;
                    pseudoMoves[newCount++] = singleMoveRook;
                    pseudoMoves[newCount++] = singleMoveQueen;
                }
                if (piecePosition.y == 6)
                {
                    EnginePosition doubleMovePosition = new EnginePosition(piecePosition.x, piecePosition.y - 2);
                    Move doubleMove = new Move(piecePosition, doubleMovePosition);
                    pseudoMoves[newCount++] = doubleMove;
                }
                EnginePosition leftMovePosition = new EnginePosition(piecePosition.x - 1, piecePosition.y - 1);
                Move leftMove = new Move(piecePosition, leftMovePosition);
                pseudoMoves[newCount++] = leftMove;
                if (leftMovePosition.y == 0)
                {
                    Move leftMoveBishop = new Move(piecePosition, leftMovePosition, PieceType.Bishop);
                    Move leftMoveKnight = new Move(piecePosition, leftMovePosition, PieceType.Knight);
                    Move leftMoveRook = new Move(piecePosition, leftMovePosition, PieceType.Rook);
                    Move leftMoveQueen = new Move(piecePosition, leftMovePosition, PieceType.Queen);

                    pseudoMoves[newCount++] = leftMoveBishop;
                    pseudoMoves[newCount++] = leftMoveKnight;
                    pseudoMoves[newCount++] = leftMoveRook;
                    pseudoMoves[newCount++] = leftMoveQueen;
                }
                EnginePosition rightMovePosition = new EnginePosition(piecePosition.x + 1, piecePosition.y - 1);
                Move rightMove = new Move(piecePosition, rightMovePosition);
                pseudoMoves[newCount++] = rightMove;
                if (rightMovePosition.y == 0)
                {
                    Move rightMoveBishop = new Move(piecePosition, rightMovePosition, PieceType.Bishop);
                    Move rightMoveKnight = new Move(piecePosition, rightMovePosition, PieceType.Knight);
                    Move rightMoveRook = new Move(piecePosition, rightMovePosition, PieceType.Rook);
                    Move rightMoveQueen = new Move(piecePosition, rightMovePosition, PieceType.Queen);

                    pseudoMoves[newCount++] = rightMoveBishop;
                    pseudoMoves[newCount++] = rightMoveKnight;
                    pseudoMoves[newCount++] = rightMoveRook;
                    pseudoMoves[newCount++] = rightMoveQueen;
                }
            }
            return newCount;
        }

        internal override void GenerateThreat(EnginePosition piecePosition, Board board, ThreatBoard threats)
        {
            Piece thisPiece = board.GetPiece(piecePosition);

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
