using CronometerTask.Domain.Cronometers;

namespace CronometerTask.Tests
{
    public class CronometerUnitTest
    {
        [Fact]
        public void TestStart()
        {
            ITimeCounter timer = new SecondsTimeCounter();
            ICronometer cronometer = Cronometer.CreateCronometer(timer);

            cronometer.Start();

            Assert.True(cronometer.IsRunning);
            Assert.False(cronometer.IsPaused);
        }

        [Fact]
        public void TestPause()
        {
            ITimeCounter timer = new SecondsTimeCounter();
            ICronometer cronometer = Cronometer.CreateCronometer(timer);

            cronometer.Start();

            Assert.True(cronometer.IsRunning);
            Assert.False(cronometer.IsPaused);
            cronometer.Pause();
            Assert.False(cronometer.IsRunning);
            Assert.True(cronometer.IsPaused);
        }

        [Fact]
        public void TestStop()
        {
            ITimeCounter timer = new SecondsTimeCounter();
            ICronometer cronometer = Cronometer.CreateCronometer(timer);

            cronometer.Start();
            Assert.True(cronometer.IsRunning);

            cronometer.Pause();
            Assert.False(cronometer.IsRunning);
            Assert.True(cronometer.IsPaused);

            cronometer.Stop();
            Assert.False(cronometer.IsRunning);
            Assert.False(cronometer.IsPaused);
        }

        [Fact]
        public void TestStartPauseStart()
        {
            ITimeCounter timer = new SecondsTimeCounter();
            ICronometer cronometer = Cronometer.CreateCronometer(timer);

            cronometer.Start();
            Assert.True(cronometer.IsRunning);
            Assert.False(cronometer.IsPaused);

            cronometer.Pause();
            Assert.False(cronometer.IsRunning);
            Assert.True(cronometer.IsPaused);

            cronometer.Start();
            Assert.True(cronometer.IsRunning);
            Assert.False(cronometer.IsPaused);
        }
    }
}