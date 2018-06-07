using System;
using System.Windows.Input;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        private RegisterViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public RegisterCommand(RegisterViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            var user = (User)parameter;

            if (user == null)
                return false;

            if (user.Password == user.ConfirmPassword)
            {
                if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
                    return false;

                return true;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            var user = (User)parameter;
            _viewModel.Register(user);
        }
    }
}
