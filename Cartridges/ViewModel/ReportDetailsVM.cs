using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartridgesNS.ViewModel
{
    class ReportDetailsVM : BaseVM
    {
        private Model.CartridgeRefillingReports row;
        public ReportDetailsVM(Model.CartridgeRefillingReports Row)
        {
            row = Row;
            ClearRow();
            UploadData();
        }
        public ReportDetailsVM()
        {

        }

        public ObservableCollection<Model.CartridgeRefillingDetails> CartridgeRefillingDetails { get; set; }
        public ObservableCollection<Model.Services> Services { get; set; }

        private Model.CartridgeRefillingDetails selRow;
        public Model.CartridgeRefillingDetails SelRow
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

        private Model.CartridgeRefillingDetails newRow;
        public Model.CartridgeRefillingDetails NewRow
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

        private string newServiceRow;
        public string NewServiceRow
        {
            get
            {
                return newServiceRow;
            }
            set
            {
                newServiceRow = value;
                OnPropertyChanged();
            }
        }

        public override void AddRow()
        {
            using (Model.BaseContext db = new Model.BaseContext())
            {
                if (Services != null && !Services.Any<Model.Services>(p => p.Name == NewServiceRow))
                {
                    NewRow.Service = Services.Single(p => p.Name == NewServiceRow).Id;                    
                }
                else
                {
                    db.Services.Add(new Model.Services() { Name = NewServiceRow });
                    db.SaveChanges();

                    UploadData();

                    NewRow.Id = Services.Single<Model.Services>(p => p.Name == NewServiceRow).Id;
                }

                db.CartridgeRefillingDetails.Add(NewRow);
                db.SaveChanges();
            }
        }

        public override void ClearRow()
        {
            NewRow = new Model.CartridgeRefillingDetails() { Report = row.Id };
            NewServiceRow = "";
        }

        public override void DeleteRow(object obj)
        {
            Model.CartridgeRefillingDetails dRow = (obj as Model.CartridgeRefillingDetails);
            using (Model.BaseContext db = new Model.BaseContext())
            {
                db.CartridgeRefillingDetails.Remove(db.CartridgeRefillingDetails.Find(dRow.Id));
                db.SaveChanges();
            }
        }

        public override void UploadData()
        {
            using (Model.BaseContext db = new Model.BaseContext())
            {
                CartridgeRefillingDetails = new ObservableCollection<Model.CartridgeRefillingDetails>(
                    db.CartridgeRefillingDetails.Include("Services").Where(p => p.Report == row.Id));
                Services = new ObservableCollection<Model.Services>(db.Services);
            }
        }
    }
}
