using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileChargingStation.Implementation;
using MobileChargingStation.Interfaces;

namespace MobileChargingStation.Test
{
    [TestFixture]
    public class UnitTestLog
    {
        private Log _uut;
        private string _pathThatWorks;
        private const string _pathThatDoesNotWork="PathThatDoesNotWork";

        [SetUp]
        public void Setup()
        {
            _uut = new Log();
            _pathThatWorks = Path.GetTempPath();
        }

        [Test]
        public void WriteToLog_PathThatWorks_LogIsThere()
        {
            //Arrange
            _uut.SetPath(_pathThatWorks);

            //Act
            _uut.WriteToLog("Testing Message");

            //Assert
            Assert.That(File.Exists(Path.Combine(_pathThatWorks, "logfile.txt")),Is.EqualTo(true));
           
        }

        [Test]
        public void WriteToLog_PathThatDoesNotWork_ThrowsIOException()
        {
            //Arrange
            _uut.SetPath(_pathThatDoesNotWork);

            //Act & Assert
            Assert.That(() => _uut.WriteToLog("Test message"), Throws.InstanceOf<IOException>());
        }


    }
}
