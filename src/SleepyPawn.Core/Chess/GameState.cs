using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Chess
{
    internal class GameState
    {
        internal Board boardState;
        internal ThreatBoard threats;
        internal Color playerToMove;
        internal CastlingRights whiteCastlingRights;
        internal CastlingRights blackCastlingRights;
        
        internal GameState(bool emptyBoard = false)
        {
            whiteCastlingRights = new CastlingRights();
            blackCastlingRights = new CastlingRights();
            boardState = new Board();
            threats = new ThreatBoard();
            threats.GenerateThreats(boardState);
            if(!emptyBoard) boardState.SetupStandard();
            playerToMove = Color.White;
        }

        internal GameState(Board board, Color player, CastlingRights whiteCastling, CastlingRights blackCastling)
        {
            boardState = board;
            threats = new ThreatBoard();
            threats.GenerateThreats(board);
            playerToMove = player;

            whiteCastlingRights = new CastlingRights(whiteCastling);
            blackCastlingRights = new CastlingRights(blackCastling);
        }

        internal Piece GetPiece(EnginePosition position)
        {
            return boardState.GetPiece(position);
        }
        internal void GenerateThreats()
        {
            threats.GenerateThreats(boardState);
        }
        internal void AddPiece(EnginePosition position, Color color, PieceType type)
        {
            boardState.AddPiece(position, color, type);
        }
        internal GameState AppendMove(Move move)
        {
            if (!move.isValid) return this;
            if (move.nullMove)
            {
                return new GameState(boardState, ColorUtils.Reverse(playerToMove), 
                    whiteCastlingRights, blackCastlingRights);
            }
            Board changedBoard = new Board(boardState);

            Piece pieceToMove = changedBoard.GetPiece(move.firstPos);

            bool wc = whiteCastlingRights.CastlingRetained;
            bool wlc = whiteCastlingRights.LongCastlingRetained;
            bool bc = blackCastlingRights.CastlingRetained;
            bool blc = blackCastlingRights.LongCastlingRetained;

            if(pieceToMove.type == PieceType.King)
            {
                if(pieceToMove.color == Color.White)
                {
                    wc = false;
                    wlc = false;

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
            return new GameState(changedBoard, ColorUtils.Reverse(playerToMove), new CastlingRights(wc,wlc), new CastlingRights(bc, blc));
        }

        internal string DebugPlayer()
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
}
