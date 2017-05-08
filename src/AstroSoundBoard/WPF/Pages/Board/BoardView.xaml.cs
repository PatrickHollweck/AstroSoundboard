// ****************************** Module Header ****************************** //
//
//
// Last Modified: 30:04:2017 / 23:24
// Creation: 25:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="BoardView.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Pages.Board
{
    using System.Collections.Generic;
    using System.Windows.Controls;

    using AstroSoundBoard.Core.Components;
    using AstroSoundBoard.Core.Objects.DataObjects;
    using AstroSoundBoard.Core.Objects.DataObjects.SoundDefinitionJsonTypes;
    using AstroSoundBoard.WPF.Controls.Sound;

    using log4net;

    using Newtonsoft.Json;

    using PropertyChanged;

    [ImplementPropertyChanged]
    public partial class BoardView : UserControl
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<SoundView> AllSounds { get; set; } = new List<SoundView>();
        public static BoardView BoardViewInstance { get; set; }

        public BoardView()
        {
            BoardViewInstance = this;

            InitializeComponent();
            DataContext = this;

            foreach (Definition definition in SoundManager.GetSoundList())
            {
                var item = new Sound
                {
                    Description = definition.Info.Description,
                    IsFavorite = SettingsManager.GetSound(definition.Sound.Name).IsFavorite,
                    Name = definition.Sound.Name,
                    VideoLink = definition.Info.VideoLink
                };

                var view = new SoundView(item);

                AllSounds.Add(view);
                ItemCtrl.Items.Add(view);
            }
        }

        public void OnlyShowFavorites(bool show)
        {
            Log.Debug("Board - only showing favs");
            if (show)
            {
                List<SoundView> matchingItems = new List<SoundView>();

                foreach (SoundView item in ItemCtrl.Items)
                {
                    if (item.LocalDefinition.IsFavorite == JsonConvert.True)
                    {
                        matchingItems.Add(item);
                    }
                }

                ItemCtrl.Items.Clear();

                foreach (SoundView view in matchingItems)
                {
                    ItemCtrl.Items.Add(view);
                }
            }
            else
            {
                ItemCtrl.Items.Clear();

                foreach (SoundView view in AllSounds)
                {
                    ItemCtrl.Items.Add(view);
                }
            }
        }

        public void SearchForElement(string element, bool onlyInFavorites)
        {
            if (string.IsNullOrWhiteSpace(element))
            {
                ItemCtrl.Items.Clear();

                foreach (SoundView view in AllSounds)
                {
                    ItemCtrl.Items.Add(view);
                }

                if (onlyInFavorites)
                {
                    OnlyShowFavorites(true);
                }
            }
            else
            {
                List<SoundView> matchingItems = new List<SoundView>();

                foreach (SoundView view in AllSounds)
                {
                    if (view.LocalDefinition.Name.ToLower().Contains(element.ToLower()))
                    {
                        matchingItems.Add(view);
                    }
                }

                ItemCtrl.Items.Clear();

                foreach (SoundView view in matchingItems)
                {
                    ItemCtrl.Items.Add(view);
                }
            }
        }
    }
}