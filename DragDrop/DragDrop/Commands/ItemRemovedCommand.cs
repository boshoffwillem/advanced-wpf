using System;
using System.Windows.Input;
using DragDrop.ViewModels;

namespace DragDrop.Commands;

public class ItemRemovedCommand : ICommand
{
    private readonly ListingViewModel _listingViewModel;

    public ItemRemovedCommand(ListingViewModel ListingViewModel)
    {
        _listingViewModel = ListingViewModel;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _listingViewModel.RemoveItem(_listingViewModel.RemovedItem);
    }

    public event EventHandler? CanExecuteChanged;
}