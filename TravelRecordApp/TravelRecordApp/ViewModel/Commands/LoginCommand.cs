﻿using System;
using System.Windows.Input;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public MainViewModel ViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public LoginCommand(MainViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            var user = (User)parameter;

            if (user == null)
                return false;

            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.Login();
        }
    }
}
