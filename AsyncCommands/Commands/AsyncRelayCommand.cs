using System;
using System.Threading.Tasks;

namespace AsyncCommands.Commands;

public class AsyncRelayCommand : AsyncCommandBaseClass
{
    private readonly Func<Task> _callback;

    public AsyncRelayCommand(Action<Exception> onException, Func<Task> callback) : base(onException)
    {
        _callback = callback;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        await _callback();
    }
}
