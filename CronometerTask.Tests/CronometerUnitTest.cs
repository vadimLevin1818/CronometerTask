using CronometerTask.Domain.Cronometer;

namespace CronometerTask.Tests
{
    public class CronometerUnitTest
    {
        [Fact]
        public void TestStart()
        {
            ICronometer cronometer = Cronometer.CreateCronometer();

            cronometer.Start();

            Assert.True(cronometer.IsRunning);
            Assert.False(cronometer.IsPaused);
        }

        [Fact]
        public void TestPause()
        {
            ICronometer cronometer = Cronometer.CreateCronometer();

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
            ICronometer cronometer = Cronometer.CreateCronometer();

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
            ICronometer cronometer = Cronometer.CreateCronometer();

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