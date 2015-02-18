using System;

namespace Shurly.Core.Helpers
{
    public static class ConvertLong
    {
        public static string ToCustomBase(long number, char[] baseChars)
        {
            string result = String.Empty;

            do
            {
                result = baseChars[number % baseChars.Length] + result;
                number /= baseChars.Length;
            }
            while (number > 0);

            return result;
        }
    }
}
