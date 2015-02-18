
namespace Shurly.Core.Helpers
{
    /// <summary>
    /// Contains strings of safe alpha numeric characters
    /// </summary>
    /// <remarks>
    /// No characters that are confusing: i, I, l, L, o, O, 0, 1, u, v
    /// </remarks>
    public static class Characters
    {
        public static string AlphaSmall = "abcdefghjkmnpqrstwxyz";
        public static string AlphaCaps = "ABCDEFGHJKMNPQRSTWXYZ";
        public static string Alpha = AlphaSmall + AlphaCaps;
        public static string Numeric = "23456789";
        public static string AlphaNumeric = Alpha + Numeric;
    }
}
