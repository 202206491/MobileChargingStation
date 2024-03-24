using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileChargingStation.Interfaces;
using MobileChargingStation.Simulator;

namespace MobileChargingStation.Test
{
    [TestFixture]
    public class UnitTestDisplay
    {
        private IDisplay _uut;
        private StringWriter _consoleBuffer;
        private const string _expectedMessage = "Test instruction message";

        [SetUp]
        public void Setup()
        {
            _uut = new DisplaySimulator();
            _consoleBuffer = new StringWriter();
            Console.SetOut(_consoleBuffer);
        }

        [Test]
        public void Instruction_Print_ShownContent()
        {
            _uut.DisplayInstruction(_expectedMessage);

            Assert.That(_consoleBuffer.ToString().Trim(), Is.Not.EqualTo(string.Empty));

        }

        [Test]
        public void Instruction_Print_CorrectContent()
        {
            _uut.DisplayInstruction(_expectedMessage);

            //Assert.IsTrue(_consoleBuffer.ToString().Trim().Contains($"{_expectedMessage}"));
            Assert.That(_consoleBuffer.ToString().Trim(), Is.EqualTo(_expectedMessage));
        }

        [Test]
        public void Announcement_Print_ShownContent()
        {
            _uut.DisplayAnnouncement(_expectedMessage);

            Assert.That(_consoleBuffer.ToString().Trim(), Is.Not.EqualTo(string.Empty));

        }

        [Test]
        public void Announcement_Print_CorrectContent()
        {
            _uut.DisplayAnnouncement(_expectedMessage);

            //Assert.IsTrue(_consoleBuffer.ToString().Trim().Contains($"{_expectedMessage}"));
            Assert.That(_consoleBuffer.ToString().Trim(), Is.EqualTo(_expectedMessage));
        }


    }
}
