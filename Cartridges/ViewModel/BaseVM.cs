using System;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Validation;

namespace CartridgesNS.ViewModel
{
    abstract class BaseVM : INotifyPropertyChanged
    {
        public ObservableCollection<Model.MainLog> MainLog
        {
            get => Other.Variables.mainLog;
            set => Other.Variables.mainLog = value;
        }

        public ObservableCollection<Model.Devices> Devices
        {
            get
            {
                return Other.Variables.devices;
            }
            set
            {
                Other.Variables.devices = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Model.Cartridges> Cartridges
        {
            get
            {
                return Other.Variables.cartridges;
            }
            set
            {
                Other.Variables.cartridges = value;
                OnPropertyChanged();
            }

        }

        public ObservableCollection<Model.Locations> Locations
        {
            get
            {
                return Other.Variables.locations;
            }
            set
            {
                Other.Variables.locations = value;
                OnPropertyChanged();
            }
        }


        private RelayCommand uploadDataCommand;
        public RelayCommand UploadDataCommand =>
            uploadDataCommand ?? (uploadDataCommand = new RelayCommand(obj =>
            {
                try
                {
                    UploadData();
                }
                catch (Exception ex)
                {
                    string msg = $"{ex.Message}\n {ex.InnerException.InnerException.Message}";
                    string title = "Ошибка подключения к БД";
                    MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }));

        private RelayCommand addRowCommand;
        public RelayCommand AddRowCommand =>
            addRowCommand ?? (addRowCommand = new RelayCommand(obj =>
            {
                try
                {
                    AddRow();
                }
                catch (DbEntityValidationException ex)
                {
                    string msg = $"Ошибка добавления записи.\n {ex.Message}?";
                    string title = "Ошибка";
                    MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (ArgumentNullException ex)
                {
                    string msg = $"{ex.Message}";
                    string title = "Непредвиденная ошибка";
                    MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    string msg = $"{ex.Message}\n {ex.InnerException.InnerException.Message}";
                    string title = "Непредвиденная ошибка";
                    MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
                }

                ClearRowCommand.Execute(null);
                UploadDataCommand.Execute(null);
            }));

        private RelayCommand clearRowCommand;
        public RelayCommand ClearRowCommand =>
            clearRowCommand ?? (clearRowCommand = new RelayCommand(obj => ClearRow()));

        private RelayCommand deleteRowCommand;
        public RelayCommand DeleteRowCommand => 
            deleteRowCommand ?? (deleteRowCommand = new RelayCommand(obj =>
            {
                if (obj != null)
                {
                    string msg = $"Вы действительно хотите удалить запись?";
                    string title = "Удаление";
                    MessageBoxResult result = MessageBox.Show(msg, title, MessageBoxButton.YesNo, MessageBoxImage.Asterisk);

                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            DeleteRow(obj);
                        }
                        catch (DbUpdateException ex)
                        {
                            msg = $"{ex.Message}\n {ex.InnerException.InnerException.Message}";
                            title = "Ошибка при удалении";
                            MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        catch (Exception ex)
                        {
                            msg = $"{ex.Message}\n {ex.InnerException.InnerException.Message}";
                            title = "Непредвиденная ошибка";
                            MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        UploadDataCommand.Execute(null);
                    }
                }
            }));


        abstract public void UploadData();
        abstract public void AddRow();
        abstract public void ClearRow();
        abstract public void DeleteRow(object obj);


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
