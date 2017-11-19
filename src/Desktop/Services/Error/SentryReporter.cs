// ****************************** Module Header ****************************** //
//
//
// Last Modified: 19:11:2017 / 18:23
// Creation: 19:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SentryReporter.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Services.Error
{
    using System;

    using AstroSoundBoard.Objects;

    using SharpRaven;

    public class SentryReporter : IErrorReporter
    {
        public void Report(Exception error)
        {
            new RavenClient(Credentials.SentryApiKey).Capture(new SharpRaven.Data.SentryEvent(error));
        }

        public void Report(object sender, UnhandledExceptionEventArgs error)
        {
            Report((Exception)error.ExceptionObject);
        }
    }
}