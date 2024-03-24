using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileChargingStation.Interfaces;

namespace MobileChargingStation.Implementation
{
    public class Log : ILog
    {
        private string _path;
        public void SetPath(string path)=>_path=Path.Combine(path, "logfile.txt");
        
        public void WriteToLog(string msg)
        {
            try
            {
                File.AppendAllText(_path, msg + Environment.NewLine);
            }
            catch (IOException e)
            {
                Console.WriteLine($"IOException caught: {e.Message}");
                throw e;
            }
        }
        
        public string ReadLog()
        {
            try
            {
                return File.ReadAllText(_path);
            }
            catch (IOException e)
            {
                Console.WriteLine($"IOException caught: {e.Message}");
                throw e;
            }
        }
    }
}
