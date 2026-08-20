using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Utils
{
    internal static class UciUtils
    {
        private static List<int> allowedNums = new List<int> {1,2,3,4,5,6,7,8};
        private static List<char> allowedLetters = new List<char> {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'};
        private static List<char> allowedPromotions = new List<char> { 'b', 'n', 'r', 'q'};
        internal static bool ValidateMove(string uciMove)
        {
            if (uciMove == null) return false;
            if (uciMove == "0000") return true;

            if (uciMove.Length > 5 || uciMove.Length < 4)
            {
                return false;
            }

            if (!char.IsLetter(uciMove[0])) return false;
            if (!char.IsNumber(uciMove[1])) return false;
            if (!char.IsLetter(uciMove[2])) return false;
            if (!char.IsNumber(uciMove[3])) return false;

            int firstNumber = 0;
            int.TryParse(uciMove[1].ToString(), out firstNumber);

            int secondNumber = 0;
            int.TryParse(uciMove[3].ToString(), out secondNumber);

            if (!allowedNums.Contains(firstNumber)) return false;
            if (!allowedNums.Contains(secondNumber)) return false;

            if (!allowedLetters.Contains(uciMove[0])) return false;
            if (!allowedLetters.Contains(uciMove[2])) return false;

            if (uciMove.Length == 5)
            {
                if (!allowedPromotions.Contains(uciMove[4])) return false;
                return true;
            }
            return true;
        }
        internal static bool IsNullMove(string uciMove)
        {
            if (uciMove == null) return false;
            if (uciMove == "0000") return true;
            return false;
        }
        internal static int UciToEngineChar(char arg)
        {
            if (char.IsLetter(arg))
            {
                if (!allowedLetters.Contains(arg)) return -1;
                return allowedLetters.IndexOf(arg);
            }
            else if (char.IsNumber(arg))
            {
                int res = -1;
                int.TryParse(arg.ToString(), out res);
                res--;
                return res;
            }
            return -1;
            
        }
        internal static PieceType UciToEnginePromotion(char arg)
        {
            if (!allowedPromotions.Contains(arg)) return PieceType.None;

            switch (arg)
            {
                case 'b':
                    return PieceType.Bishop;
                case 'n':
                    return PieceType.Knight;
                case 'r':
                    return PieceType.Rook;
                case 'q':
                    return PieceType.Queen;
                default:
                    return PieceType.None;
            }
        }
    }
}