using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Chess.Enums;
using SleepyPawn.Core.Search.Evaluators;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Core.Search.PositionEvaluator
{
    internal class Evaluator : IEvaluator
    {
        public int EvaluatePosition(SleepyChess chess, Color player)
        {
            if (player == Color.None) return 0;

            int materialDif = chess.GetState().material.GetMaterial(player) - chess.GetState().material.GetMaterial(ColorUtils.Reverse(player));
            int castlingDif = 0;
            if(player == Color.White)
            {
                if (chess.GetState().info.whiteCastlingRights.CastlingRetained) castlingDif += 1;
                if (chess.GetState().info.whiteCastlingRights.LongCastlingRetained) castlingDif += 1;
                if (chess.GetState().info.blackCastlingRights.CastlingRetained) castlingDif -= 1;
                if (chess.GetState().info.blackCastlingRights.LongCastlingRetained) castlingDif -= 1;
            }
            else
            {
                if (chess.GetState().info.whiteCastlingRights.CastlingRetained) castlingDif -= 1;
                if (chess.GetState().info.whiteCastlingRights.LongCastlingRetained) castlingDif -= 1;
                if (chess.GetState().info.blackCastlingRights.CastlingRetained) castlingDif += 1;
                if (chess.GetState().info.blackCastlingRights.LongCastlingRetained) castlingDif += 1;
            }

            return materialDif + castlingDif;
        }
    }
}
