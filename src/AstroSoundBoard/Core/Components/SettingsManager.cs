// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:04:2017 / 23:43
// Creation: 16:04:2017
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
			Log.Debug("INIT");

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
			Log.Debug("Registering Sound!");
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
				Log.Error("Error while trying to register Sound...", exception);
				if (File.Exists(AppSettings.SoundSettingsFilePath))
				{
					File.Delete(AppSettings.SoundSettingsFilePath);
				}

				throw;
			}
		}

		public static void WriteSounds()
		{
			Log.Info("Writing sound...");
			if (File.Exists(AppSettings.SoundSettingsFilePath))
			{
				File.Delete(AppSettings.SoundSettingsFilePath);
			}

			File.WriteAllText(AppSettings.SoundSettingsFilePath, JsonConvert.SerializeObject(Cache));
		}

		public static Sound GetSound(string Name)
		{
			Log.Info($"Getting sound for {Name}");

			foreach (Sound item in Cache.SoundList)
			{
				if (item.Name == Name)
				{
					return item;
				}
			}

			RegisterSound(new Sound { Name = Name, IsFavorite = JsonConvert.False });
			return Cache.SoundList[Cache.SoundList.Count + 1];
		}

		public static void RewriteSound(Sound Sounds)
		{
			Log.Info($"Changing Sound of {Sounds.Name}");
			for (int i = 0; i < Cache.SoundList.Count; i++)
			{
				Log.Info(Cache.SoundList[i].Name + " " + Sounds.Name);
				if (Cache.SoundList[i].Name == Sounds.Name)
				{
					Log.Info("Match found...");
					Cache.SoundList[i] = Sounds;
					WriteSounds();
					return;
				}
			}
		}
	}
}