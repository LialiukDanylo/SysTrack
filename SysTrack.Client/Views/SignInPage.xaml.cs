using System.Windows;
using System.Windows.Controls;
using SysTrack.Client.ViewModels;

namespace SysTrack.Client.Views
{
    public partial class SignInPage : Page
    {
        public SignInPage()
        {
            
            SignInPageVM vm = new SignInPageVM();
            DataContext = vm;
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is SignInPageVM vm)
            {
                vm.Data.Password = PasswordBox.Password;
            }
        }
    }
}
