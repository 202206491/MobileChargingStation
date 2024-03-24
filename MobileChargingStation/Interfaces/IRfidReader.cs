using MobileChargingStation.EventArguments;

namespace MobileChargingStation.Interfaces
{
    public interface IRfidReader
    {
        public event EventHandler<RfidEventArgs> RfidEvent;
    }
}
