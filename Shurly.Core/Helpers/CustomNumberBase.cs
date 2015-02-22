using System;
using System.Linq;
using System.Text;

namespace Shurly.Core.Helpers
{
    public static class CustomNumberBase
    {
        public static string Encode(long base10Value, char[] targetBaseChars)
        {
            string result = String.Empty;

            while (base10Value > 0)
            {
                result = targetBaseChars[base10Value % targetBaseChars.Length] + result;
                base10Value /= targetBaseChars.Length;
            }

            return result;   
        }

        public static long Decode(string customBaseValue, char[] sourceBaseChars)
        {
            long base10Value = 0;

            for (int i = 0; i < customBaseValue.Length; i++)
            {
                base10Value = base10Value * sourceBaseChars.Length + Array.IndexOf(sourceBaseChars, customBaseValue[i]);
            }

            return base10Value;
        }
    }
}
