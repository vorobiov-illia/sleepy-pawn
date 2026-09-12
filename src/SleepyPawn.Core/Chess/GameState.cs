using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Chess.Rules;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess
{
    internal class GameState
    {
        internal Board boardState;
        internal Color playerToMove;
        internal CastlingRights whiteCastlingRights;
        internal CastlingRights blackCastlingRights;
        internal EnPassantState whiteEnPassantState;
        internal EnPassantState blackEnPassantState;
        internal EnginePosition whiteKingPosition;
        internal EnginePosition blackKingPosition;

        internal GameState(bool emptyBoard = false)
        {
            whiteCastlingRights = new CastlingRights();
            blackCastlingRights = new CastlingRights();
            whiteEnPassantState = new EnPassantState();
            blackEnPassantState = new EnPassantState();
            boardState = new Board();
            if (!emptyBoard)
            {
                boardState.SetupStandard();
                whiteKingPosition = PieceUtils.defaultWhiteKingPosition;
                blackKingPosition = PieceUtils.defaultBlackKingPosition;
            }
            else
            {
                whiteKingPosition = BoardUtils.IllegalPosition;
                blackKingPosition = BoardUtils.IllegalPosition;
            }
            playerToMove = Color.White;
        }

        internal GameState(string fen)
        {
            Tuple<CastlingRights, CastlingRights> rights = FenUtils.GetCastlingRights(fen);
            whiteCastlingRights = rights.Item1;
            blackCastlingRights = rights.Item2;

            Tuple<EnPassantState, EnPassantState> enPassantStates = FenUtils.GetEnPassantStates(fen);
            whiteEnPassantState = enPassantStates.Item1;
            blackEnPassantState = enPassantStates.Item2;

            boardState = FenUtils.GetBoard(fen);

            whiteKingPosition = boardState.FindKing(Color.White);
            blackKingPosition = boardState.FindKing(Color.Black);

            playerToMove = FenUtils.GetPlayerToMove(fen);
        }

        internal GameState(
            Board board,
            Color player,
            CastlingRights wc,
            CastlingRights bc,
            EnPassantState we,
            EnPassantState be,
            EnginePosition wkp,
            EnginePosition bkp)
        {
            boardState = board;
            playerToMove = player;

            whiteCastlingRights = new CastlingRights(wc);
            blackCastlingRights = new CastlingRights(bc);

            whiteEnPassantState = new EnPassantState(we);
            blackEnPassantState = new EnPassantState(be);

            whiteKingPosition = wkp;
            blackKingPosition = bkp;
        }
        internal GameState(GameState other)
        {
            boardState = new Board(other.boardState);
            playerToMove = other.playerToMove;

            whiteCastlingRights = new CastlingRights(other.whiteCastlingRights);
            blackCastlingRights = new CastlingRights(other.blackCastlingRights);

            whiteEnPassantState = new EnPassantState(other.whiteEnPassantState);
            blackEnPassantState = new EnPassantState(other.blackEnPassantState);

            whiteKingPosition = other.whiteKingPosition;
            blackKingPosition = other.blackKingPosition;
        }

        internal Piece GetPiece(EnginePosition position)
        {
            return boardState.GetPiece(position);
        }
        internal bool KingSafetyPrecheck(Move move)
        {
            EnginePosition oldKingPosition = BoardUtils.IllegalPosition;
            if (boardState.GetPiece(move.firstPos).type == PieceType.King)
            {
                if (playerToMove == Color.White)
                {
                    if (!boardState.PositionCheck(whiteKingPosition)) return true;
                    oldKingPosition = whiteKingPosition;
                    whiteKingPosition = move.secondPos;
                }
                else if (playerToMove == Color.Black)
                {
                    if (!boardState.PositionCheck(blackKingPosition)) return true;
                    oldKingPosition = blackKingPosition;
                    blackKingPosition = move.secondPos;
                }
                else return false;
            }

            Piece potentialVictim = boardState.GetPiece(move.secondPos);
            Piece pieceToMove = boardState.GetPiece(move.firstPos);

            bool enPassantMove = false;
            Piece enPassantVictim = new Piece();
            EnginePosition enPassantVictimPosition = BoardUtils.IllegalPosition;

            if (boardState.GetPiece(move.firstPos).type == PieceType.Sleepy
                && potentialVictim.isEmpty
                && move.firstPos.x != move.secondPos.x)
            {
                enPassantMove = true;
                if(pieceToMove.color == Color.White)
                {
                    enPassantVictimPosition.x = move.secondPos.x;
                    enPassantVictimPosition.y = move.secondPos.y - 1;
                }
                if (pieceToMove.color == Color.Black)
                {
                    enPassantVictimPosition.x = move.secondPos.x;
                    enPassantVictimPosition.y = move.secondPos.y + 1;
                }
                enPassantVictim = boardState.GetPiece(enPassantVictimPosition);
                boardState.RemovePiece(enPassantVictimPosition);
            }

            boardState.ReplacePiece(move.firstPos,move.secondPos);

            bool safe = !KingChecked(playerToMove);

            if (enPassantMove)
            {
                boardState.AddPiece(enPassantVictimPosition, enPassantVictim);
            }

            if (boardState.PositionCheck(oldKingPosition))
            {
                if (playerToMove == Color.White)
                {
                    whiteKingPosition = oldKingPosition;
                }
                else if (playerToMove == Color.Black)
                {
                    blackKingPosition = oldKingPosition;
                }
            }
            
            boardState.ReplacePiece(move.secondPos, move.firstPos);
            boardState.AddPiece(move.secondPos, potentialVictim);
            return safe;
        }
        internal void AddPiece(EnginePosition position, Color color, PieceType type)
        {
            boardState.AddPiece(position, color, type);
        }
        internal bool KingChecked(Color color)
        {
            Piece piece;
            if (color == Color.White)
            {
                if (!boardState.PositionCheck(whiteKingPosition)) return false;

                piece = boardState.GetPiece(whiteKingPosition);
            }
            else if (color == Color.Black)
            {
                if (!boardState.PositionCheck(blackKingPosition)) return false;

                piece = boardState.GetPiece(blackKingPosition);
            }
            else
            {
                return false;
            }
            if (piece.isEmpty) return false;
            if (piece.color != color) return false;
            if (piece.type != PieceType.King) return false;
            
            if (color == Color.White)
            {
                if (boardState.IsTileAttacked(whiteKingPosition, Color.Black)) return true;
            }
            else if (color == Color.Black)
            {
                if (boardState.IsTileAttacked(blackKingPosition, Color.White)) return true;
            }
            else
            {
                return false;
            }
            return false;
        }

        internal List<Move> GetLegalMoves()
        {
            return LegalMoveAnalyzer.GetLegalMoves(this);
        }

        internal GameState AppendMove(Move move)
        {
            if (!move.isValid) return this;
            if (move.nullMove)
            {
                return new GameState(
                    boardState, 
                    ColorUtils.Reverse(playerToMove), 
                    whiteCastlingRights, blackCastlingRights,
                    whiteEnPassantState, blackEnPassantState,
                    whiteKingPosition, blackKingPosition
                );
            }
            Board changedBoard = new Board(boardState);

            Piece pieceToMove = changedBoard.GetPiece(move.firstPos);

            bool wc = whiteCastlingRights.CastlingRetained;         // White short castling right
            bool wlc = whiteCastlingRights.LongCastlingRetained;    // White long castling right
            bool bc = blackCastlingRights.CastlingRetained;         // Black short castling right
            bool blc = blackCastlingRights.LongCastlingRetained;    // Black long castling right

            EnginePosition? wev = null; // White En Passant vulnerability tile position
            EnginePosition? wel = null; // White En Passant according pawn position
            EnginePosition? bev = null; // White En Passant vulnerability tile position
            EnginePosition? bel = null; // White En Passant according pawn position

            EnginePosition wkp = whiteKingPosition;
            EnginePosition bkp = blackKingPosition;

            if (pieceToMove.type == PieceType.Sleepy)
            {
                
                if (pieceToMove.color == Color.White)
                {
                    if (Math.Abs(move.secondPos.y - move.firstPos.y) > 1)
                    {
                        wev = new EnginePosition(move.secondPos.x, move.secondPos.y - 1);
                        wel = new EnginePosition(move.secondPos);
                    }
                    if(blackEnPassantState.EnPassantVulnerability != null && blackEnPassantState.EnPassantLink != null)
                    {
                        if (move.secondPos == blackEnPassantState.EnPassantVulnerability)
                        {
                            changedBoard.RemovePiece(blackEnPassantState.EnPassantLink.Value);
                        }
                    }
                }
                if (pieceToMove.color == Color.Black)
                {
                    if (Math.Abs(move.secondPos.y - move.firstPos.y) > 1)
                    {
                        bev = new EnginePosition(move.secondPos.x, move.secondPos.y + 1);
                        bel = new EnginePosition(move.secondPos);
                    }
                    if (whiteEnPassantState.EnPassantVulnerability != null && whiteEnPassantState.EnPassantLink != null)
                    {
                        if (move.secondPos == whiteEnPassantState.EnPassantVulnerability)
                        {
                            changedBoard.RemovePiece(whiteEnPassantState.EnPassantLink.Value);
                        }
                    }
                }
            }
            if (pieceToMove.type == PieceType.King)
            {
                if(pieceToMove.color == Color.White)
                {
                    wc = false;
                    wlc = false;
                    wkp = move.secondPos;

                    if( move.firstPos == PieceUtils.defaultWhiteKingPosition &&
                        move.secondPos == PieceUtils.whiteKingShortCastle)
                    {
                        changedBoard.ReplacePiece(PieceUtils.whiteShortRook, PieceUtils.whiteShortRookAfterCastle);
                    }
                    if (move.firstPos == PieceUtils.defaultWhiteKingPosition &&
                        move.secondPos == PieceUtils.whiteKingLongCastle)
                    {
                        changedBoard.ReplacePiece(PieceUtils.whiteLongRook, PieceUtils.whiteLongRookAfterCastle);
                    }
                }
                if(pieceToMove.color == Color.Black)
                {
                    bc = false;
                    blc = false;
                    bkp = move.secondPos;

                    if (move.firstPos == PieceUtils.defaultBlackKingPosition &&
                        move.secondPos == PieceUtils.blackKingShortCastle)
                    {
                        changedBoard.ReplacePiece(PieceUtils.blackShortRook, PieceUtils.blackShortRookAfterCastle);
                    }
                    if (move.firstPos == PieceUtils.defaultBlackKingPosition &&
                        move.secondPos == PieceUtils.blackKingLongCastle)
                    {
                        changedBoard.ReplacePiece(PieceUtils.blackLongRook, PieceUtils.blackLongRookAfterCastle);
                    }
                }
            }
            if (pieceToMove.type == PieceType.Rook)
            {
                if (move.firstPos == PieceUtils.whiteShortRook) wc = false;
                if (move.firstPos == PieceUtils.whiteLongRook) wlc = false;
                if (move.firstPos == PieceUtils.blackShortRook) bc = false;
                if (move.firstPos == PieceUtils.blackLongRook) blc = false;
            }
            if (move.secondPos == PieceUtils.whiteShortRook) wc = false;
            if (move.secondPos == PieceUtils.whiteLongRook) wlc = false;
            if (move.secondPos == PieceUtils.blackShortRook) bc = false;
            if (move.secondPos == PieceUtils.blackLongRook) blc = false;

            changedBoard.ReplacePiece(move.firstPos, move.secondPos, move.promotionPiece);
            return new GameState(
                changedBoard,
                ColorUtils.Reverse(playerToMove),
                new CastlingRights(wc, wlc),
                new CastlingRights(bc, blc),
                new EnPassantState(wev, wel),
                new EnPassantState(bev, bel),
                wkp,
                bkp
            );
        }

        internal string DebugMoveOrder()
        {
            string template = "'s time to move.";
            if(playerToMove == Color.White)
            {
                return "White" + template;
            }
            else if (playerToMove == Color.Black)
            {
                return "Black" + template;
            }
            else
            {
                return "Game is over.";
            }
        }
        internal string DebugCheck(Color color)
        {
            string templateCheck = "'s king is under check.";
            if (color == Color.White)
            {
                if (KingChecked(color))
                {
                    return "White" + templateCheck;
                }
                return "";
            }
            else if (color == Color.Black)
            {
                if (KingChecked(color))
                {
                    return "Black" + templateCheck;
                }
                return "";
            }
            else
            {
                return "There is no such king...";
            }
        }

        internal string DebugBoard()
        {
            return boardState.ToString();
        }

        public override string ToString()
        {
            return boardState.ToString();
        }
    }
    internal struct CastlingRights
    {
        internal bool CastlingRetained = true;
        internal bool LongCastlingRetained = true;

        public CastlingRights()
        {
            CastlingRetained = true;
            LongCastlingRetained = true;
        }
        public CastlingRights(bool cr, bool lcr)
        {
            CastlingRetained = cr;
            LongCastlingRetained = lcr;
        }
        public CastlingRights(CastlingRights other)
        {
            CastlingRetained = other.CastlingRetained;
            LongCastlingRetained = other.LongCastlingRetained;
        }
    }
    internal struct EnPassantState
    {
        internal EnginePosition? EnPassantVulnerability = null;
        internal EnginePosition? EnPassantLink = null;

        public EnPassantState()
        {
            EnPassantVulnerability = null;
            EnPassantLink = null;
        }
        public EnPassantState(EnginePosition? epv, EnginePosition? epl)
        {
            EnPassantVulnerability = epv;
            EnPassantLink = epl;
        }
        public EnPassantState(EnPassantState other)
        {
            EnPassantVulnerability = other.EnPassantVulnerability;
            EnPassantLink = other.EnPassantLink;
        }
    }
}
