using System.Diagnostics;
using SysTrack.Client.Models;
using SysTrack.Client.MVVM;
using SysTrack.Client.Services;
using SysTrack.Shared.Models;

namespace SysTrack.Client.ViewModels
{
    internal class SignInPageVM : ViewModelBase
    {
        private UserData _data;
        public UserData Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropetryChanged();
            }
        }
        public RelayCommand SignInCommand => new RelayCommand(execute => SignIn()); 

        public SignInPageVM()
        {
            _data = new UserData();
        }

        private async void SignIn()
        {
            var loginData = new LoginRequest
            {
                Name = _data.Name,
                Password = _data.Password
            };
            var auth = new AuthService(new System.Net.Http.HttpClient());
            var response = await auth.SignIn(loginData);

            Debug.WriteLine("Response:");
            Debug.WriteLine(response.Success);
            Debug.WriteLine(response.Token + "\n");
        }
    }
}