using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DragDrop.Views;

public partial class ListingView : UserControl
{
    #region Dependency Properties

    public static readonly DependencyProperty ItemDropCommandProperty = DependencyProperty.Register(
        "ItemDropCommand",
        typeof(ICommand),
        typeof(ListingView),
        new PropertyMetadata(default(ICommand)));

    public ICommand ItemDropCommand
    {
        get => (ICommand)GetValue(ItemDropCommandProperty);
        set => SetValue(ItemDropCommandProperty, value);
    }

    public static readonly DependencyProperty IncomingItemProperty = DependencyProperty.Register(
        "IncomingItem",
        typeof(object),
        typeof(ListingView),
        new FrameworkPropertyMetadata(default,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public object IncomingItem
    {
        get => (object)GetValue(IncomingItemProperty);
        set => SetValue(IncomingItemProperty, value);
    }

    public static readonly DependencyProperty ItemRemovedCommandProperty = DependencyProperty.Register(
        "ItemRemovedCommand",
        typeof(ICommand),
        typeof(ListingView),
        new PropertyMetadata(default(ICommand)));

    public ICommand ItemRemovedCommand
    {
        get => (ICommand)GetValue(ItemRemovedCommandProperty);
        set => SetValue(ItemRemovedCommandProperty, value);
    }

    public static readonly DependencyProperty RemovedItemProperty = DependencyProperty.Register(
        "RemovedItem",
        typeof(object),
        typeof(ListingView),
        new FrameworkPropertyMetadata(default, 
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public object RemovedItem
    {
        get => (object)GetValue(RemovedItemProperty);
        set => SetValue(RemovedItemProperty, value);
    }

    public static readonly DependencyProperty ItemInsertedCommandProperty = DependencyProperty.Register(
        "ItemInsertedCommand",
        typeof(ICommand),
        typeof(ListingView),
        new PropertyMetadata(default(ICommand)));

    public ICommand ItemInsertedCommand
    {
        get => (ICommand)GetValue(ItemInsertedCommandProperty);
        set => SetValue(ItemInsertedCommandProperty, value);
    }

    public static readonly DependencyProperty TargetItemProperty = DependencyProperty.Register(
        "TargetItem",
        typeof(object),
        typeof(ListingView),
        new FrameworkPropertyMetadata(default,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public object TargetItem
    {
        get => (object)GetValue(TargetItemProperty);
        set => SetValue(TargetItemProperty, value);
    }

    public static readonly DependencyProperty InsertedItemProperty = DependencyProperty.Register(
        "InsertedItem",
        typeof(object),
        typeof(ListingView),
        new FrameworkPropertyMetadata(default,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public object InsertedItem
    {
        get => (object)GetValue(InsertedItemProperty);
        set => SetValue(InsertedItemProperty, value);
    }

    #endregion
    
    public ListingView()
    {
        InitializeComponent();
    }

    private void ListItem_OnMouseMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton != MouseButtonState.Pressed
            || sender is not FrameworkElement element) return;

        var item = element.DataContext;
        var result = System.Windows.DragDrop.DoDragDrop(
            element,
            new DataObject(DataFormats.Serializable, item),
            DragDropEffects.Move);

        // If drag was not successful, add item back to
        // original list.
        if (result == DragDropEffects.None)
        {
            AddItem(item);
        }
    }

    private void ListItem_DragOver(object sender, DragEventArgs e)
    {
        if (!ItemInsertedCommand?.CanExecute(null) ?? true) return;
        if (sender is not FrameworkElement element) return;
        TargetItem = element.DataContext;
        InsertedItem = e.Data.GetData(DataFormats.Serializable);
        ItemInsertedCommand.Execute(null);
    }

    private void AddItem(object item)
    {
        if (!ItemDropCommand?.CanExecute(null) ?? true) return;
        IncomingItem = item;
        ItemDropCommand.Execute(null);
    }

    private void ListingView_OnDragOver(object sender, DragEventArgs e)
    {
        var item = e.Data.GetData(DataFormats.Serializable);
        AddItem(item);
    }

    private void ListingView_OnDragLeave(object sender, DragEventArgs e)
    {
        var result = VisualTreeHelper.HitTest(listingItems, e.GetPosition(listingItems));
        
        // If we are no longer within the bounds of our list view
        if (result is null)
        {
            if (!ItemRemovedCommand?.CanExecute(null) ?? true) return;
            RemovedItem = e.Data.GetData(DataFormats.Serializable);
            ItemRemovedCommand.Execute(null);
        }
        // Else, we are dragging over items in our list view
        else
        {
            
        }
    }
}