using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CustomControl.Controls
{
    public class AnalogClock : Control
    {
        private Line _hourHand;
        private Line _minuteHand;
        private Line _secondHand;
        
        public static DependencyProperty ShowSecondsProperty = 
            DependencyProperty.Register("ShowSeconds", typeof(bool), typeof(AnalogClock),
                new PropertyMetadata(true));

        public bool ShowSeconds
        {
            get => (bool) GetValue(ShowSecondsProperty);
            set => SetValue(ShowSecondsProperty, value);
        }
        
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
            /*Binding showSecondHandBinding = new Binding
            {
                Path = new PropertyPath(nameof(ShowSeconds)),
                Source = this,
                Converter = new BooleanToVisibilityConverter()
            };
            _secondHand.SetBinding(VisibilityProperty, showSecondHandBinding);*/
            DispatcherTimer timer = new()
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            timer.Tick += (s, e) => UpdateClock();
            timer.Start();
            base.OnApplyTemplate();
        }

        private void UpdateClock()
        {
            _hourHand.RenderTransform = new RotateTransform((DateTime.Now.Hour / 12.0) * 360, 0.5, 0.5);
            _minuteHand.RenderTransform = new RotateTransform((DateTime.Now.Minute / 60.0) * 360, 0.5, 0.5);
            _secondHand.RenderTransform = new RotateTransform((DateTime.Now.Second / 60.0) * 360, 0.5, 0.5);
        }
    }
}