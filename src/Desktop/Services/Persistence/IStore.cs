// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:11:2017 / 16:17
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="IStore.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Services.Persistence
{
    using System;
    using System.Collections.Generic;

    public interface IStore<T>
    {
        void Insert(T item);

        void Save();

        T Get(Predicate<T> predicate);

        ICollection<T> GetAll();
    }
}