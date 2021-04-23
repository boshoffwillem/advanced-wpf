using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace VisualStates
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private DispatcherTimer _timer;

        private string _clockText = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public string ClockText
        {
            get
            {
                return _clockText;
            }
            set
            {
                _clockText = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(ClockText)));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += (s, e) => OnTimeChanged(DateTime.Now);
            _timer.Start();
            DataContext = this;
        }

        private void OnTimeChanged(DateTime currentTime)
        {
            ClockText = currentTime.ToString("HH:mm:ss");
            UpdateVisualState(currentTime);
        }

        private void UpdateVisualState(DateTime currentTime)
        {
            if (currentTime.Hour > 6 && currentTime.Hour < 18)
            {
                // Every control has a VisualStateManager.
                // The name you give you visual state (2nd argument) is
                // what the XAML will use to update the state.
                VisualStateManager.GoToState(ClockButton, "Day", false);
            }
            else
            {
                VisualStateManager.GoToState(ClockButton, "Night", false);
            }
        }
    }
}
