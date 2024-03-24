using System;
using MobileChargingStation.EventArguments;

namespace MobileChargingStation.Interfaces
{


    public interface IUsbCharger
    {
        // Event triggered on new current value
        public event EventHandler<CurrentValueEventArgs>? CurrentValueEvent;

        // Direct access to the current current value
        public double CurrentValue { get; }

        // Require connection status of the phone
        public bool Connected { get; }

        // Start charging
        public void StartCharge();

        // Stop charging
        public void StopCharge();
        
    }
}

