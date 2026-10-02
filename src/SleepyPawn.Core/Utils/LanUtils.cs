using SleepyPawn.Core.Chess.Enums;

namespace SleepyPawn.Core.Utils
{
    internal static class LanUtils // Long algebraic notation
    {
        private static readonly List<int> allowedNums = new List<int> {1,2,3,4,5,6,7,8};
        private static readonly List<char> allowedLetters = new List<char> {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'};
        private static readonly List<char> allowedPromotions = new List<char> { 'b', 'n', 'r', 'q'};
        internal static bool ValidateMove(string lanMove)
        {
            if (lanMove == null) return false;
            if (lanMove == "0000") return true;

            if (lanMove.Length > 5 || lanMove.Length < 4)
            {
                return false;
            }

            if (!char.IsLetter(lanMove[0])) return false;
            if (!char.IsNumber(lanMove[1])) return false;
            if (!char.IsLetter(lanMove[2])) return false;
            if (!char.IsNumber(lanMove[3])) return false;

            int firstNumber = 0;
            int.TryParse(lanMove[1].ToString(), out firstNumber);

            int secondNumber = 0;
            int.TryParse(lanMove[3].ToString(), out secondNumber);

            if (!allowedNums.Contains(firstNumber)) return false;
            if (!allowedNums.Contains(secondNumber)) return false;

            if (!allowedLetters.Contains(lanMove[0])) return false;
            if (!allowedLetters.Contains(lanMove[2])) return false;

            if (lanMove.Length == 5)
            {
                if (!allowedPromotions.Contains(lanMove[4])) return false;
                return true;
            }
            return true;
        }
        internal static bool IsNullMove(string lanMove)
        {
            if (lanMove == null) return false;
            if (lanMove == "0000") return true;
            return false;
        }
        internal static int LanToEngineChar(char arg)
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
        internal static PieceType LanToEnginePromotion(char arg)
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