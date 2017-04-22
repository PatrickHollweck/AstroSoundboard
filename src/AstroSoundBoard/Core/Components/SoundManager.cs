// ****************************** Module Header ****************************** //
//
//
// Last Modified: 22:04:2017 / 15:52
// Creation: 17:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SoundManager.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Components
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Resources;

    using AstroSoundBoard.Core.Objects.DataObjects.SoundDefinition;
    using AstroSoundBoard.Core.Objects.DataObjects.SoundDefinitionJsonTypes;

    using log4net;

    using Newtonsoft.Json;

    public static class SoundManager
    {
        // These variables are getting assigned in the Startup Process in the Updater class!
        private static SoundDefinitions SoundDefinition { get; set; }
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // I could have used a static constructor but I like having more control over when this will happen!
        public static void Init()
        {
            Log.Info("Starting in Sound Manager!");
            Updater.GetDefinition();
        }

        public static class Storage
        {
            public static object GetAudioFileFromResources(string value)
            {
                ResourceSet resourceSet = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

                foreach (DictionaryEntry item in resourceSet)
                {
                    if (item.Key.ToString() == value)
                    {
                        return item.Value;
                    }
                }

                return null;
            }
        }

        public static class Information
        {
            public static Definition GetSoundInfo(string name)
            {
                foreach (Definition item in SoundDefinition.SoundList)
                {
                    if (item.Sound.Name == name)
                    {
                        return item;
                    }
                }

                return null;
            }

            public static List<Definition> GetSoundList()
            {
                return SoundDefinition.SoundList;
            }
        }

        internal static class Updater
        {
            internal static void GetDefinition()
            {
                try
                {
                    SoundDefinition = JsonConvert.DeserializeObject<SoundDefinitions>(Properties.Resources.SoundDefinition);
                }
                catch (Exception exception)
                {
                    Log.Fatal("Can not de-serialize the downloaded Json!", exception);
                }
            }
        }
    }
}