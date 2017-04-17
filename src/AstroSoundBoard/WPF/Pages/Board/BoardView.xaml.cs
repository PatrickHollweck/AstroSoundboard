// ****************************** Module Header ****************************** //
//
//
// Last Modified: 17:04:2017 / 16:05
// Creation: 16:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="BoardView.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Pages.Board
{
	using System.Collections.Generic;
	using System.Windows.Controls;

	using AstroSoundBoard.Core.Objects.DataObjects;

	using log4net;

	public partial class BoardView : UserControl
	{
		private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private List<Sound> AllSounds { get; set; } = new List<Sound>();

		public BoardView()
		{
			InitializeComponent();
			DataContext = this;
		}
	}
}