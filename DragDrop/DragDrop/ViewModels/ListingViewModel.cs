using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DragDrop.Annotations;
using DragDrop.Commands;

namespace DragDrop.ViewModels;

public class ListingViewModel : INotifyPropertyChanged
{
    private readonly ObservableCollection<ListItemViewModel> _listItems;
    private string _listName;

    public string ListName
    {
        get => _listName;
        set
        {
            _listName = value;
            OnPropertyChanged();
        }
    }

    private ListItemViewModel _incomingItem;

    public ListItemViewModel IncomingItem
    {
        get => _incomingItem;
        set
        {
            _incomingItem = value;
            OnPropertyChanged();
        }
    }

    private ListItemViewModel _removedItem;

    public ListItemViewModel RemovedItem
    {
        get => _removedItem;
        set
        {
            _removedItem = value;
            OnPropertyChanged();
        }
    }

    private ListItemViewModel _insertedItem;

    public ListItemViewModel InsertedItem
    {   
        get => _insertedItem;
        set
        {
            _insertedItem = value;
            OnPropertyChanged();
        }
    }

    private ListItemViewModel _targetItem;

    public ListItemViewModel TargetItem
    {
        get => _targetItem;
        set
        {
            _targetItem = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    private void OnPropertyChanged([CallerMemberName] string? PropertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
    }
    
    public IEnumerable<ListItemViewModel> ListItems => _listItems;

    public ICommand ItemReceivedCommand { get; }
    
    public ICommand ItemRemovedCommand { get; }
    
    public ICommand ItemInsertedCommand { get; }

    public ListingViewModel()
    {
        _listItems = new ObservableCollection<ListItemViewModel>();
        ItemReceivedCommand = new ItemReceivedCommand(this);
        ItemRemovedCommand = new ItemRemovedCommand(this);
        ItemInsertedCommand = new ItemInsertedCommand(this);
    }

    public void AddItem(ListItemViewModel item)
    {
        if (_listItems.Contains(item)) return;
        _listItems.Add(item);
    }
    
    public void RemoveItem(ListItemViewModel item)
    {
        _listItems.Remove(item);
    }

    public void InsertItem(ListItemViewModel item, ListItemViewModel targetItem)
    {
        if (item == targetItem) return;
        
        var oldIndex = _listItems.IndexOf(item);
        var newIndex = _listItems.IndexOf(targetItem);

        if (oldIndex == -1 || newIndex == -1) return;
        _listItems.Move(oldIndex, newIndex);
    }
}