using System;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Data.SqlClient;

namespace CartridgesNS.ViewModel
{
    class CartridgesVM : BaseVM
    {
        private Model.Cartridges selRow;
        public Model.Cartridges SelRow
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

        private Model.Cartridges newRow = new Model.Cartridges();
        public Model.Cartridges NewRow
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

        //public CartridgesVM()
        //{
        //    UploadData();
        //}

        public override void UploadData()
        {
            using (Model.BaseContext db = new Model.BaseContext())
            {
                Cartridges = new ObservableCollection<Model.Cartridges>(db.Cartridges);
            }
        }

        public override void AddRow()
        {
            using (Model.BaseContext db = new Model.BaseContext())
            {
                db.Cartridges.Add(NewRow);
                db.SaveChanges();
            }
        }

        public override void ClearRow()
        {
            NewRow = new Model.Cartridges();
        }

        public override void DeleteRow(object obj)
        {
            Model.Cartridges cartridge = (obj as Model.Cartridges);

            using (Model.BaseContext db = new Model.BaseContext())
            {
                db.Cartridges.Remove(db.Cartridges.Find(cartridge.Id));
                db.SaveChanges();
            }

            UploadData();
        }
    }
}
