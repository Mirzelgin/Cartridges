using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartridgesNS.ViewModel
{
    class LocationsVM : BaseVM
    {
        private Model.Locations selRow;
        public Model.Locations SelRow
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

        private Model.Locations newRow;
        public Model.Locations NewRow
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
                db.Locations.Add(NewRow);
                db.SaveChanges();
            }
        }

        public override void ClearRow()
        {
            NewRow = new Model.Locations();
        }

        public override void DeleteRow(object obj)
        {
            Model.Locations locations = obj as Model.Locations;
            using (Model.BaseContext db = new Model.BaseContext())
            {
                db.Locations.Remove(db.Locations.Find(locations.Id));
                db.SaveChanges();
            }
        }

        public override void UploadData()
        {
            using (Model.BaseContext db = new Model.BaseContext())
            {
                Locations = new ObservableCollection<Model.Locations>(db.Locations);
            }
        }
    }
}
