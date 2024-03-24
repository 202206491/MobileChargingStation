using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileChargingStation.EventArguments
{
    public class RfidEventArgs : EventArgs
    {
        public int Id { get; set; }
    }
}
