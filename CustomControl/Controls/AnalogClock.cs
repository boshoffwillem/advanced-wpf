using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CustomControl.Controls
{
    public delegate void TimeChangedEventHandler(object sender, TimeChangedEventArgs args);
    
    [TemplatePart(Name = "PART_HourHand", Type = typeof(Line))]
    [TemplatePart(Name = "PART_MinuteHand", Type = typeof(Line))]
    [TemplatePart(Name = "PART_SecondHand", Type = typeof(Line))]
    public class AnalogClock : Clock
    {
        #region Private Fields

        private Line _hourHand;
        private Line _minuteHand;
        private Line _secondHand;

        #endregion

        #region Constructor

        static AnalogClock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnalogClock), 
                new FrameworkPropertyMetadata(typeof(AnalogClock)));
        }

        #endregion

        #region Private Methods

        private void UpdateClockHands(DateTime time)
        {
            _hourHand.RenderTransform = new RotateTransform((time.Hour / 12.0) * 360, 0.5, 0.5);
            _minuteHand.RenderTransform = new RotateTransform((time.Minute / 60.0) * 360, 0.5, 0.5);
            _secondHand.RenderTransform = new RotateTransform((time.Second / 60.0) * 360, 0.5, 0.5);
        }

        #endregion

        #region Protected Methods

        protected override void OnTimeChanged(DateTime newTime)
        {
            UpdateClockHands(newTime);
            base.OnTimeChanged(newTime);
        }

        #endregion

        #region Public Methods

        public override void OnApplyTemplate()
        {
            _hourHand = Template.FindName("PART_HourHand", this) as Line;
            _minuteHand = Template.FindName("PART_MinuteHand", this) as Line;
            _secondHand = Template.FindName("PART_SecondHand", this) as Line;
            base.OnApplyTemplate();
        }

        #endregion
    }
}