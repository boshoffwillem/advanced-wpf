using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AsyncCommands.Commands;

public abstract class AsyncCommandBaseClass : ICommand
{
    private readonly Action<Exception> _onException;

    protected AsyncCommandBaseClass(Action<Exception> onException)
    {
        _onException = onException;
    }

    private bool _isExecuting;

    public bool IsExecuting
    {
        get => _isExecuting;
        set
        {
            _isExecuting= value;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }


    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return !IsExecuting;
    }

    public async void Execute(object parameter)
    {
        IsExecuting = true;
        try
        {
            //throw new InvalidOperationException("Testing...");
            await ExecuteAsync(parameter);

        }
        catch (Exception e)
        {
            _onException?.Invoke(e);
        }
        IsExecuting= false;
    }

    public abstract Task ExecuteAsync(object parameter);
}
