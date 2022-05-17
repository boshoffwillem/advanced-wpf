using System;
using System.Windows;
using System.Windows.Input;
using DragDrop.ViewModels;

namespace DragDrop.Commands;

public class DeleteRectangleCommand : ICommand
{
    private readonly CanvasViewModel _canvasViewModel;

    public DeleteRectangleCommand(CanvasViewModel CanvasViewModel)
    {
        _canvasViewModel = CanvasViewModel;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        // MessageBox.Show($"{_canvasViewModel.RemoveRectangleName} was removed.");
    }

    public event EventHandler? CanExecuteChanged;
}