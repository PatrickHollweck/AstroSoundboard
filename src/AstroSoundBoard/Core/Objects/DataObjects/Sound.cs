// ****************************** Module Header ****************************** //
//
//
// Last Modified: 17:04:2017 / 12:25
// Creation: 16:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="Sound.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Objects.DataObjects
{
	using Newtonsoft.Json;

	public class Sound
	{
		public string Name { get; set; }

		public int Version { get; set; }

		public string IsFavorite { get; set; }

		public string Path => $"{AppSettings.InstallationPath}/content/{Name}";

		public string Description { get; set; }
		public string VideoSource { get; set; }

		public void MarkAsFavorite()
		{
			if (IsFavorite == JsonConvert.True)
			{
				IsFavorite = JsonConvert.False;
			}
			else
			{
				IsFavorite = JsonConvert.True;
			}
		}
	}
}