using MobileChargingStation.Interfaces;
using MobileChargingStation.Simulator;
using MobileChargingStation.Implementation;
using MobileChargingStation;
using System;

namespace MCSConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            //hej omid igen

            DoorSim door = new DoorSim();
            RfidReaderSim reader = new RfidReaderSim();
            IDisplay display = new DisplaySimulator();
            UsbChargerSimulator usbCharger = new UsbChargerSimulator();
            ChargeControl chargeControl = new ChargeControl(usbCharger, display);
            ILog logger = new Log();
            StationControl stationControl = new StationControl(door, display, reader, logger, chargeControl);



            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E for Exit, O for Open, C for Close, R for Rfid, U for Usbconnect: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        System.Console.WriteLine("Opening door: ");
                        door.DoorOpened();
                        break;

                    case 'C':
                        System.Console.WriteLine("Closing door: ");
                        door.DoorClosed();
                        break;

                    case 'R':
                        System.Console.WriteLine("Enter RFID id: ");
                        string idString = System.Console.ReadLine();
                        int id = Convert.ToInt32(idString);
                        reader.Scan(id);
                        break;
                    case 'U':
                        System.Console.WriteLine("Phone Connected: ");
                        usbCharger.SimulateConnected(true);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}

