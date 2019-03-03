using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartridgesNS.Other
{
    static class Variables
    {
        static public ObservableCollection<Model.MainLog> mainLog;
        static public ObservableCollection<Model.Devices> devices;
        static public ObservableCollection<Model.Cartridges> cartridges;
        static public ObservableCollection<Model.Locations> locations;

        static public ObservableCollection<Model.CartridgeRefillingReports> cartridgeRefillingReports;
        static public ObservableCollection<Model.CartridgeRefillingDetails> cartridgeRefillingDetails;
    }
}
