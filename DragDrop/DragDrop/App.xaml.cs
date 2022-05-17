using System.Windows;
using DragDrop.ViewModels;

namespace DragDrop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var leftList = new ListingViewModel
        {
            ListName = "Left List"
        };
        leftList.AddItem(new ListItemViewModel("Item 1"));
        leftList.AddItem(new ListItemViewModel("Item 2"));
        leftList.AddItem(new ListItemViewModel("Item 5"));
        leftList.AddItem(new ListItemViewModel("Item 6"));
        leftList.AddItem(new ListItemViewModel("Item 7"));
        var rightList = new ListingViewModel
        {
            ListName = "Right List"
        };
        rightList.AddItem(new ListItemViewModel("Item 3"));
        rightList.AddItem(new ListItemViewModel("Item 4"));
        rightList.AddItem(new ListItemViewModel("Item 8"));
        rightList.AddItem(new ListItemViewModel("Item 9"));
        rightList.AddItem(new ListItemViewModel("Item 10"));
        
        MainWindow = new MainWindow
        {
            DataContext = new MainViewModel
            {
                CanvasLeft = new CanvasViewModel(),
                CanvasRight = new CanvasViewModel(),
                ListingLeft = leftList,
                ListingRight = rightList
            }
        };
        MainWindow.Show();
        base.OnStartup(e);
    }
}