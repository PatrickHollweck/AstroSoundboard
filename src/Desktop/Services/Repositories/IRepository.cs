// ****************************** Module Header ****************************** //
//
//
// Last Modified: 19:11:2017 / 18:48
// Creation: 19:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="IRepository.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Services.Repositories
{
    using System;
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        T Get(Predicate<T> predicate);

        ICollection<T> GetAll();
    }
}