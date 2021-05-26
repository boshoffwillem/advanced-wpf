using System.Windows;
using System.Windows.Controls;

namespace CustomControl.Controls
{
    public partial class ControllableClock : UserControl
    {
        public static readonly DependencyProperty ClockProperty = 
            DependencyProperty.Register("Clock", typeof(Clock), typeof(ControllableClock), new PropertyMetadata(null));

        public Clock Clock
        {
            get => (Clock) GetValue(ClockProperty);
            set => SetValue(ClockProperty, value);
        }
        
        public ControllableClock()
        {
            InitializeComponent();
        }
    }
}