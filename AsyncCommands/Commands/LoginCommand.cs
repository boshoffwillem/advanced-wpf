using AsyncCommands.Services;
using AsyncCommands.ViewModels;
using System;
using System.Threading.Tasks;

namespace AsyncCommands.Commands;

public class LoginCommand : AsyncCommandBaseClass
{
    private readonly AuthenticationViewModel _authenticationViewModel;
    private readonly IAuthenticationService _authenticationService;

    public LoginCommand(AuthenticationViewModel AuthenticationViewModel, IAuthenticationService AuthenticationService,
        Action<Exception> onException) : base(onException)
    {
        _authenticationViewModel = AuthenticationViewModel;
        _authenticationService = AuthenticationService;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        _authenticationViewModel.StatusMessage = "Logging in...";
        await _authenticationService.Login(_authenticationViewModel.Username);
        _authenticationViewModel.StatusMessage = "Successfully logged in.";
    }
}
