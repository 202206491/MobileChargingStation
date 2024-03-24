using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileChargingStation.Interfaces;
using MobileChargingStation.EventArguments;
using MobileChargingStation.Implementation;
using MobileChargingStation.Simulator;

namespace MobileChargingStation.Implementation
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        
        private LadeskabState _state;
        private IChargeControl _charger;
        private int _oldId;
        private IDoor _door;
        private IDisplay _display;
        private ILog _log;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        //  constructor
        public StationControl(IDoor door, IDisplay display, IRfidReader reader, ILog log, IChargeControl charger)
        {
            _state = LadeskabState.Available;

            reader.RfidEvent += OnRfidScan;
            door.DoorOpenedEvent += OnDoorOpened;
            door.DoorClosedEvent += OnDoorClosed;

            _door = door;
            _display = display;
            _log = log;
            _charger = charger;

        }

        private void OnRfidScan(object? sender, RfidEventArgs data) => RfidDetected(data.Id);

        private void OnDoorOpened(object? sender, System.EventArgs? data)
        {
            _state = LadeskabState.DoorOpen;
            _display.DisplayInstruction("Connect your phone.");
        }

        private void OnDoorClosed(object? sender, System.EventArgs? data)
        {
            _state = LadeskabState.Available;
            _display.DisplayInstruction("Scan your RFID tag.");
        }

        private bool IdIsCorrect(int id) => _oldId == id;

        private void DoorOpenHandler(int id) => _display.DisplayInstruction("Connect your phone and close the door.");


        private void RfidDetected(int id)
        {
            switch(_state)
            {
                case LadeskabState.Available:
                    AvailableHandler(id);
                    break;
                case LadeskabState.DoorOpen:
                    DoorOpenHandler(id);
                    break;
                case LadeskabState.Locked:
                    LockedHandler(id);
                    break;
            }
        }

        private void AvailableHandler(int id)
        {
            if (_charger.IsConnected())
            {
                _charger.StartCharge();
                _door.LockDoor();
                _oldId = id;
                _log.WriteToLog($"{DateTime.Now}: Mobile Charging Station is locked with RFID: {id}.");
                _display.DisplayAnnouncement("The locker is locked and your phone is charging. Use your RFID tag to unlock.");
                _state = LadeskabState.Locked;
            }
            else
            {
                _display.DisplayInstruction("Your phone is not connected properly. Try again.");
            }
        }

        private void LockedHandler(int id)
        {
            if (IdIsCorrect(id))
            {
                _charger.StopCharge();
                _door.UnlockDoor();
                _log.WriteToLog($"{DateTime.Now}: Mobile Charging Station is unlocked with RFID: {id}.");
                _display.DisplayInstruction("Take your phone out of the Mobile Charging Station and close the door");
                _state = LadeskabState.Available;
            }
            else
            {
                _display.DisplayInstruction("Wrong RFID tag");
            }
        }
    }
}
