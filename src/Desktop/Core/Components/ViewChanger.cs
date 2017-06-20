// ****************************** Module Header ****************************** //
// 
// 
// Last Modified: 11:05:2017 / 16:23
// Creation: 11:05:2017
// Project: AstroSoundBoard
// 
// 
// <copyright file="ViewChanger.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //



namespace AstroSoundBoard.Core.Components
{
    using System;
    using System.Reflection;

    using AstroSoundBoard.WPF.Pages.About;
    using AstroSoundBoard.WPF.Pages.Board;
    using AstroSoundBoard.WPF.Pages.Settings;
    using AstroSoundBoard.WPF.Windows;

    using log4net;

    /// <summary>
    /// This is a helper class to compensate for my incompetence, also it allows view changing.
    /// </summary>
    public class ViewChanger
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // Instance of the MainWindow active (Gets set in the MainWindow ctor)
        public static MainWindow MainWindowInstance { get; set; }

        // Enum of Pages in the Application.
        public enum Page
        {
            Board,
            Settings,
            About
        }

        /// <summary>
        /// Changes to the specified view.
        /// </summary>
        /// <param name="p">Page to change to</param>
        public static void ChangeViewTo(Page page)
        {
            Log.Info($"Changing View to : {page}");
            MainWindowInstance.DataContext = GetViewFromEnum(page);
        }

        /// <summary>
        /// Returns a new View from the requested Page enum.
        /// </summary>
        /// <param name="page">Page to get</param>
        /// <returns><paramref name="page"/></returns>
        private static object GetViewFromEnum(Page page)
        {
            switch (page)
            {
                case Page.Board:
                    return new BoardView();

                case Page.Settings:
                    return new SettingsView();

                case Page.About:
                    return new AboutView();

                default:
                    throw new ArgumentException("Illegal Argument");
            }
        }
    }
}