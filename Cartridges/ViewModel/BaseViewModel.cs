using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace CartridgesNS.ViewModel
{
    class BaseViewModel : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public virtual void Free()
        {

        }

        public virtual void DoWork()
        {

        }
    }
}
