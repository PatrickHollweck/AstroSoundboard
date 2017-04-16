// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:04:2017 / 23:32
// Creation: 16:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="Sounds.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Objects.DataObjects
{
	using System.Collections.Generic;

	public class Sounds
	{
		public Sounds()
		{
			SoundList = new List<Sound>();
		}

		public List<Sound> SoundList { get; set; }
	}
}