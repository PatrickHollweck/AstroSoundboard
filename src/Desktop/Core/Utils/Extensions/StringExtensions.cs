namespace AstroSoundBoard.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToFileName(this string source)
        {
            return source.Replace(' ', '_');
        }

        public static string ToDisplayName(this string source)
        {
            return source.Replace('_', ' ');
        }
    }
}