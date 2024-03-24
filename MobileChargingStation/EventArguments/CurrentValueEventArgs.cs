using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileChargingStation.EventArguments
{
     public class CurrentValueEventArgs : EventArgs
    {
        public double Current { get; set; }
    }
}
