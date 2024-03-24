using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileChargingStation.EventArguments;
using MobileChargingStation.Simulator;

namespace MobileChargingStation.Test
{
    public class UnitTestRfidReader
    {
        private RfidReaderSim _uut;
        private RfidEventArgs _RfidEventArgs;

        [SetUp]
        public void Setup() 
        {
        _uut = new RfidReaderSim();
        _RfidEventArgs = null;
        _uut.RfidEvent += (o, args) => { _RfidEventArgs = args; };
        }

        [TestCase(1)]
        public void IdIsScanned_EventIsRunned(int id)
        {
            _uut.Scan(id);

            Assert.That(_RfidEventArgs, Is.Not.Null);
        }

        [TestCase(1)]
        public void IdIsScanned_IdIsRecievedCorrect(int id)
        {
            _uut.Scan(id);

            Assert.That(_RfidEventArgs.Id, Is.EqualTo(id));
        }

        [TestCase(1)]
        public void ErrorId_DoesThrowException(int id)
        {
            _uut = new RfidReaderSim();
            Assert.DoesNotThrow(() => _uut.Scan(id));
        }        


    }
}
