using System;
using MobileChargingStation.Interfaces;

namespace MobileChargingStation.Simulator
{
public class DoorSim : IDoor
{
    public event EventHandler? DoorOpenedEvent;
    public event EventHandler? DoorClosedEvent;
    public bool IsLocked { get; private set; }  

    public void LockDoor() 
    {
        IsLocked = true;
    
    }

    public void UnlockDoor()
    {
        IsLocked = false;
    }

    public void DoorOpened() => DoorOpenedEvent?.Invoke(this, System.EventArgs.Empty);
    public void DoorClosed() => DoorClosedEvent?.Invoke(this, System.EventArgs.Empty);
}
}