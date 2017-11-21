// ****************************** Module Header ****************************** //
//
//
// Last Modified: 04:07:2017 / 18:33
// Creation: 20:06:2017
// Project: AstroSoundBoard
//
//
// <copyright file="StringExtensions.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Misc.Extensions
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