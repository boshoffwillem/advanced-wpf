using AsyncCommands.ViewModels;
using System.Windows;

namespace AsyncCommands
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new AuthenticationViewModel();
            DataContext = vm;
        }
    }
}
