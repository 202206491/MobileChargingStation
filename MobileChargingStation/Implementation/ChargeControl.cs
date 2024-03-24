using MobileChargingStation.Interfaces;
using MobileChargingStation.EventArguments;

namespace MobileChargingStation.Implementation
{
    public class ChargeControl : IChargeControl
    {
        public enum ChargingState { Charging, NotCharging}

        private IDisplay _display;
        private ChargingState _state;
        private IUsbCharger _usbCharger;
        private string _currentMessage = "";

        public ChargeControl(IUsbCharger usbCharger, IDisplay display)
        {
            usbCharger.CurrentValueEvent += UsbCharger_CurrentValueEvent;

            _usbCharger = usbCharger;
            _display = display;
            _state = ChargingState.NotCharging;

        }

        private void UsbCharger_CurrentValueEvent(object sender, CurrentValueEventArgs e) => HandleCurrentValueEvent(sender, e);

        public void HandleCurrentValueEvent(object sender, CurrentValueEventArgs e) 
        {
        
            if(_state == ChargingState.Charging)
            {
                double current = e.Current;
                string newMessage = "";

                switch(current) 
                {
                    case 0:
                        newMessage = "No connection.";
                        break;
                    case > 0 and <= 5:
                        newMessage = "Fully Charged";
                            StopCharge();
                        break;
                    case > 5 and <= 500:
                        newMessage = "Charging";
                        break;
                    case > 500:
                        newMessage = "Overload";
                            StopCharge();
                        break;
                    default:
                        break;
                }
                if(newMessage != _currentMessage)
                {
                    _display.DisplayAnnouncement(newMessage);
                    _currentMessage = newMessage;
                }

            }
      
        }

        public bool IsConnected() => _usbCharger.Connected;

        public void StartCharge() 
        {
            _state = ChargingState.Charging;
            _usbCharger.StartCharge();
        }

        public void StopCharge() 
        {
        _state = ChargingState.NotCharging;
            _usbCharger.StopCharge();
        }

    }
}
