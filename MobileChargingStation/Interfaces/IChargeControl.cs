using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileChargingStation.Interfaces
{
    public interface IChargeControl
    {
        public bool IsConnected();
        public void StartCharge();

        public void StopCharge();

    }
}
