// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:04:2017 / 23:23
// Creation: 16:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="FileSystem.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Utils
{
	using System.IO;

	using log4net;

	public class FileSystem
	{
		private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

		public class FileHelper
		{
			public static bool CreateIfMissing(string path)
			{
				if (!File.Exists(path))
				{
					File.Create(path);
					return true;
				}

				return false;
			}

			/// <summary>
			/// Compares the Amount of Files in a Directory with the expected Amount given as a <paramref name="expectedCount"/>
			/// </summary>
			/// <param name="expectedCount">Amount of Files expected to be in a Directory</param>
			/// <param name="directory">path to the Directory containing the Files</param>
			/// <returns>True -> File count matched - False -> File Count does not match</returns>
			public static bool CompareFileCount(int expectedCount, string directory)
			{
				// Check is the Path is a Folder
				if (File.GetAttributes(directory) != FileAttributes.Directory)
				{
					return false;
				}

				// Compare the File Count with the expected Amount
				if (expectedCount == Directory.GetFiles(directory).Length)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
	}
}