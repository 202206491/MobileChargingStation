using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MobileChargingStation.Implementation;
using NSubstitute;
using MobileChargingStation.Interfaces;
using MobileChargingStation.EventArguments;

namespace MobileChargingStation.Test
{
    [TestFixture]
    public class UnitTestStationControl
    {
        private StationControl _unitStationControl;
        private IDoor _unitDoor;
        private IDisplay _unitDisplay;
        private IRfidReader _unitRfidReader;
        private ILog _unitLog;
        private IChargeControl _unitChargeControl;
      
        [SetUp]
        public void Setup()
        {
            _unitDoor = Substitute.For<IDoor>();
            _unitDisplay = Substitute.For<IDisplay>();
            _unitRfidReader = Substitute.For<IRfidReader>();
            _unitLog = Substitute.For<ILog>();
            _unitChargeControl =Substitute.For<IChargeControl>();
        
            _unitStationControl=new StationControl(_unitDoor, _unitDisplay, _unitRfidReader,
                _unitLog, _unitChargeControl);
        }

        [Test]
        public void OnDoorOpened_DisplayInstruction()
        {
            //Act
            _unitDoor.DoorOpenedEvent += Raise.Event();

            //Assert
            _unitDisplay.Received(1).DisplayInstruction("Connect your phone.");
        }

        [Test]
        public void OnDoorClosed_DisplayInstruction()
        {
            //Act
            _unitDoor.DoorClosedEvent += Raise.Event();

            //Assert
            _unitDisplay.Received(1).DisplayInstruction("Scan your RFID tag.");
        }

        [Test]
        public void AlreadyConnected_Announcement()
        {
            //Arrange
            _unitChargeControl.IsConnected().Returns(true);

            //Act
            _unitRfidReader.RfidEvent += Raise.EventWith(new RfidEventArgs { Id = 1});

            //Assert
            _unitDisplay.Received(1).DisplayAnnouncement("The locker is locked and your phone is charging. Use your RFID tag to unlock.");
            
        }

        [Test]
        public void NotConnected_Instruction()
        {
            //Arrange
            _unitChargeControl.IsConnected().Returns(false);

            //Act
            _unitRfidReader.RfidEvent += Raise.EventWith(new RfidEventArgs { Id = 1 });

            //Assert
            _unitDisplay.Received(1).DisplayInstruction("Your phone is not connected properly. Try again.");
        }

        [Test]
        public void CorrectID_Instruction()
        {
            //Arrange
            _unitChargeControl.IsConnected().Returns(true);

            //Act
            _unitRfidReader.RfidEvent += Raise.EventWith(new RfidEventArgs { Id = 1 });
            _unitRfidReader.RfidEvent += Raise.EventWith(new RfidEventArgs { Id = 1 });


            //Assert
            _unitDisplay.Received(1).DisplayInstruction("Take your phone out of the Mobile Charging Station and close the door");
        }

        [Test]
        public void WrongID_Instruction()
        {
            //Arrange
            _unitChargeControl.IsConnected().Returns(true);

            //Act
            _unitRfidReader.RfidEvent += Raise.EventWith(new RfidEventArgs { Id = 1 });
            _unitRfidReader.RfidEvent += Raise.EventWith(new RfidEventArgs { Id = 2 });

            //Assert
            _unitDisplay.Received(1).DisplayInstruction("Wrong RFID tag");
        }

        [Test]
        public void DoorOpenHandler_Announcement()
        {
            //Arrange
            _unitDoor.DoorOpenedEvent += Raise.Event();

            //Act
            _unitRfidReader.RfidEvent += Raise.EventWith(new RfidEventArgs { Id = 1 });

            //Assert
            _unitDisplay.Received(1).DisplayInstruction("Connect your phone and close the door.");
        }
    }
}
