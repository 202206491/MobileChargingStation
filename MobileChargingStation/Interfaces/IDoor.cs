using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileChargingStation.Interfaces
{
    public interface IDoor
    {

        public event EventHandler DoorClosedEvent;
        public event EventHandler DoorOpenedEvent;
        public void LockDoor();
        public void UnlockDoor();
    }   
}
