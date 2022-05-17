using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DragDrop.Annotations;
using DragDrop.Commands;

namespace DragDrop.ViewModels;

public class CanvasViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? PropertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
    }

    public ICommand SaveRectangleCommand { get; }
    
    public ICommand DeleteRectangleCommand { get; }

    private int _X;

    public int X
    {
        get => _X;
        set
        {
            _X = value;
            OnPropertyChanged();
        }
    }

    private int _Y;

    public int Y
    {
        get => _Y;
        set
        {
            _Y = value;
            OnPropertyChanged();
        }
    }

    private string _removeRectangleName;

    public string RemoveRectangleName
    {
        get => _removeRectangleName;
        set
        {
            _removeRectangleName = value;
            OnPropertyChanged();
        }
    }

    public CanvasViewModel()
    {
        SaveRectangleCommand = new SaveRectangleCommand(this);
        DeleteRectangleCommand = new DeleteRectangleCommand(this);
    }
}