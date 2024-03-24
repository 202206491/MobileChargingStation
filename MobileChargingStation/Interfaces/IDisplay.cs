using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileChargingStation.Interfaces
{
    public interface IDisplay
    {
        public void DisplayInstruction(string msg);
        public void DisplayAnnouncement(string msg);

    }
}
