
using System.Threading;
using NUnit.Framework;
using PolyhydraGames.Extensions;

namespace Extensions.Test1
{
    [TestFixture]
    public class TimerTests
    {
        [Test]
        public void Timer_ElapsedTime_ReturnsDuration()
        {
            var timer = new PolyhydraGames.Extensions.Timer();
            Thread.Sleep(50);
            timer.Stop();
            Assert.That(timer.ElapsedMilliseconds, Is.GreaterThanOrEqualTo(50));
        }

        [Test]
        public void Timer_Pause_Resume_WorksCorrectly()
        {
            var timer = new PolyhydraGames.Extensions.Timer();
            Thread.Sleep(20);
            timer.Pause();
            Thread.Sleep(50);
            timer.Resume();
            Thread.Sleep(20);
            timer.Stop();
            Assert.That(timer.ElapsedMilliseconds, Is.GreaterThanOrEqualTo(40).And.LessThan(120));

        }
    }
}
