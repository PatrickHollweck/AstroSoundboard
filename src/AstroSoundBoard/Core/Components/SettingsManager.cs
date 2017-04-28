// ****************************** Module Header ****************************** //
//
//
// Last Modified: 28:04:2017 / 22:47
// Creation: 25:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SettingsManager.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Components
{
    using System;
    using System.IO;

    using AstroSoundBoard.Core.Objects;
    using AstroSoundBoard.Core.Objects.DataObjects;

    using log4net;

    using Newtonsoft.Json;

    public class SettingsManager
	{
		private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private static Sounds Cache { get; set; }

		public static void Init()
		{
			Log.Debug("Starting the SettingsManager");

			if (!File.Exists(AppSettings.SoundSettingsFilePath))
			{
				CreateStandardFile();
			}
			else
			{
				try
				{
					var readText = File.ReadAllText(AppSettings.SoundSettingsFilePath);

					if (readText == string.Empty)
					{
						File.Delete(AppSettings.SoundSettingsFilePath);
						CreateStandardFile();
					}

					Cache = JsonConvert.DeserializeObject<Sounds>(readText);
				}
				catch (Exception exception)
				{
					Log.Error("Something failed.", exception);
				}
			}
		}

		private static void CreateStandardFile()
		{
			Sound stdObject = new Sound();

			Sound stdSounds = new Sound
				{
					Name = "DummyItem",
					IsFavorite = JsonConvert.False
				};

			ResetCache();
			Cache.SoundList.Add(stdSounds);
			File.WriteAllText(AppSettings.SoundSettingsFilePath, JsonConvert.SerializeObject(stdObject));
		}

		private static void ResetCache()
		{
			Cache = null;
			Cache = new Sounds();
		}

		private static void RegisterSound(Sound sound)
		{
			Log.Debug("Registering Definition!");
			Cache.SoundList.Add(sound);
			WriteSounds();
		}

		public static void RegisterSoundIfNotExists(Sound sound)
		{
			try
			{
				for (int i = 0; i < Cache.SoundList.Count; i++)
				{
					if (Cache.SoundList[i].Name == sound.Name)
					{
						break;
					}
					else if (i == Cache.SoundList.Count - 1)
					{
						RegisterSound(sound);
					}
				}
			}
			catch (Exception exception)
			{
				Log.Error("Error while trying to register Definition...", exception);
				if (File.Exists(AppSettings.SoundSettingsFilePath))
				{
					File.Delete(AppSettings.SoundSettingsFilePath);
				}

				throw;
			}
		}

		public static void WriteSounds()
		{
			Log.Debug("Writing sound...");
			if (File.Exists(AppSettings.SoundSettingsFilePath))
			{
				File.Delete(AppSettings.SoundSettingsFilePath);
			}

			File.WriteAllText(AppSettings.SoundSettingsFilePath, JsonConvert.SerializeObject(Cache));
		}

		public static Sound GetSound(string name)
		{
			foreach (Sound item in Cache.SoundList)
			{
				if (item.Name == name)
				{
					return item;
				}
			}

			RegisterSound(new Sound { Name = name, IsFavorite = JsonConvert.False });
			return GetSound(name);
		}

		public static void RewriteSound(Sound sound)
		{
			if (sound.Name.Contains(" "))
			{
				sound.Name = sound.Name.Replace(' ', '_');
			}

			Log.Debug($"Changing Definition of {sound.Name}");
			for (int i = 0; i < Cache.SoundList.Count; i++)
			{
				if (Cache.SoundList[i].Name == sound.Name)
				{
					Cache.SoundList[i] = sound;
					WriteSounds();
					return;
				}
			}

			Log.Error("NO MATCH!");
		}
	}
}