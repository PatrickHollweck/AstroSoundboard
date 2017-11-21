// ****************************** Module Header ****************************** //
//
//
// Last Modified: 21:11:2017 / 20:13
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="ItemManager.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Controls;

    using AstroSoundBoard.Models.DataModels;
    using AstroSoundBoard.Objects;
    using AstroSoundBoard.Objects.Interfaces;
    using AstroSoundBoard.Services.Repositories;

    using FetzDeLib.Extensions;

    using Newtonsoft.Json;

    public class ItemManager<TView>
        where TView : class, IAddableView
    {
        private List<SoundModel> models = new List<SoundModel>();
        private readonly List<TView> views = new List<TView>();
        private readonly Func<SoundModel, TView> creationDelegate;
        private bool CurrentFavoriteStatus { get; set; }

        public ItemManager(Func<SoundModel, TView> creationDelegate)
        {
            this.creationDelegate = creationDelegate;
            Reload();
        }

        private void Reload()
        {
            models = SettingsManager.GetSounds();

            if (models.Count < SoundRepository.GetAll().Count)
            {
                foreach (var definition in SoundRepository.GetAll())
                {
                    var sound = SoundModel.GetModel(definition);

                    if (!models.Contains(sound))
                    {
                        SettingsManager.Cache.Add(sound);
                    }
                }

                SettingsManager.Save();
            }

            views.Clear();
            foreach (SoundModel model in models)
            {
                views.Add(creationDelegate(model));
            }
        }

        public void Search(ref ItemsControl itemControl, string element)
        {
            /* BEGIN EASTER EGG CODE */
            string[] links =
                {
                    "http://stackoverflow.com/admin.php",
                    "https://youtu.be/dWja_zn_ftc",
                    "https://youtu.be/LVGRAd8TuaQ",
                    "https://youtu.be/k7VcSMr3hbc",
                    "https://youtu.be/K5tVbVu9Mkg"
                };

            if (element.ToLower().EqualsAnyOf("static", "void", "main", "public"))
            {
                Process.Start(links[AppSettings.Rng.Next(0, links.Length)]);
            }

            /* END EASTER EGG CODE */
            itemControl.Items.Filter = item => Filter(item as TView);

            bool Filter(IAddableView model)
            {
                if (model == null)
                {
                    return false;
                }

                return model.SoundModel.Sound.Name.ToLower().Contains(element.ToLower());
            }
        }

        public void ToogleFavorites(ref ItemsControl itemControl)
        {
            CurrentFavoriteStatus = !CurrentFavoriteStatus;

            itemControl.Items.Filter = item => Filter(item as TView);

            bool Filter(IAddableView model)
            {
                if (model == null)
                {
                    return false;
                }

                bool isFavorite = model.SoundModel.Sound.IsFavorite == JsonConvert.True;
                if (CurrentFavoriteStatus)
                {
                    return isFavorite;
                }
                else
                {
                    return !isFavorite;
                }
            }
        }

        public void SetAll(ref ItemsControl itemControl)
        {
            Reload();
            try
            {
                itemControl.Items.Clear();
                foreach (TView view in views)
                {
                    itemControl.Items.Add(view);
                }
            }
            catch
            {
                // Eat
            }
        }
    }
}