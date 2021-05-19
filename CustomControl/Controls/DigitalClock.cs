using System;
using System.Windows;

namespace CustomControl.Controls
{
    [TemplatePart(Name = "PART_Colon", Type = typeof(UIElement))]
    public class DigitalClock : Clock
    {
        #region Private Fields

        private UIElement _colon;

        #endregion
        
        #region Dependency Properties

        public static readonly DependencyProperty ColonBlinkProperty = 
            DependencyProperty.Register("ColonBlink", typeof(bool), typeof(DigitalClock));

        #endregion

        #region Public Properties

        public bool ColonBlink
        {
            get => (bool) GetValue(ColonBlinkProperty);
            set => SetValue(ColonBlinkProperty, value);
        }

        #endregion

        #region Constructor

        static DigitalClock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DigitalClock), 
                new FrameworkPropertyMetadata(typeof(DigitalClock)));
        }

        #endregion

        #region Protected Methods

        protected override void OnTimeChanged(DateTime newTime)
        {
            if (_colon is not null)
            {
                if (ColonBlink && !ShowSeconds)
                {
                    _colon.Visibility = _colon.IsVisible ? Visibility.Hidden : Visibility.Visible;
                }
                else
                {
                    _colon.Visibility = Visibility.Visible;
                }
            }
            
            base.OnTimeChanged(newTime);
        }

        #endregion

        #region Public Methods

        public override void OnApplyTemplate()
        {
            _colon = Template.FindName("PART_Colon", this) as UIElement;
            base.OnApplyTemplate();
        }

        #endregion
    }
}