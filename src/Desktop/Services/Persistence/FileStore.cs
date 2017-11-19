// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:11:2017 / 16:24
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="FileStore.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Services.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using AstroSoundBoard.Objects;

    using Newtonsoft.Json;

    internal class FileStore<T> : IStore<T>
    {
        private readonly string storageLocationPostfix;
        private string FullStorageLocationPath => AppSettings.InstallationFilePath + "/" + storageLocationPostfix;

        private readonly List<T> cache;

        public FileStore(string locationToStore = null)
        {
            // Decide which postfix to use.
            storageLocationPostfix = locationToStore ?? $"{typeof(T).Name}.store";

            // Initialize variables
            cache = new List<T>();

            // Check if there was something stored previously, if yes load it into the cache.
            var allFiles = Directory.GetFiles(AppSettings.InstallationFilePath);
            foreach (string file in allFiles)
            {
                if (file.Contains(storageLocationPostfix))
                {
                    string savedContent = File.ReadAllText(FullStorageLocationPath);
                    cache = JsonConvert.DeserializeObject<List<T>>(savedContent);
                    break;
                }
            }
        }

        public void Insert(T item)
        {
            cache.Add(item);
        }

        public void Save()
        {
            File.WriteAllText(storageLocationPostfix, JsonConvert.SerializeObject(cache));
        }

        public T Get(Predicate<T> predicate)
        {
            return cache.Find(predicate);
        }

        public ICollection<T> GetAll()
        {
            return cache;
        }
    }
}