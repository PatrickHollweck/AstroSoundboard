// ****************************** Module Header ****************************** //
//
//
// Last Modified: 26:04:2017 / 21:33
// Creation: 16:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="ViewChanger.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Components
{
    using AstroSoundBoard.WPF.Pages.Board;
    using AstroSoundBoard.WPF.Pages.Settings;
    using AstroSoundBoard.WPF.Windows;

    using log4net;

    public class ViewChanger
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static MainWindow MainWindowInstance { get; set; }

        public enum Page
        {
            Board,
            Settings,
            About
        }

        public static void ChangeViewTo(Page p)
        {
            Log.Info($"Changing View to : {p}");
            MainWindowInstance.DataContext = GetViewFromEnum(p);
        }

        private static object GetViewFromEnum(Page p)
        {
            switch (p)
            {
                case Page.Board:
                    return new BoardView();

                case Page.Settings:
                    return new SettingsView();

                case Page.About:
                    return new WPF.Pages.About.AboutView();

                default:
                    throw new System.ArgumentException("Illegal Argument");
            }
        }
    }
}