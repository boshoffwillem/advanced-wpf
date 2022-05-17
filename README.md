# advanced-wpf
A repository containing advanced WPF concepts


## 1.Visual States

In WPF, visual states are multiple appearances that your control can display, depending on any criteria you set.

We have a button control, that represents a clock. During day time it will have a light visual state, and during night time it will have a darker visual state.

## 2.MVVM

Model-View-ViewModel (MVVM) is an structural design pattern that separates an applications functionality into three parts. 
MVVM is equivalent to Model-View-Controller (MVC).
The three parts are:

### 2.1 Model

Models are the lowest layer of classes in this pattern. They contain all the business data and logic. 
If you have a Person table in a database, for example, you would create a Person model/class that contains all the data that's in the database (usually every field is represented by a property), and any logic than can be applied to a Person.

### 2.2 ViewModel

The layer above the Model is the ViewModel. ViewModels are simply classes that are responsible for converting Model data into formats that can be displayed and used by a View.

### 2.3 View

The highest layer is the View. The view does not know about business data or logic. It's should be as decoupled as possible from any domain logic or data.
The view will simply used data that is exposed by the ViewModel and then render it. The idea is that if you want to change the way you render or display your data, you only have to rewrite your Views, not your Models and ViewModels.

In the example in the repository we have a simple application that demonstrates the separation of data, as well as how to switch between different Views.

## 3.Custom Control

The repo contains an example of creating a custom control: an analog clock. 

### 3.1 Creating a control

To create a control, create a class and inherit from System.Windows.Controls.Control. Then import that namespace in view (XAML) where the control is to be used.
For example, 

```xaml
xmlns:controls="clr-namespace:CustomControl.Controls"
```

Then use it in the view as follows:

```xaml
<controls:AnalogClock />
```

### 3.2 Creating a custom style

Create a new resource dictionary and target the ControlTemplate style of the custom control.
The ControlTemplate defines how the control will look.
Give all the different components of the ControlTemplate a name starting with **PART_**.
This is very important and a convention for controls. Naming something **PART_** allows it to be changed
when overriding/customizing the template.

For example, when creating a custom ControlTemplate for a TextBox, the actual content of the TextBox is a PART:

```xaml
<Style TargetType="TextBox">
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="TextBox">
                <Border BorderThickness="1" 
                        BorderBrush="Gray" 
                        CornerRadius="3">
                    <ScrollViewer x:Name="PART_ContentHost" />
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

### 3.2 Assigning the custom style to the control

To actually assign the style to the control, give the control a static constructor, and override the
DefaultStyleKeyProperty:

```c#
static AnalogClock()
{
    DefaultStyleKeyProperty.OverrideMetadata(typeof(AnalogClock), 
        new FrameworkPropertyMetadata(typeof(AnalogClock)));
}
```

This tells the application that it needs to apply some external style to the control.
BUT, for it to actually find that style, you have to create a folder called **Themes**.
Inside that folder create a resource dictionary called **Generic.xaml**. This is where you will merge all custom styles for controls.
The OverrideMetadata method will look in Themes/Generic.xaml for any custom styles.

### 3.3 Adding custom behaviour

When adding behaviour or logic to the control be sure to do it in the override of OnApplyTemplate.
Otherwise you might try to access the template components/PARTs before it's applied.

## 4.Dependency Properties

Dependency properties are properties for XAML components/controls, like the analog clock built in Section 3.
These properties allow to add WPF features to your components, like data bindings, animations, etc.

Let's add a dependency property to the analog clock to control whether the analog clock should be shown or not.

### 4.1 Steps

Add a static dependency property in the <control>.cs file and register it.

```c#
public static DependencyProperty ShowSecondsProperty = 
            DependencyProperty.Register("ShowSeconds", typeof(bool), typeof(AnalogClock),
                new PropertyMetadata(true));
```

No we need a property in <control>.cs that hooks up to this dependency property.

```c#
public bool ShowSeconds
{
    get => (bool) GetValue(ShowSecondsProperty);
    set => SetValue(ShowSecondsProperty, value);
}
```

No the dependency property is set up, but it doesn't do anything yet.
We need to now tie it to the component that will use the component (in this case the SecondHand).
There is a couple of ways to set up this binding.

**First way**

```c#
_secondHand = Template.FindName("PART_SecondHand", this) as Line;
Binding showSecondHandBinding = new Binding
{
    Path = new PropertyPath(nameof(ShowSeconds)),
    Source = this,
    Converter = new BooleanToVisibilityConverter()
};
_secondHand.SetBinding(VisibilityProperty, showSecondHandBinding);
```

This sets up the binding between the second hand and our dependency property, 
and it specifies that the value of the dependency property sets the visibility of the component. 

This should make the property available on the control:

```xaml
<controls:AnalogClock ShowSeconds="False" />
```

**Second Way**

There is another, easier, way to set up this binding: a TemplateBinding.

Go to the style of the analog clock.

```xaml
<ControlTemplate.Resources>
    <BooleanToVisibilityConverter  x:Key="BooleanToVisibilityConverter" />
</ControlTemplate.Resources>
<Line Stroke="Red"
    StrokeThickness="1"
    VerticalAlignment="Center"
    HorizontalAlignment="Center"
    X1="0"
    X2="-100"
    x:Name="PART_SecondHand"
    Visibility="{TemplateBinding ShowSeconds, Converter={StaticResource BooleanToVisibilityConverter}}" />
```

### 4.2 Dependency Property Metadata

#### 4.2.1 Metadata callbacks

The PropertyMetadata has a parameter for a callback delegate: **PropertyChangedCallback**. 
WPF suggests using this callback to apply any logic to the DependencyProperty instead of doing it in the setter of Property related to the DependencyProperty.
The reason for this is that anytime the dependency property is triggered through XAML bindings, it will not call
the setter of the related Property; it will only execute the callback delegate of the DependencyProperty metadata.

Another callback in the PropertyMetadata is the **CoerceValueCallback**. This callback is very similar
to the **ValidateValueCallback**. The difference is that when the **ValidateValueCallback** returns false,
it throws an exception. The **CoerceValueCallback**, however, when it fails to validate, allows you to modify
the value so that it does pass validation.

**CoerceValueCallback** is called before **PropertyChangedCallback**.

#### 4.2.2 Dependency Property callbacks

There is another callback method that's part of the DependencyProperty and not the PropertyMetadata,
but it's still sort of like metadata: the **ValidateValueCallback**.

This callback gets called before the **CoerceValueCallback** and the **PropertyChangedCallback**.

What you do with this callback is return true if you're happy with the new value (you validate it);
if you return false, an ArgumentException gets raised.

#### 4.2.3 CoerceValueCallback vs. ValidateValueCallback

These callbacks are mutually exclusive, however, they should be used in synchrony.
When you have values that don't validate but they can be changed to be acceptable,
do so with **CoerceValueCallback**. If you have values that don't validate and they
cannot change, handle them with **ValidateValueCallback**.

## 5.Routed Events

Routed events are like normal C# events, just for WPF components.

 We're going to set up an event that updates a text box displaying the current time of the Analog clock control.
 
### Step 1: Define the event

Declare a public static RoutedEvent and register it with the EventManager.

```c#
public static RoutedEvent TimeChangedEvent =
    EventManager.RegisterRoutedEvent("TimeChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AnalogClock));
```

### Step 2: Define the event handler

Set up the event that the RoutedEvent wraps.

```c#
public event RoutedEventHandler TimeChanged
{
    add
    {
        AddHandler(TimeChangedEvent, value);
    }
    remove
    {
        RemoveHandler(TimeChangedEvent, value);
    }
}
```

### Step 3: Invoke the event

Raise the event based on some logic.

```c#
protected virtual void OnTimeChanged(DateTime time)
{
    UpdateClock(time);
    RaiseEvent(new RoutedEventArgs(TimeChangedEvent, this));
}
```

### Step 4: Use the event

The event should now be available

```xaml
<controls:AnalogClock TimeChanged="AnalogClock_OnTimeChanged" />
```

### Step 5: Add custom EventArgs

We want an event args from which we can get time.
Create a new class called TimeChangedEventArgs and inherit from RoutedEventArgs.
Also, define a property for the new time.

```c#
public class TimeChangedEventArgs : RoutedEventArgs
{
    public DateTime NewTime { get; set; }
    
    public TimeChangedEventArgs()
    {
    }

    public TimeChangedEventArgs(RoutedEvent routedEvent) : base(routedEvent)
    {
    }

    public TimeChangedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
    {
    }
}
```

Currently our RoutedEvent uses the RoutedEventHandler delegate. We need to define a new delegate that uses our TimeChangedEventArgs.

```c#
public delegate void TimeChangedEventHandler(object sender, TimeChangedEventArgs args);


public static RoutedEvent TimeChangedEvent =
    EventManager.RegisterRoutedEvent("TimeChanged", RoutingStrategy.Bubble, typeof(TimeChangedEventHandler), typeof(AnalogClock));
    
    
protected virtual void OnTimeChanged(DateTime time)
{
    UpdateClock(time);
    RaiseEvent(new TimeChangedEventArgs(TimeChangedEvent, this)
    {
        NewTime = time
    });
}


private void AnalogClock_OnTimeChanged(object Sender, TimeChangedEventArgs E)
{
    TimeTextBox.Text = E.NewTime.ToString("HH:mm:ss");
}
```

### Routing strategies

Currently the routed event has a strategy of Bubble. The different strategies are:

#### Bubble

When the event is raised, any element that has the element raising the event inside of it, can handle the event.

#### Direct

Only the element firing the event can handle the event.

#### Tunnel

Any element inside the element firing the event can handle the event; so it's the opposite direction of Bubble.

### Different way of custom EventArgs

For very simple event args (has only one property), we can use a built in class instead of defining our own: **RoutedPropertyChangedEventHandler**

It's generic and accepts the type of property you want.

```c#
public static RoutedEvent TimeChangedEvent =
    EventManager.RegisterRoutedEvent("TimeChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<DateTime>), typeof(AnalogClock));
    
    
public event RoutedPropertyChangedEventHandler<DateTime> TimeChanged
{
    add
    {
        AddHandler(TimeChangedEvent, value);
    }
    remove
    {
        RemoveHandler(TimeChangedEvent, value);
    }
}


protected virtual void OnTimeChanged(DateTime time)
{
    UpdateClock(time);
    RaiseEvent(new RoutedPropertyChangedEventArgs<DateTime>(DateTime.Now.AddSeconds(-1), DateTime.Now, TimeChangedEvent));
}


private void AnalogClock_OnTimeChanged(object Sender, RoutedPropertyChangedEventArgs<DateTime> E)
{
    TimeTextBox.Text = E.NewValue.ToString("HH:mm:ss");
}
```

## 6.Control Documentation

You can document custom controls using attributes so that user's know what's in a control in case they want to override to Control's template.

### 6.1 Visual States

Document the various visual states that a control can be in.

```c#
[TemplateVisualState(Name = "Day", GroupName = "TimeStates")]
[TemplateVisualState(Name = "Night", GroupName = "TimeStates")]
public abstract class Clock : Control
{
    private void UpdateVisualState(DateTime time)
    {
        if (time.Hour is > 6 and < 18)
        {
            VisualStateManager.GoToState(this, "Day", false);
        }
        else
        {
            VisualStateManager.GoToState(this, "Night", false);
        }
    }
```

### 6.2 Custom PARTs

You can document the PARTs of your custom control so that users know to reimplement them when overriding the control template.

```c#
[TemplatePart(Name = "PART_HourHand", Type = typeof(Line))]
[TemplatePart(Name = "PART_MinuteHand", Type = typeof(Line))]
[TemplatePart(Name = "PART_SecondHand", Type = typeof(Line))]
public class AnalogClock : Clock
{
    private Line _hourHand;
    private Line _minuteHand;
    private Line _secondHand;

    static AnalogClock()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AnalogClock), 
            new FrameworkPropertyMetadata(typeof(AnalogClock)));
    }

    public override void OnApplyTemplate()
    {
        _hourHand = Template.FindName("PART_HourHand", this) as Line;
        _minuteHand = Template.FindName("PART_MinuteHand", this) as Line;
        _secondHand = Template.FindName("PART_SecondHand", this) as Line;
        base.OnApplyTemplate();
    }
}
```

## 7.DragDrop

The DragDrop project includes:

- Dragging an object around on a canvas
- Dragging objects between canvasses
- Changing the oreder of list view items by dragging them around
- Dragging list view items bewteen list views

### 7.1 Gotchas

- To enable dragging on a canvas you must set its `Background` property
- To prevent items from flickering when dragging disable the `IsHitTestVisible` property
or use `VisualTreeHelper`
