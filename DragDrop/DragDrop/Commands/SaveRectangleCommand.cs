using System;
using System.Windows;
using System.Windows.Input;
using DragDrop.ViewModels;

namespace DragDrop.Commands;

public class SaveRectangleCommand : ICommand
{
    private readonly CanvasViewModel _canvasViewModel;

    public SaveRectangleCommand(CanvasViewModel CanvasViewModel)
    {
        _canvasViewModel = CanvasViewModel;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        MessageBox.Show("Successfully saved the rectangle to" +
                        $"\nX: {_canvasViewModel.X}" +
                        $"\nY: {_canvasViewModel.Y}");
    }

    public event EventHandler? CanExecuteChanged;
}