using SleepyPawn.Core.Chess;
using SleepyPawn.Core.Chess.Enums;
using System.Runtime.InteropServices.JavaScript;
partial class SleepyPawnWeb
{
    private static SleepyChess chess;

    public static void Main()
    {
        chess = new SleepyChess();
    }

    [JSExport]
    internal static string DebugBoard()
    {
        return chess.DebugCurrentBoard();
    }

    [JSExport]
    internal static string DebugGameState()
    {
        if(chess.GetOutcome() == Outcome.InProgress)
        {
            return chess.DebugCurrentMoveOrder();
        }
        else
        {
            return chess.DebugOutcome();
        }
    }

    [JSExport]
    internal static void Reset()
    {
        chess = new SleepyChess();
    }

    [JSExport]
    internal static bool TryMove(string lan)
    {
        return chess.TryLanMove(lan);
    }
}
