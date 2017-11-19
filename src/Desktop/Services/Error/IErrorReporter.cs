// ****************************** Module Header ****************************** //
//
//
// Last Modified: 19:11:2017 / 18:18
// Creation: 19:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="IErrorReporter.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Services.Error
{
    using System;

    public interface IErrorReporter
    {
        void Report(Exception error);

        void Report(object sender, UnhandledExceptionEventArgs error);
    }
}