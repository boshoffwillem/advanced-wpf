using System;
using System.Windows.Input;
using DragDrop.ViewModels;

namespace DragDrop.Commands;

public class ItemInsertedCommand : ICommand
{
    private readonly ListingViewModel _listingViewModel;

    public ItemInsertedCommand(ListingViewModel ListingViewModel)
    {
        _listingViewModel = ListingViewModel;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _listingViewModel.InsertItem(_listingViewModel.InsertedItem, _listingViewModel.TargetItem);
    }

    public event EventHandler? CanExecuteChanged;
}