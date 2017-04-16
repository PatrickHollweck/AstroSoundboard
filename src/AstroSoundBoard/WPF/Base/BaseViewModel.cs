// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:04:2017 / 23:09
// Creation: 16:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="BaseViewModel.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Base
{
	using System.ComponentModel;

	using PropertyChanged;

	[ImplementPropertyChanged]
	public class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
	}
}