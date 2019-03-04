using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using CartridgesNS.Model;

namespace CartridgesNS.ViewModel
{
    class MainWindowVM : INotifyPropertyChanged
    {
        Model.BaseContext db = new BaseContext();

        private ObservableCollection<Model.Cartridges> cartridges;
        private Model.Cartridges cartridgesSelRow;
        private Model.Cartridges cartridgesNewRow;
        private ObservableCollection<Model.Devices> devices;
        private Model.Devices devicesSelRow;
        private Model.Devices devicesNewRow;
        private ObservableCollection<Model.Locations> locations;
        private Model.Locations locationSelRow;
        private Model.Locations locationNewRow;
        private ObservableCollection<Model.MainLog> mainLog;
        private Model.MainLog mainLogSelRow;
        private Model.MainLog mainLogNewRow;

        public ObservableCollection<Cartridges> Cartridges { get => cartridges; set => cartridges = value; }
        public Cartridges CartridgesSelRow { get => cartridgesSelRow; set => cartridgesSelRow = value; }
        public Cartridges CartridgesNewRow { get => cartridgesNewRow; set => cartridgesNewRow = value; }
        public ObservableCollection<Devices> Devices { get => devices; set => devices = value; }
        public Devices DevicesSelRow { get => devicesSelRow; set => devicesSelRow = value; }
        public Devices DevicesNewRow { get => devicesNewRow; set => devicesNewRow = value; }
        public ObservableCollection<Locations> Locations { get => locations; set => locations = value; }
        public Locations LocationSelRow { get => locationSelRow; set => locationSelRow = value; }
        public Locations LocationNewRow { get => locationNewRow; set => locationNewRow = value; }
        public ObservableCollection<MainLog> MainLog { get => mainLog; set => mainLog = value; }
        public MainLog MainLogSelRow { get => mainLogSelRow; set => mainLogSelRow = value; }
        public MainLog MainLogNewRow { get => mainLogNewRow; set => mainLogNewRow = value; }

        private RelayCommand addRowCommnad;
        public MainWindowVM()
        {
            MainLog = new ObservableCollection<MainLog>(db.MainLog.Include("Devices"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop="")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
