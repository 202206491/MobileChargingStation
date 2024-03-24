using MobileChargingStation.EventArguments;
using MobileChargingStation.Interfaces;

namespace MobileChargingStation.Simulator
{
    public class RfidReaderSim : IRfidReader
    {
        public event EventHandler<RfidEventArgs> RfidEvent;
        public void Scan(int id) => OnRfidScan(new RfidEventArgs { Id = id });

        private void OnRfidScan(RfidEventArgs e) => RfidEvent?.Invoke(this, e);

    }
}