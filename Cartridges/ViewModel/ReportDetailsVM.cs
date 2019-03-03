using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartridgesNS.ViewModel
{
    class ReportDetailsVM : BaseVM
    {
        public ObservableCollection<Model.CartridgeRefillingDetails> CartridgeRefillingDetails
        {
            get
            {
                return Other.Variables.cartridgeRefillingDetails;
            }
            set
            {
                Other.Variables.cartridgeRefillingDetails = value;
            }
        }

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

        public override void AddRow()
        {
            throw new NotImplementedException();
        }

        public override void ClearRow()
        {
            throw new NotImplementedException();
        }

        public override void DeleteRow(object obj)
        {
            throw new NotImplementedException();
        }

        public override void UploadData()
        {
            throw new NotImplementedException();
        }
    }
}
