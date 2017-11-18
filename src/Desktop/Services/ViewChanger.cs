// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:11:2017 / 14:22
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="ViewChanger.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Services
{
    using System;
    using System.Reflection;

    using AstroSoundBoard.Views;
    using AstroSoundBoard.Views.Pages;

    using log4net;

    /// <summary>
    /// This is a helper class to compensate for my incompetence, also it allows view changing.
    /// </summary>
    public class ViewChanger
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // Instance of the MainWindow active (Gets set in the MainWindow ctor)
        public static ShellView MainWindowInstance { get; set; }

        // Enum of Pages in the Application.
        public enum Page
        {
            /// <summary>
            /// The board page.
            /// </summary>
            Board,

            /// <summary>
            /// The Settings page.
            /// </summary>
            Settings,

            /// <summary>
            /// The About page.
            /// </summary>
            About
        }

        /// <summary>
        /// Changes to the specified view.
        /// </summary>
        /// <param name="page">Page to change to</param>
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