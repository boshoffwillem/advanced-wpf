using System.ComponentModel;
using System.Runtime.CompilerServices;
using DragDrop.Annotations;

namespace DragDrop.ViewModels;

public class ListItemViewModel : INotifyPropertyChanged
{
    private string _description;

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? PropertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
    }

    public ListItemViewModel(string description)
    {
        _description = description;
    }

    public override string ToString()
    {
        return Description;
    }
}