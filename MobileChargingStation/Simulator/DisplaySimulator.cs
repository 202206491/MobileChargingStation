using MobileChargingStation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileChargingStation.Simulator
{
    public class DisplaySimulator : IDisplay
    {
        public void DisplayInstruction(string msg) => Console.WriteLine(msg);

        public void DisplayAnnouncement(string msg) => Console.WriteLine(msg);
    }  
}
