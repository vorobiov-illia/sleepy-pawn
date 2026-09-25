using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Chess.Rules;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess
{
    internal class State
    {
        internal Board boardState;
        internal Color playerToMove;
        internal StateInfo[] history;
        internal ulong[] hashes;
        internal int ply;
        internal ref StateInfo info => ref history[ply];

        internal State(bool emptyBoard = false)
        {
            history = new StateInfo[2048];
            hashes = new ulong[2048];
            ply = 0;
            boardState = new Board();
            if (!emptyBoard)
            {
                boardState.SetupStandard();
            }
            history[0] = new StateInfo(emptyBoard);
            playerToMove = Color.White;
            hashes[0] = ZobristUtils.HashState(this);
        }

        internal State(string fen)
        {
            history = new StateInfo[2048];
            hashes = new ulong[2048];
            ply = 0;
            history[0] = new StateInfo(true);

            Tuple<CastlingRights, CastlingRights> rights = FenUtils.GetCastlingRights(fen);
            history[0].whiteCastlingRights = rights.Item1;
            history[0].blackCastlingRights = rights.Item2;

            Tuple<EnPassantState, EnPassantState> enPassantStates = FenUtils.GetEnPassantStates(fen);
            history[0].whiteEnPassantState = enPassantStates.Item1;
            history[0].blackEnPassantState = enPassantStates.Item2;

            boardState = FenUtils.GetBoard(fen);

            history[0].whiteKingPosition = boardState.FindKing(Color.White);
            history[0].blackKingPosition = boardState.FindKing(Color.Black);

            playerToMove = FenUtils.GetPlayerToMove(fen);

            history[0].halfMoveClock = FenUtils.GetHalfMoveCount(fen);
            hashes[0] = ZobristUtils.HashState(this);
        }
        internal State(State other)
        {
            boardState = new Board(other.boardState);
            playerToMove = other.playerToMove;

            history = new StateInfo[2048];
            hashes = new ulong[2048];
            ply = other.ply;
            Array.Copy(other.history, history, 2048);
            Array.Copy(other.hashes, hashes, 2048);
        }

        internal ref StateInfo GetInfo()
        {
            return ref history[ply];
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
                    if (!boardState.PositionCheck(info.whiteKingPosition)) return true;
                    oldKingPosition = info.whiteKingPosition;
                    info.whiteKingPosition = move.secondPos;
                }
                else if (playerToMove == Color.Black)
                {
                    if (!boardState.PositionCheck(info.blackKingPosition)) return true;
                    oldKingPosition = info.blackKingPosition;
                    info.blackKingPosition = move.secondPos;
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
                    info.whiteKingPosition = oldKingPosition;
                }
                else if (playerToMove == Color.Black)
                {
                    info.blackKingPosition = oldKingPosition;
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
                if (!boardState.PositionCheck(info.whiteKingPosition)) return false;

                piece = boardState.GetPiece(info.whiteKingPosition);
            }
            else if (color == Color.Black)
            {
                if (!boardState.PositionCheck(info.blackKingPosition)) return false;

                piece = boardState.GetPiece(info.blackKingPosition);
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
                if (boardState.IsTileAttacked(info.whiteKingPosition, Color.Black)) return true;
            }
            else if (color == Color.Black)
            {
                if (boardState.IsTileAttacked(info.blackKingPosition, Color.White)) return true;
            }
            else
            {
                return false;
            }
            return false;
        }
        internal int GetLegalMoves(ref Span<Move> moves)
        {
            return LegalMoveAnalyzer.GetLegalMoves(this, ref moves);
        }
        internal int GetPseudoMoves(ref Span<Move> moves)
        {
            int pseudoCount = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    EnginePosition position = new EnginePosition(i, j);
                    Piece piece = GetPiece(position);

                    if (piece.isEmpty) continue;
                    if (piece.color != playerToMove) continue;

                    pseudoCount = LegalMoveAnalyzer.pieceRules.GetPseudoMoves(position, this, ref moves, pseudoCount);
                }
            }
            return pseudoCount;
        }

        internal void AppendMove(Move move)
        {
            if (!move.isValid) return;

            StateInfo currentStateInfo = history[ply];
            StateInfo newStateInfo = history[ply];
            newStateInfo.takenPiece = new Piece();
            newStateInfo.takenPiecePosition = BoardUtils.IllegalPosition;
            newStateInfo.blackEnPassantState = new EnPassantState();
            newStateInfo.whiteEnPassantState = new EnPassantState();
            newStateInfo.lastMove = move;

            ply++;
            
            if (move.nullMove)
            {
                playerToMove = ColorUtils.Reverse(playerToMove);
                history[ply] = newStateInfo;
                return;
            }

            Piece pieceToMove = boardState.GetPiece(move.firstPos);
            if(!boardState.GetPiece(move.secondPos).isEmpty)
            {
                newStateInfo.halfMoveClock = -1;
            }

            if (pieceToMove.type == PieceType.Sleepy)
            {
                newStateInfo.halfMoveClock = -1;
                if (pieceToMove.color == Color.White)
                {
                    if (Math.Abs(move.secondPos.y - move.firstPos.y) > 1)
                    {
                        newStateInfo.whiteEnPassantState.EnPassantVulnerability = new EnginePosition(move.secondPos.x, move.secondPos.y - 1);
                        newStateInfo.whiteEnPassantState.EnPassantLink = new EnginePosition(move.secondPos);
                    }
                    if(currentStateInfo.blackEnPassantState.EnPassantVulnerability != null && currentStateInfo.blackEnPassantState.EnPassantLink != null)
                    {
                        if (move.secondPos == currentStateInfo.blackEnPassantState.EnPassantVulnerability)
                        {
                            newStateInfo.takenPiece = GetPiece(currentStateInfo.blackEnPassantState.EnPassantLink.Value);
                            newStateInfo.takenPiecePosition = currentStateInfo.blackEnPassantState.EnPassantLink.Value;
                            boardState.RemovePiece(currentStateInfo.blackEnPassantState.EnPassantLink.Value);
                        }
                    }
                }
                if (pieceToMove.color == Color.Black)
                {
                    if (Math.Abs(move.secondPos.y - move.firstPos.y) > 1)
                    {
                        newStateInfo.blackEnPassantState.EnPassantVulnerability = new EnginePosition(move.secondPos.x, move.secondPos.y + 1);
                        newStateInfo.blackEnPassantState.EnPassantLink = new EnginePosition(move.secondPos);
                    }
                    if (currentStateInfo.whiteEnPassantState.EnPassantVulnerability != null && currentStateInfo.whiteEnPassantState.EnPassantLink != null)
                    {
                        if (move.secondPos == currentStateInfo.whiteEnPassantState.EnPassantVulnerability)
                        {
                            newStateInfo.takenPiece = GetPiece(currentStateInfo.whiteEnPassantState.EnPassantLink.Value);
                            newStateInfo.takenPiecePosition = currentStateInfo.whiteEnPassantState.EnPassantLink.Value;
                            boardState.RemovePiece(currentStateInfo.whiteEnPassantState.EnPassantLink.Value);
                        }
                    }
                }
            }
            if (pieceToMove.type == PieceType.King)
            {
                if(pieceToMove.color == Color.White)
                {
                    newStateInfo.whiteCastlingRights.CastlingRetained = false;
                    newStateInfo.whiteCastlingRights.LongCastlingRetained = false;
                    newStateInfo.whiteKingPosition = move.secondPos;

                    if( move.firstPos == PieceUtils.defaultWhiteKingPosition &&
                        move.secondPos == PieceUtils.whiteKingShortCastle)
                    {
                        boardState.ReplacePiece(PieceUtils.whiteShortRook, PieceUtils.whiteShortRookAfterCastle);
                    }
                    if (move.firstPos == PieceUtils.defaultWhiteKingPosition &&
                        move.secondPos == PieceUtils.whiteKingLongCastle)
                    {
                        boardState.ReplacePiece(PieceUtils.whiteLongRook, PieceUtils.whiteLongRookAfterCastle);
                    }
                }
                if(pieceToMove.color == Color.Black)
                {
                    newStateInfo.blackCastlingRights.CastlingRetained = false;
                    newStateInfo.blackCastlingRights.LongCastlingRetained = false;
                    newStateInfo.blackKingPosition = move.secondPos;

                    if (move.firstPos == PieceUtils.defaultBlackKingPosition &&
                        move.secondPos == PieceUtils.blackKingShortCastle)
                    {
                        boardState.ReplacePiece(PieceUtils.blackShortRook, PieceUtils.blackShortRookAfterCastle);
                    }
                    if (move.firstPos == PieceUtils.defaultBlackKingPosition &&
                        move.secondPos == PieceUtils.blackKingLongCastle)
                    {
                        boardState.ReplacePiece(PieceUtils.blackLongRook, PieceUtils.blackLongRookAfterCastle);
                    }
                }
            }
            if (pieceToMove.type == PieceType.Rook)
            {
                if (move.firstPos == PieceUtils.whiteShortRook) 
                    newStateInfo.whiteCastlingRights.CastlingRetained = false;
                if (move.firstPos == PieceUtils.whiteLongRook) 
                    newStateInfo.whiteCastlingRights.LongCastlingRetained = false;
                if (move.firstPos == PieceUtils.blackShortRook) 
                    newStateInfo.blackCastlingRights.CastlingRetained = false;
                if (move.firstPos == PieceUtils.blackLongRook) 
                    newStateInfo.blackCastlingRights.LongCastlingRetained = false;
            }
            if (move.secondPos == PieceUtils.whiteShortRook)
                newStateInfo.whiteCastlingRights.CastlingRetained = false;
            if (move.secondPos == PieceUtils.whiteLongRook)
                newStateInfo.whiteCastlingRights.LongCastlingRetained = false;
            if (move.secondPos == PieceUtils.blackShortRook)
                newStateInfo.blackCastlingRights.CastlingRetained = false;
            if (move.secondPos == PieceUtils.blackLongRook)
                newStateInfo.blackCastlingRights.LongCastlingRetained = false;

            Piece takenPiece = boardState.GetPiece(move.secondPos);
            if (!takenPiece.isEmpty)
            {
                newStateInfo.takenPiece = takenPiece;
                newStateInfo.takenPiecePosition = move.secondPos;
            }
            boardState.ReplacePiece(move.firstPos, move.secondPos, move.promotionPiece);
            playerToMove = ColorUtils.Reverse(playerToMove);
            newStateInfo.halfMoveClock++;
            history[ply] = newStateInfo;
            hashes[ply] = ZobristUtils.HashState(this);
        }

        internal void UndoMove()
        {
            if (ply == 0) return;

            Move move = info.lastMove;

            if (move.isValid == false) return;
            if (move.nullMove)
            {
                playerToMove = ColorUtils.Reverse(playerToMove);
                ply--;
                return;
            }

            Piece lastMovedPiece = boardState.GetPiece(move.secondPos);
            if (move.promotionPiece != PieceType.None)
            {
                Color promotedPieceColor = lastMovedPiece.color;
                boardState.RemovePiece(move.secondPos);
                boardState.AddPiece(move.firstPos, new Piece(promotedPieceColor, PieceType.Sleepy));
            }
            else
            {
                boardState.ReplacePiece(move.secondPos, move.firstPos);
                if(lastMovedPiece.type == PieceType.King)
                {
                    if(move.firstPos == PieceUtils.defaultWhiteKingPosition && lastMovedPiece.color == Color.White)
                    {
                        if(move.secondPos == PieceUtils.whiteKingShortCastle)
                        {
                            boardState.ReplacePiece(PieceUtils.whiteShortRookAfterCastle, PieceUtils.whiteShortRook);
                        }
                        if (move.secondPos == PieceUtils.whiteKingLongCastle)
                        {
                            boardState.ReplacePiece(PieceUtils.whiteLongRookAfterCastle, PieceUtils.whiteLongRook);
                        }
                    }
                    if (move.firstPos == PieceUtils.defaultBlackKingPosition && lastMovedPiece.color == Color.Black)
                    {
                        if (move.secondPos == PieceUtils.blackKingShortCastle)
                        {
                            boardState.ReplacePiece(PieceUtils.blackShortRookAfterCastle, PieceUtils.blackShortRook);
                        }
                        if (move.secondPos == PieceUtils.blackKingLongCastle)
                        {
                            boardState.ReplacePiece(PieceUtils.blackLongRookAfterCastle, PieceUtils.blackLongRook);
                        }
                    }
                }
            }

            boardState.AddPiece(info.takenPiecePosition,info.takenPiece);
            playerToMove = ColorUtils.Reverse(playerToMove);
            ply--;
        }

        internal bool CheckRepetitonRule(int maxRepetitions)
        {
            int count = 1;

            int limit = Math.Max(0, ply - info.halfMoveClock);

            for(int i = ply-2; i >= limit; i-=2)
            {
                if (hashes[i] == hashes[ply])
                {
                    count++;
                }
            }
            return count >= maxRepetitions;
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
    internal struct StateInfo
    {
        internal CastlingRights whiteCastlingRights;
        internal CastlingRights blackCastlingRights;
        internal EnPassantState whiteEnPassantState;
        internal EnPassantState blackEnPassantState;
        internal EnginePosition whiteKingPosition;
        internal EnginePosition blackKingPosition;

        internal Piece takenPiece;
        internal EnginePosition takenPiecePosition;
        internal Move lastMove;

        internal int halfMoveClock;
        public StateInfo(bool emptyBoard)
        {
            whiteCastlingRights = new CastlingRights();
            blackCastlingRights = new CastlingRights();
            whiteEnPassantState = new EnPassantState();
            blackEnPassantState = new EnPassantState();

            if (!emptyBoard)
            {
                whiteKingPosition = PieceUtils.defaultWhiteKingPosition;
                blackKingPosition = PieceUtils.defaultBlackKingPosition;
            }
            else
            {
                whiteKingPosition = BoardUtils.IllegalPosition;
                blackKingPosition = BoardUtils.IllegalPosition;
            }

            takenPiece = new Piece();
            takenPiecePosition = BoardUtils.IllegalPosition;
            lastMove = new Move();

            halfMoveClock = 0;
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
