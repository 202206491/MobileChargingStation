using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileChargingStation.Interfaces
{
    public interface ILog
    {
        public void SetPath(string path); //not done yet
        public void WriteToLog(string msg);
        public string ReadLog(); //not done yet
    }
}
