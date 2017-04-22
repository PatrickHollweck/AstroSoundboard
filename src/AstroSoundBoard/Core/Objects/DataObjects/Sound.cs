// ****************************** Module Header ****************************** //
//
//
// Last Modified: 21:04:2017 / 23:43
// Creation: 21:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="Definition.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Objects.DataObjects
{
    public class Sound
	{
		// Definition
		public string Name { get; set; }

		// Settings
		public string IsFavorite { get; set; }

		// Info
		public string Description { get; set; }
		public string VideoLink { get; set; }
	}
}