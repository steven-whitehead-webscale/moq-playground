using FluentAssertions;
using Moq;
using Xunit;

namespace moq_playground
{

    public class Lander
    {
        public virtual string MotorStatus
        {
            get
            {
                // Code that checks the status of the lander's motor
                return "OK";
            }
        }
    }


    public class MarsLander : Lander
    {
        public virtual int BatteryChargePercentage { get; set; }
        public virtual bool PathBlocked { get; set; }

        public virtual bool MyMethod()
        {
            return true;
        }
    }


    public class MarsLanderMonitor
    {
        MarsLander _lander;

        public MarsLanderMonitor(MarsLander lander)
        {
            _lander = lander;
        }

        public bool LanderCanMove()
        {
            _lander.MyMethod();

            return _lander.BatteryChargePercentage > 10 & !_lander.PathBlocked
                                                        & _lander.MotorStatus == "OK";
        }
    }

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Mock<MarsLander> mockMarsLander = new Mock<MarsLander>();
            mockMarsLander.Setup(m => m.BatteryChargePercentage).Returns(25);
            mockMarsLander.Setup(m => m.PathBlocked).Returns(false);
            mockMarsLander.Setup(m => m.MotorStatus).Returns("OK");
            mockMarsLander.Setup(m => m.MyMethod()).Returns(false);

            MarsLanderMonitor monitor = new MarsLanderMonitor(mockMarsLander.Object);

            monitor.LanderCanMove().Should().BeTrue();
        }
    }
}