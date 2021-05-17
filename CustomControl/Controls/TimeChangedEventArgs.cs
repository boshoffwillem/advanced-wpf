using System;
using System.Windows;

namespace CustomControl.Controls
{
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
}