// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:11:2017 / 13:47
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="FileSystem.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Misc
{
    using System.IO;

    public class FileSystem
    {
        /// <summary>
        /// Helpers for Folders
        /// </summary>
        public class FolderHelper
        {
            /// <summary>
            /// Creates a Folder if it does not exist
            /// </summary>
            /// <param name="path">Path of the Folder</param>
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