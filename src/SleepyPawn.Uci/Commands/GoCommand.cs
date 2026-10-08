using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Search;
using SleepyPawn.Core.Utils;

namespace SleepyPawn.Uci.Commands
{
    internal class GoCommand : Command
    {
        internal override void Execute(SleepyChess engine, SleepySearch? search)
        {
            if(search == null)
            {
                Span<Move> moves = stackalloc Move[512];
                int count = engine.GetLegalMoves(ref moves);

                if (count == 0)
                {
                    Console.WriteLine("bestmove 0000");
                }
                else
                {
                    Random rng = new Random();
                    int choice = rng.Next(count);

                    engine.ForceMove(moves[choice]);
                    Console.WriteLine("bestmove " + LanUtils.ToLan(moves[choice]));
                }
            }
            else
            {
                Move bestMove = search.GetBestMove(engine, 4);
                engine.ForceMove(bestMove);
                Console.WriteLine("bestmove " + LanUtils.ToLan(bestMove));
            }
        }
    }
}
