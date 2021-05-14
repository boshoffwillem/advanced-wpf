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

## 3.3 Adding custom behaviour

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