using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SysTrack.Client.MVVM
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected void OnPropetryChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
