using System;
using System.Windows;
using CustomControl.Controls;

namespace CustomControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AnalogClock_OnTimeChanged(object Sender, RoutedPropertyChangedEventArgs<DateTime> E)
        {
            TimeTextBox.Text = E.NewValue.ToString("HH:mm:ss");
        }
    }
}