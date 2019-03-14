using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace CartridgesNS
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Other.DisplayRootRegistry displayRootRegistry = new Other.DisplayRootRegistry();
        ViewModel.MainWindowVM mainWindowVM;

        public App()
        {
            displayRootRegistry.RegisterWindowType<ViewModel.MainWindowVM, MainWindow>();
            displayRootRegistry.RegisterWindowType<ViewModel.ReportDetailsVM, Views.ReportDetails>();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                using (Model.BaseContext db = new Model.BaseContext())
                {
                    Other.Variables.mainLog = new ObservableCollection<Model.MainLog>(db.MainLog);
                    Other.Variables.devices = new ObservableCollection<Model.Devices>(db.Devices);
                    Other.Variables.cartridges = new ObservableCollection<Model.Cartridges>(db.Cartridges);
                    Other.Variables.locations = new ObservableCollection<Model.Locations>(db.Locations);
                    Other.Variables.cartridgeRefillingReports = new ObservableCollection<Model.CartridgeRefillingReports>(db.CartridgeRefillingReports);
                }
            }
            catch (Exception ex)
            {
                string msg = $"{ex.Message}";
                string title = "Ошибка подключения к БД";
                MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}
