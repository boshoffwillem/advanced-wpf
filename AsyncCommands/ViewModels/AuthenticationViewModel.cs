using AsyncCommands.Commands;
using AsyncCommands.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AsyncCommands.ViewModels;

public class AuthenticationViewModel : INotifyPropertyChanged
{
    private string _statusMessage;

    public ICommand LoginCommand { get; set; }

    public ICommand LoginCommandRelay { get; set; }

    public string StatusMessage
    {
        get => _statusMessage;
        set
        {
            _statusMessage = value;
            OnPropertyChanged();
        }
    }

    public string Username { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public AuthenticationViewModel()
    {
        LoginCommand = new LoginCommand(this, new AuthenticationService(),
            e =>
            {
                StatusMessage = $"Exception occurred: {e}";
            });
        LoginCommandRelay = new AsyncRelayCommand(e =>
        {
            StatusMessage = e.Message;
        },
        LoginRelay);
    }

    private async Task LoginRelay()
    {
        StatusMessage = "Logging in...";
        await new AuthenticationService().Login(Username);
        StatusMessage = "Successfully logged in.";
    }
}
