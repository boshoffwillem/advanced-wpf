using System;
using System.Windows.Input;
using DragDrop.ViewModels;

namespace DragDrop.Commands;

public class ItemReceivedCommand : ICommand
{
    private readonly ListingViewModel _listingViewModel;

    public ItemReceivedCommand(ListingViewModel ListingViewModel)
    {
        _listingViewModel = ListingViewModel;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _listingViewModel.AddItem(_listingViewModel.IncomingItem);
    }

    public event EventHandler? CanExecuteChanged;
}