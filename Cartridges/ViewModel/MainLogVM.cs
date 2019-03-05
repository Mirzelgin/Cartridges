using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace CartridgesNS.ViewModel
{
    class MainLogVM : BaseVM
    {

        private Model.MainLog selRow;
        public Model.MainLog SelRow
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

        private Model.MainLog newRow = new Model.MainLog() { DateTime = DateTime.Now };
        public Model.MainLog NewRow
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
 
        public override void UploadData()
        {
            using (Model.BaseContext db = new Model.BaseContext())
            {
                MainLog = new ObservableCollection<Model.MainLog>(db.MainLog);
                Devices = new ObservableCollection<Model.Devices>(db.Devices.Include("Locations").Include("Cartridges"));
            }
        }

        public override void AddRow()
        {
            using (Model.BaseContext db = new Model.BaseContext())
            {
                db.MainLog.Add(NewRow);
                db.SaveChanges();
            }
        }

        public override void DeleteRow(object obj)
        {
            Model.MainLog mainLog = obj as Model.MainLog;
            using (Model.BaseContext db = new Model.BaseContext())
            {
                db.MainLog.Remove(db.MainLog.Find(mainLog.Id));
                db.SaveChanges();
            }
        }

        public override void ClearRow()
        {
            NewRow = new Model.MainLog() { DateTime = DateTime.Now };
        }
    }
}
