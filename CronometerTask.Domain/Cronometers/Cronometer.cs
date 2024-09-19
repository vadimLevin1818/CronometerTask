using CronometerTask.Domain.Common;

namespace CronometerTask.Domain.Cronometers
{
    public class Cronometer : AggregateRoot, ICronometer
    {
        private readonly System.Timers.Timer _timer;
        private readonly ICronometerTimeMeasure _cronometerTimeMeasure;
        private EventHandler<UnitOfTimeElapsedEventArgs>? UnitOfTimeElapsedInternal;

        private Cronometer(ICronometerTimeMeasure cronometerTimeMeasure):base(Guid.NewGuid())
        {
            _cronometerTimeMeasure = cronometerTimeMeasure;
            _timer = new System.Timers.Timer();
            _timer.Interval = _cronometerTimeMeasure.Interval;
            _timer.Elapsed += _timer_Elapsed;
        }

        public event EventHandler<UnitOfTimeElapsedEventArgs>? UnitOfTimeElapsed
        {
            add
            {
                UnitOfTimeElapsedInternal += value;
            }
            remove
            {
                UnitOfTimeElapsedInternal -= value;
            }
        }

        private void _timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _cronometerTimeMeasure.AdvanceTime();
            var args = new UnitOfTimeElapsedEventArgs() { CronometerTimeMeasure = _cronometerTimeMeasure };
            UnitOfTimeElapsedInternal?.Invoke(this, args);
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
            _cronometerTimeMeasure?.Reset();
            IsRunning = false;
            IsPaused = false;

            var args = new UnitOfTimeElapsedEventArgs() { CronometerTimeMeasure = _cronometerTimeMeasure };
            UnitOfTimeElapsedInternal?.Invoke(this, args);
        }

        public bool IsRunning { get; private set; }
        public bool IsPaused { get; private set; }
        

        public static Cronometer CreateCronometer(ICronometerTimeMeasure cronometerTime)
        {
            return new Cronometer(cronometerTime);
        }
    }
}
