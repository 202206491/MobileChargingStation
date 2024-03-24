using MobileChargingStation.Simulator;

using System.Runtime.CompilerServices;

namespace MobileChargingStation.Test
{
    [TestFixture]
    public class UnitTestDoor
    {
        private enum FakeEventArgs { Opened, Closed }

        private DoorSim _uut;
        private FakeEventArgs? _eventArgs;


        [SetUp]
        public void Setup()
        {
            _eventArgs = null;
            _uut = new DoorSim();

            _uut.DoorOpened();
            _uut.DoorOpenedEvent += (o, args) => { _eventArgs = FakeEventArgs.Opened; };

            _uut.DoorOpened();
            _uut.DoorClosedEvent += (o, args) => { _eventArgs = FakeEventArgs.Closed; };

        }

        [Test]
        public void DoorIsLocked()
        {
            _uut.LockDoor();

            Assert.That(_uut.IsLocked, Is.True);

        }

        [Test]
        public void DoorIsUnlocked()
        {
            _uut.UnlockDoor();

            Assert.That(_uut.IsLocked, Is.False);

        }

        [Test]
        public void DoorOpened_EventIsCorrectRunned()
        {
            _uut.DoorOpened();

            Assert.That(_eventArgs, Is.EqualTo(FakeEventArgs.Opened));

        }

        [Test]
        public void DoorOpened_EventRunned()
        {
            _uut.DoorOpened();

            Assert.That(_eventArgs, Is.Not.Null);

        }

        [Test]
        public void DoorClosed_EventIsCorrectRunned()
        {
            _uut.DoorClosed();

            Assert.That(_eventArgs, Is.EqualTo(FakeEventArgs.Closed));

        }

        [Test]
        public void DoorClosed_EventRunned()
        {
            _uut.DoorClosed();

            Assert.That(_eventArgs, Is.Not.Null);

        }


    }
}