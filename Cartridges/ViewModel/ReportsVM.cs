using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CartridgesNS.ViewModel
{
    class ReportsVM : BaseVM
    {
        public ObservableCollection<Model.CartridgeRefillingReports> CartridgeRefillingReports
        {
            get
            {
                return Other.Variables.cartridgeRefillingReports;
            }
            set
            {
                Other.Variables.cartridgeRefillingReports = value;
                OnPropertyChanged();
            }
        }

        private Model.CartridgeRefillingReports selRow;
        public Model.CartridgeRefillingReports SelRow
        {
            get
            {
                return selRow;
            }
            set
            {
                selRow = value;
                OnPropertyChanged();
            }
        }

        private Model.CartridgeRefillingReports newRow = new Model.CartridgeRefillingReports() { DateTime = DateTime.Now };
        public Model.CartridgeRefillingReports NewRow
        {
            get
            {
                return newRow;
            }
            set
            {
                newRow = value;
                OnPropertyChanged();
            }
        }

        private ICommand openReportDetailsWindow;
        public ICommand OpenReportDetailsWindow
        {
            get
            {
                if (openReportDetailsWindow == null)
                {
                    openReportDetailsWindow = new OpenReportDetailsWindowCommand(this);
                }
                return openReportDetailsWindow;
            }
        }


        public override void AddRow()
        {
            using (Model.BaseContext db = new Model.BaseContext())
            {
                db.CartridgeRefillingReports.Add(NewRow);
                db.SaveChanges();
            }
        }

        public override void ClearRow()
        {
            NewRow = new Model.CartridgeRefillingReports() { DateTime = DateTime.Now };
        }

        public override void DeleteRow(object obj)
        {
            Model.CartridgeRefillingReports row = obj as Model.CartridgeRefillingReports;
            using (Model.BaseContext db = new Model.BaseContext())
            {
                db.CartridgeRefillingReports.Remove(db.CartridgeRefillingReports.Find(row.Id));
                db.SaveChanges();
            }
        }

        public override void UploadData()
        {
            using (Model.BaseContext db = new Model.BaseContext())
            {
                CartridgeRefillingReports = new ObservableCollection<Model.CartridgeRefillingReports>(
                    db.CartridgeRefillingReports.Include("CartridgeRefillingDetails"));
            }
        }
    }

    abstract class MyCommand : ICommand
    {
        protected ReportsVM _reportsVM;

        public MyCommand(ReportsVM reportsVM)
        {
            _reportsVM = reportsVM;
        }

        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
    }

    class OpenReportDetailsWindowCommand : MyCommand
    {
        public OpenReportDetailsWindowCommand(ReportsVM reportsVM) : base(reportsVM)
        {
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override async void Execute(object parameter)
        {
            var displayRootRegistry = (Application.Current as App).displayRootRegistry;

            var reportDetailsVM = new ReportDetailsVM(parameter as Model.CartridgeRefillingReports);
            await displayRootRegistry.ShowModalPresentation(reportDetailsVM);
        }
    }
}
