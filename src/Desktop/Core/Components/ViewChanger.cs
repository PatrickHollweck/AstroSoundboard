using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;
using AstroSoundBoard.WPF.Windows;

using log4net;

namespace AstroSoundBoard.Core.Components
{
    /// <summary>
    /// This is a helper class to compensate for my incompetence, also it allows view changing.
    /// </summary>
    internal static class ViewChanger
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly Dictionary<string, UserControl> ViewCache = new Dictionary<string, UserControl>();

        // Instance of the MainWindow active (Gets set in the MainWindow ctor)
        public static MainWindow MainWindowInstance { get; set; }

        /// <summary>
        /// Changes to the specified view.
        /// </summary>
        public static void ChangeViewTo<TPage>() where TPage : UserControl, new()
        {
            var viewName = typeof(TPage).Name;

            if (!ViewCache.ContainsKey(viewName))
            {
                ViewCache.Add(viewName, new TPage());
            }

            ViewCache.TryGetValue(viewName, out UserControl view);

            Log.Info($"Changing view to {viewName}");
            MainWindowInstance.DataContext = view;
        }
    }
}