using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

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

        private Model.CartridgeRefillingReports newRow;
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
}
