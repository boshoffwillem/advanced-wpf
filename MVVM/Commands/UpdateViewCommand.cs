using MVVM.ViewModels;
using System;
using System.Windows.Input;

namespace MVVM.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel _mainViewModel;

        public UpdateViewCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Home")
            {
                _mainViewModel.SelectedViewModel = new HomeViewModel();
            }
            else if (parameter.ToString() == "Account")
            {
                _mainViewModel.SelectedViewModel = new AccountViewModel();
            }
        }
    }
}