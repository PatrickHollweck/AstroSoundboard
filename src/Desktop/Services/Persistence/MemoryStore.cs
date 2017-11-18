// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:11:2017 / 15:53
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="MemoryStore.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Services.Persistence
{
    using System;
    using System.Collections.Generic;

    internal class MemoryStore<T> : IStore<T>
    {
        private readonly List<T> cache;

        public MemoryStore()
        {
            cache = new List<T>();
        }

        public void Insert(T item)
        {
            cache.Add(item);
        }

        public void Save()
        {
        }

        public T Get(Predicate<T> predicate)
        {
            return cache.Find(predicate);
        }
    }
}