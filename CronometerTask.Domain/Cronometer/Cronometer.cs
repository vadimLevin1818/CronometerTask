
using CronometerTask.Domain.Common;

namespace CronometerTask.Domain.Cronometer
{
    public class Cronometer : ICronometer
    {
        private readonly System.Timers.Timer _timer;
        private readonly ICronometerTime _cronometerTime;

        private Cronometer(ICronometerTime cronometerTime)
        {
            _cronometerTime = new CronometerTime();
            _timer = new System.Timers.Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _cronometerTime.AdvanceTime();
            var args = new TimerElapsedEventArgs() { CronometerTime = _cronometerTime };
            UnitOfTimeElapsed?.Invoke(this, args);
        }

        public void Start()
        {
            _timer.Start();
            IsRunning = true;
            IsPaused = false;
        }

        public void Pause()
        {
            _timer.Stop();
            IsRunning = false;
            IsPaused = true;
        }

        public void Stop()
        {
            _cronometerTime.Reset();
            IsRunning = false;
            IsPaused = false;

            var args = new TimerElapsedEventArgs() { CronometerTime = _cronometerTime };
            UnitOfTimeElapsed?.Invoke(this, args);
        }

        private void TimerCallback(Object state)
        {

        }

        private void RaiseSecondElapsed()
        {

        }

        public event EventHandler<TimerElapsedEventArgs> UnitOfTimeElapsed;
        public int Seconds { get; private set; }
        public int Minutes { get; private set; }
        public int Hours { get; private set; }
        public ICronometerTime CronometerTime { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsPaused { get; private set; }
        

        public static Cronometer CreateCronometer(ICronometerTime? cronometerTime = null)
        {
            return new Cronometer(cronometerTime);
        }
    }
}
