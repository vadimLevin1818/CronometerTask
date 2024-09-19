using CronometerTask.Domain.Cronometers;

namespace CronometerTask.Tests
{
    public class CronometerUnitTest
    {
        [Fact]
        public void TestStart()
        {
            ICronometerTimeMeasure timer = new SecondsTimeMeasure();
            ICronometer cronometer = Cronometer.CreateCronometer(timer);

            cronometer.Start();

            Assert.True(cronometer.IsRunning);
            Assert.False(cronometer.IsPaused);
        }

        [Fact]
        public void TestPause()
        {
            ICronometerTimeMeasure timer = new SecondsTimeMeasure();
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
            ICronometerTimeMeasure timer = new SecondsTimeMeasure();
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
            ICronometerTimeMeasure timer = new SecondsTimeMeasure();
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