using System.Collections.ObjectModel;

namespace CartridgesNS.ViewModel
{
    class DevicesVM : BaseVM
    {
        private Model.Devices selRow;
        public Model.Devices SelRow
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

        private Model.Devices newRow = new Model.Devices();
        public Model.Devices NewRow
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

        //public DevicesVM()
        //{
        //    UploadData();
        //}

        public override void UploadData()
        {
            using (Model.BaseContext db = new Model.BaseContext())
            {
                Devices = new ObservableCollection<Model.Devices>(db.Devices.Include("Locations").Include("Cartridges"));
                Locations = new ObservableCollection<Model.Locations>(db.Locations);
                Cartridges = new ObservableCollection<Model.Cartridges>(db.Cartridges);
            }
        }

        public override void AddRow()
        {
            using (Model.BaseContext db = new Model.BaseContext())
            {
                db.Devices.Add(NewRow);
                db.SaveChanges();
            }
        }

        public override void DeleteRow(object obj)
        {
            Model.Devices device = obj as Model.Devices;
            using (Model.BaseContext db = new Model.BaseContext())
            {
                db.Devices.Remove(db.Devices.Find(device.InventoryId));
                db.SaveChanges();
            }
        }

        public override void ClearRow()
        {
            NewRow = new Model.Devices();
        }
    }
}
