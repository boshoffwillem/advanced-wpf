using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CustomControl.Controls
{
    [TemplateVisualState(Name = "Day", GroupName = "TimeStates")]
    [TemplateVisualState(Name = "Night", GroupName = "TimeStates")]
    public abstract class Clock : Control
    {
        #region Dependency Properties

        public static readonly DependencyProperty ShowSecondsProperty = 
            DependencyProperty.Register("ShowSeconds", typeof(bool), typeof(Clock),
                new PropertyMetadata(true));

        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(Clock),
                new PropertyMetadata(DateTime.Now));

        #endregion

        #region Routed Events

        public static readonly RoutedEvent TimeChangedEvent =
            EventManager.RegisterRoutedEvent("TimeChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<DateTime>), typeof(Clock));
        
        public event RoutedPropertyChangedEventHandler<DateTime> TimeChanged
        {
            add => AddHandler(TimeChangedEvent, value);
            remove => RemoveHandler(TimeChangedEvent, value);
        }

        #endregion

        #region Public Properties

        public bool ShowSeconds
        {
            get => (bool) GetValue(ShowSecondsProperty);
            set => SetValue(ShowSecondsProperty, value);
        }

        public DateTime CurrentTime
        {
            get => (DateTime) GetValue(CurrentTimeProperty);
            set => SetValue(CurrentTimeProperty, value);
        }

        #endregion

        #region Private Methods

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

        #endregion

        #region Protected Methods

        protected virtual void OnTimeChanged(DateTime newTime)
        {
            UpdateVisualState(newTime);
            RaiseEvent(new RoutedPropertyChangedEventArgs<DateTime>(CurrentTime, newTime, TimeChangedEvent));
            CurrentTime = newTime;
        }

        #endregion

        #region Public Methods

        public override void OnApplyTemplate()
        {
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
            timer.Tick += (s, e) => OnTimeChanged(DateTime.Now);
            timer.Start();
            base.OnApplyTemplate();
        }

        #endregion
    }
}