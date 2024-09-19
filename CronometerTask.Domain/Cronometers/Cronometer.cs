using CronometerTask.Domain.Common;

namespace CronometerTask.Domain.Cronometers
{
    public class Cronometer : AggregateRoot, ICronometer
    {
        private readonly System.Timers.Timer _timer;
        private readonly ICronometerTimeMeasure? _cronometerTime;

        private Cronometer(ICronometerTimeMeasure cronometerTime):base(Guid.NewGuid())
        {
            _cronometerTime = cronometerTime;
            _timer = new System.Timers.Timer();
            _timer.Interval = _cronometerTime.Interval;
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _cronometerTime?.AdvanceTime();
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
            _cronometerTime?.Reset();
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

        public event EventHandler<TimerElapsedEventArgs>? UnitOfTimeElapsed;
        public int Seconds { get; private set; }
        public int Minutes { get; private set; }
        public int Hours { get; private set; }
        public ICronometerTimeMeasure CronometerTime { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsPaused { get; private set; }
        

        public static Cronometer CreateCronometer(ICronometerTimeMeasure cronometerTime)
        {
            return new Cronometer(cronometerTime);
        }
    }
}
