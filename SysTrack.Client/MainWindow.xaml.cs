using System.Windows;
using SysTrack.Client.Views;

namespace SysTrack.Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new SignInPage());
        }
    }
}