using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace CartridgesNS.ViewModel
{
    class MainWindowVM : INotifyPropertyChanged
    {
        ObservableCollection<Model.Cartridges> cartridges;
        Model.Cartridges cartridgesSelRow;
        ObservableCollection<Model.Devices> devices;
        Model.Devices devicesSelRow;
        ObservableCollection<Model.Locations> locations;
        Model.Locations locationSelRow;
        ObservableCollection<Model.MainLog> mainLog;
        Model.MainLog mainLogSelRow;

        public ObservableCollection<Model.Cartridges> Cartridges { get => cartridges; set => cartridges = value; }
        public Model.Cartridges CartridgesSelRow { get => cartridgesSelRow; set => cartridgesSelRow = value; }
        public ObservableCollection<Model.Devices> Devices { get => devices; set => devices = value; }
        public Model.Devices DevicesSelRow { get => devicesSelRow; set => devicesSelRow = value; }
        public ObservableCollection<Model.Locations> Locations { get => locations; set => locations = value; }
        public Model.Locations LocationSelRow { get => locationSelRow; set => locationSelRow = value; }
        public ObservableCollection<Model.MainLog> MainLog { get => mainLog; set => mainLog = value; }
        public Model.MainLog MainLogSelRow { get => mainLogSelRow; set => mainLogSelRow = value; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop="")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
