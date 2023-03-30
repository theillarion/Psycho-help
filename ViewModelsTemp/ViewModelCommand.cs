﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Xk7.ViewModels
{
    public class ViewModelCommand : ICommand
    {
  

        private readonly Action<object> _executeAction;
        private readonly Predicate<object> _canExecutePredicate;


        public ViewModelCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
            _canExecutePredicate = null;
        }

        public ViewModelCommand(Action<object> executeAction, Predicate<object> canExecutePredicate)
        {
            _executeAction = executeAction;
            _canExecutePredicate = canExecutePredicate;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object? parameter)
        {
            return _canExecutePredicate == null ? true : _canExecutePredicate(parameter);   
        }

        public void Execute(object? parameter)
        {
            _executeAction(parameter);
        }
    }
}
