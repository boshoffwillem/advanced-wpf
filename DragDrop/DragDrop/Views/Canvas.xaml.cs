using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DragDrop.Views;

public partial class CanvasView : UserControl
{
    public static readonly DependencyProperty IsChildHitTestVisibleProperty =
        DependencyProperty.Register("IsChildHitTestVisible",
            typeof(bool),
            typeof(CanvasView),
            new PropertyMetadata(true));

    public bool IsChildHitTestVisible
    {
        get => (bool)GetValue(IsChildHitTestVisibleProperty);
        set => SetValue(IsChildHitTestVisibleProperty, value);
    }

    public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
        "Color",
        typeof(Brush),
        typeof(CanvasView),
        new PropertyMetadata(default(Brush)));

    public Brush Color
    {
        get => (Brush)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public static readonly DependencyProperty RectangleDropCommandProperty = DependencyProperty.Register(
        "RectangleDropCommand",
        typeof(ICommand),
        typeof(CanvasView),
        new PropertyMetadata(default(ICommand)));

    public ICommand RectangleDropCommand
    {
        get => (ICommand)GetValue(RectangleDropCommandProperty);
        set => SetValue(RectangleDropCommandProperty, value);
    }

    public static readonly DependencyProperty RectangleDeleteCommandProperty = DependencyProperty.Register(
        "RectangleDeleteCommand",
        typeof(ICommand),
        typeof(CanvasView),
        new PropertyMetadata(default(ICommand)));

    public ICommand RectangleDeleteCommand
    {
        get => (ICommand)GetValue(RectangleDeleteCommandProperty);
        set => SetValue(RectangleDeleteCommandProperty, value);
    }

    public static readonly DependencyProperty RemoveRectangleNameProperty = DependencyProperty.Register(
        "RemoveRectangleName",
        typeof(string),
        typeof(CanvasView),
        new FrameworkPropertyMetadata(
            default(string),
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public string RemoveRectangleName
    {
        get => (string)GetValue(RemoveRectangleNameProperty);
        set => SetValue(RemoveRectangleNameProperty, value);
    }
    
    public CanvasView()
    {
        InitializeComponent();
    }
    
    private void Rectangle_OnMouseMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton != MouseButtonState.Pressed) return;

        IsChildHitTestVisible = false;
        System.Windows.DragDrop.DoDragDrop(
            rectangle,
            new DataObject(DataFormats.Serializable, rectangle),
            DragDropEffects.Move);
        IsChildHitTestVisible = true;
    }

    private void Rectangle_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        /*
        IsChildHitTestVisible = false;
        System.Windows.DragDrop.DoDragDrop(
            rectangle,
            new DataObject(DataFormats.Serializable, rectangle),
            DragDropEffects.Move);
        IsChildHitTestVisible = true;
    */
    }

    private void Canvas_OnDrop(object sender, DragEventArgs e)
    {
        var data = e.Data.GetData(DataFormats.Serializable);
        
        if (data is not UIElement element) return;
        
        var dropPosition = e.GetPosition(canvas);
        Canvas.SetLeft(element, dropPosition.X);
        Canvas.SetTop(element, dropPosition.Y);
        RectangleDropCommand?.Execute(null);

        if (canvas.Children.Contains(element)) return;
        
        canvas.Children.Add(element);
    }

    private void Canvas_OnDragOver(object sender, DragEventArgs e)
    {
        var data = e.Data.GetData(DataFormats.Serializable);
        
        if (data is not UIElement element) return;
        
        var dropPosition = e.GetPosition(canvas);
        Canvas.SetLeft(element, dropPosition.X);
        Canvas.SetTop(element, dropPosition.Y);
    }

    private void Canvas_OnDragLeave(object sender, DragEventArgs e)
    {
        var data = e.Data.GetData(DataFormats.Serializable);
        
        if (data is not FrameworkElement element) return;
        
        canvas.Children.Remove(element);
        RemoveRectangleName = element.Name;
        RectangleDeleteCommand?.Execute(null);
    }
}