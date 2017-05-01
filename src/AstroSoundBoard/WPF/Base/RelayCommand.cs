// ****************************** Module Header ****************************** //
//
//
// Last Modified: 30:04:2017 / 13:44
// Creation: 30:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="RelayCommand.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Base
{
    using System;
    using System.Windows.Input;

    internal class RelayCommand : ICommand
    {
        public RelayCommand(Action action)
        {
            LocalAction = action;
        }

        private Action LocalAction { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            LocalAction.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}