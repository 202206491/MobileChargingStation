using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileChargingStation.Interfaces;
using MobileChargingStation.EventArguments;
using MobileChargingStation.Implementation;
using NSubstitute;

namespace MobileChargingStation.Test
{
    [TestFixture]
    public class UnitTestChargeControl
    {
        private ChargeControl _uut;
        private IDisplay _display;
        private IUsbCharger _usbCharger;


        [SetUp]
        public void Setup()
        {
            _display = Substitute.For<IDisplay>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _uut = new ChargeControl(_usbCharger, _display);

        }
        [Test]
        public void StateNotCharging_EventDoesNothing()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentValueEventArgs { Current = 0 });

            _display.Received(0).DisplayAnnouncement("No connection.");
        }


        [Test]
        public void UsbStartCharge() 
        {
            _uut.StartCharge();

            _usbCharger.Received(1).StartCharge();

        }

        [Test]
        public void UsbStopCharge() 
        {
            _uut.StopCharge();

            _usbCharger.Received(1).StopCharge();
        }

        [Test]
        public void IsConected_TrueReturned () 
        { 
            _usbCharger.Connected.Returns(true);

            Assert.That(true, Is.EqualTo(_uut.IsConnected()));
        }

        [Test]
        public void IsConnected_FalseReturned () 
        {
            _usbCharger.Connected.Returns(false);

            Assert.That(false, Is.EqualTo(_uut.IsConnected()));
        }

        [TestCase(1.1)]
        public void HandleCurrentValueEvent_NotHappens(double current)
        {
            _uut.StartCharge();

            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentValueEventArgs { Current = current });

            _display.Received(0).DisplayAnnouncement("No connection.");
        }

        [TestCase(0)]
        public void HandleCurrentValueEvent_NoCurrent_Display(double current)
        {
            _uut.StartCharge();
            
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentValueEventArgs { Current = current });

            _display.Received(1).DisplayAnnouncement("No connection.");
        }

        [TestCase(0)]
        public void HandleCurrentValueEvent_DisplayOnMessageChangeOnly(double current)
        {
            _uut.StartCharge();

            for (int i = 0; i < 10; i++)
            {
                _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentValueEventArgs { Current = current });
            }

            _display.Received(1).DisplayAnnouncement("No connection.");
        }

        [TestCase(0.1)]
        [TestCase(5)]
        public void HandleCurrentValueEvent_FullyCharged(double current)
        {
            _uut.StartCharge();

            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentValueEventArgs { Current = current });

            _display.Received(1).DisplayAnnouncement("Fully Charged");
        }

        [TestCase(1.1)]
        [TestCase(5)]
        public void HandleCurrentValueEvent_UsbStopCharge(double current)
        {
            _uut.StartCharge();

            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentValueEventArgs { Current = current });
            
            _usbCharger.Received(1).StopCharge();
        }

        [TestCase(3)]
        public void HandleCurrentValueEvent__NotChargingState(double current)
        {
            _uut.StartCharge();

            for (int i = 0; i < 10; i++)
            {
                _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentValueEventArgs { Current = current });
            }

            _display.Received(1).DisplayAnnouncement("Fully Charged");
        }


        [TestCase(5.1)]
        [TestCase(500)]
        public void HandleCurrentValueEvent_DisplayCharging(double current)
        {
            _uut.StartCharge();

            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentValueEventArgs { Current = current });
            
            _display.Received(1).DisplayAnnouncement("Charging");
        }

        [TestCase(355)]
        public void HandleCurrentValueEvent_StillCharging(double current)
        {
            _uut.StartCharge();

            for (int i = 0; i < 10; i++)
            {
                _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentValueEventArgs { Current = current });
            }

            _display.Received(1).DisplayAnnouncement("Charging");
        }

        [TestCase(500.1)]
        [TestCase(1000)]
        public void HandleCurrentValueEvent_DisplayOverload(double current)
        {
            _uut.StartCharge();
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentValueEventArgs { Current = current });
            _display.Received(0).DisplayAnnouncement("Current overload.");
        }

        [TestCase(500.1)]
        [TestCase(1000)]
        public void HandleCurrentValueEvent_BiggerValues_UsbStopCharge(double current)
        {
            _uut.StartCharge();
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentValueEventArgs { Current = current });
            _usbCharger.Received(1).StopCharge();
        }

       

    }


}
