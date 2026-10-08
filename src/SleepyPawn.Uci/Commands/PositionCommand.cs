using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Search;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Uci.Commands
{
    internal class PositionCommand : Command
    {
        private bool makeMove = false;
        private bool startPos = false;
        private string fen = "";
        private string[]? moves;

        internal PositionCommand(string fen = "", string[]? moves = null)
        {
            if (string.IsNullOrEmpty(fen))
            {
                startPos = true;
            }
            else
            {
                if (FenUtils.IsValid(fen))
                {
                    this.fen = fen;
                }
                else
                {
                    startPos = true;
                }
            }
            if(moves != null)
            {
                bool valid = true;
                for (int i = 0; i < moves.Length; i++) 
                {
                    if (!string.IsNullOrEmpty(moves[i]))
                    {
                        Move moveStruct = new Move();
                        moveStruct.FromLan(moves[i]);
                        if (!moveStruct.isValid) valid = false;
                    }
                    else
                    {
                        valid = false;
                    }
                }
                if (valid)
                {
                    this.moves = moves;
                    makeMove = true;
                }
            }
        }

        internal override void Execute(SleepyChess engine, SleepySearch? search = null)
        {
            if (engine == null) return;
            if (startPos)
            {
                engine.SetupInitialPosition();
            }
            else
            {
                engine.SetPosition(fen);
            }
            if (makeMove)
            {
                if (moves != null)
                {
                    for (int i = 0; i < moves.Length; i++)
                    {
                        Move move = new Move();
                        move.FromLan(moves[i]);
                        engine.ForceMove(move);
                    }
                }
            }
        }
    }
}
