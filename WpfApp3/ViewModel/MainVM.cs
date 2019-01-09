using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp3.ViewModel
{
    class MainVM : INotifyPropertyChanged
    {
        public MainVM()
        {
            UpdateData();
        }

        //MainLog
        private ObservableCollection<Model.MainLog> mainLog;
        public ObservableCollection<Model.MainLog> MainLog { get { return mainLog; } set { mainLog = value; OnPropertyChanged(); } }

        private Model.MainLog selMainLog = new Model.MainLog();
        public Model.MainLog SelMainLog { get { return selMainLog; } set { selMainLog = value; OnPropertyChanged(); } }

        public ObservableCollection<Model.Locations> locations;
        public ObservableCollection<Model.Locations> Locations { get => locations; set { locations = value; OnPropertyChanged(); } }

        public ObservableCollection<Model.Devices> devices;
        public ObservableCollection<Model.Devices> Devices { get => devices; set { devices = value; OnPropertyChanged(); } }

        public ObservableCollection<Model.Cartridges> cartridges;
        public ObservableCollection<Model.Cartridges> Cartridges { get => cartridges; set { cartridges = value; OnPropertyChanged(); } }

        public ObservableCollection<Model.DeviceAssociation> DeviceAssociation { get; set; }

        private DateTime lastUpdate;
        public DateTime LastUpdate { get { return lastUpdate; } set { lastUpdate = value; OnPropertyChanged(); } }

        //Загрузка данных из базы
        public void UpdateData()
        {
            try
            {
                using (Model.ModelDB db = new Model.ModelDB())
                {
                    MainLog = new ObservableCollection<Model.MainLog>(db.MainLog);
                    Cartridges = new ObservableCollection<Model.Cartridges>(db.DeviceAssociation.Select(p => p.Cartridges).Distinct());
                    Devices = new ObservableCollection<Model.Devices>(db.DeviceAssociation.Select(p => p.Devices).Distinct());
                    Locations = new ObservableCollection<Model.Locations>(db.DeviceAssociation.Select(p => p.Locations).Distinct());
                    DeviceAssociation = new ObservableCollection<Model.DeviceAssociation>(db.DeviceAssociation);
                }
                LastUpdate = DateTime.Now;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Command UpdateData(), error: " + ex.Message);
                return;
            }
        }
        private RelayCommand update;
        public RelayCommand Update { get { return update ?? (update = new RelayCommand(obj => UpdateData())); } }

        private RelayCommand delete;
        public RelayCommand Delete
        {
            get
            {
                return delete ?? (delete = new RelayCommand(obj =>
                {
                    try
                    {
                        using (Model.ModelDB db = new Model.ModelDB())
                        {
                            db.Entry(SelMainLog).State = System.Data.Entity.EntityState.Deleted;
                            db.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Command Delete(), error: " + ex.Message);
                        return;
                    }
                    UpdateData();
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
