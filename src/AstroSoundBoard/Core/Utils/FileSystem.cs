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

namespace AstroSoundBoard.Core.Utils
{
    using System.IO;
    using System.Reflection;

    using log4net;

    public class FileSystem
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Helperz for Folderz
        /// </summary>
        public class FolderHelper
        {
            /// <summary>
            /// Creates a Folder if it does not exist
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public static bool CreateIfMissing(string path)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    return true;
                }

                return false;
            }
        }
    }
}