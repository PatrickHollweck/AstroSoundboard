// ****************************** Module Header ****************************** //
//
//
// Last Modified: 08:05:2017 / 17:59
// Creation: 08:05:2017
// Project: AstroSoundBoard
//
//
// <copyright file="FileSystem.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

using System.IO;

namespace AstroSoundBoard.Core.Utils
{
    public static class FileSystem
    {
        public static class FolderHelper
        {
            /// <summary>
            /// Creates a Folder if it does not exist
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public static void CreateIfMissing(string path)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }
    }
}