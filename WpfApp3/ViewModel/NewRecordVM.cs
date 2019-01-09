using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Linq;

namespace WpfApp3.ViewModel
{
    class NewRecordVM : INotifyPropertyChanged
    {
        public NewRecordVM()
        {
            UpdateData();
            ClearNewRecord();
        }

        public ObservableCollection<Model.Locations> locations;
        public ObservableCollection<Model.Locations> Locations { get => locations; set { locations = value; OnPropertyChanged(); } }

        public ObservableCollection<Model.Devices> devices;
        public ObservableCollection<Model.Devices> Devices { get => devices; set { devices = value; OnPropertyChanged(); } }

        public ObservableCollection<Model.Cartridges> cartridges;
        public ObservableCollection<Model.Cartridges> Cartridges { get => cartridges; set { cartridges = value; OnPropertyChanged(); } }

        public ObservableCollection<Model.DeviceAssociation> DeviceAssociation { get; set; }

        private Model.MainLog newRecord;
        public Model.MainLog NewRecord
        {
            get { return newRecord; }
            set
            {
                newRecord = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand add;
        public RelayCommand Add { get { return add ?? (add = new RelayCommand(obj => AddNewRecord())); } }
        private void AddNewRecord()
        {
            try
            {
                using (Model.ModelDB db = new Model.ModelDB())
                {
                    NewRecord.DeviceAssociation = db.DeviceAssociation.Where(p => (p.DeviceId == NewRecord.DeviceId && p.CartridgeId == NewRecord.CartridgeId && p.LocationId == NewRecord.LocationId)).First();
                    db.MainLog.Add(NewRecord);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Command Add(), error: " + ex.Message);
                return;
            }
            ClearNewRecord();
            UpdateData();
        }

        private RelayCommand update;
        public RelayCommand Update { get { return update ?? (update = new RelayCommand(obj => UpdateData())); } }
        public void UpdateData()
        {
            try
            {
                using (Model.ModelDB db = new Model.ModelDB())
                {
                    //Загружаем из базы только те данные, которые
                    //связаны между собой записью в таблице DeviceAssociation
                    Cartridges = new ObservableCollection<Model.Cartridges>(db.DeviceAssociation.Select(p => p.Cartridges).Distinct());
                    Devices = new ObservableCollection<Model.Devices>(db.DeviceAssociation.Select(p => p.Devices).Distinct());
                    Locations = new ObservableCollection<Model.Locations>(db.DeviceAssociation.Select(p => p.Locations).Distinct());
                    DeviceAssociation = new ObservableCollection<Model.DeviceAssociation>(db.DeviceAssociation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Command UpdateData(), error: " + ex.Message);
                return;
            }
        }

        private RelayCommand clear;
        public RelayCommand Clear { get { return clear ?? (clear = new RelayCommand(obj => ClearNewRecord())); } }
        public void ClearNewRecord()
        {
            //Создаём объект новой записи
            NewRecord = new Model.MainLog() { DateTime = DateTime.Now };
            //********************************************
            UpdateData();
        }

        private RelayCommand changedLocation;
        public RelayCommand ChangedLocation
        {
            get
            {
                return changedLocation ?? (changedLocation = new RelayCommand(obj =>
                {
                    if (obj == null) return;

                    //Принимаем в качестве параметра команды выбранный 
                    //пользователем объект Locationscrjkmrj rkfccjd 
                    Model.Locations selectedItem = (obj as Model.Locations);


                    using (Model.ModelDB db = new Model.ModelDB())
                    {
                        //Выбираем записи ассоциаций для выбранной локации
                        var query = db.DeviceAssociation.Where(p => p.LocationId == selectedItem.Id);

                        //Выбираем картриджи доступные в выбранной локации                       
                        Cartridges = new ObservableCollection<Model.Cartridges>(query.Select(p => p.Cartridges).Distinct());

                        //Выбираем устройства доступные в выбранной локации                        
                        Devices = new ObservableCollection<Model.Devices>(query.Select(p => p.Devices).Distinct());
                    }
                }));
            }
        }

        private RelayCommand changedCartridge;
        public RelayCommand ChangedCartridge
        {
            get
            {
                return changedCartridge ?? (changedCartridge = new RelayCommand(obj =>
                {
                    if (obj == null) return;

                    //Принимаем в качестве параметра команды выбранный 
                    //пользователем объект Cartridges
                    Model.Cartridges selectedItem = (obj as Model.Cartridges);

                    using (Model.ModelDB db = new Model.ModelDB())
                    {
                        //Выбираем записи ассоциаций для выбранного картриджа
                        var query = db.DeviceAssociation.Where(p => p.CartridgeId == selectedItem.Id);

                        //Выбираем устройства с выбранным
                        Devices = new ObservableCollection<Model.Devices>(query.Select(p => p.Devices).Distinct());
                    }
                }));
            }
        }

        private RelayCommand changedDevice;
        public RelayCommand ChangedDevice
        {
            get
            {
                return changedDevice ?? (changedDevice = new RelayCommand(obj =>
                {
                    if (obj == null) return;

                    //Принимаем в качестве параметра команды выбранный 
                    //пользователем объект Cartridges
                    Model.Devices selectedItem = (obj as Model.Devices);
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
