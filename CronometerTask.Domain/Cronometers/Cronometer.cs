using CronometerTask.Domain.Common;

namespace CronometerTask.Domain.Cronometers
{
    public class Cronometer : AggregateRoot, ICronometer
    {
        // timer to follow the inner state of cronometer
        private readonly System.Timers.Timer _timer;
        // using time counter interface so we can change to a different counter without modifying existing code
        private readonly ITimeCounter _cronometerTimeCounter;
        private EventHandler<UnitOfTimeElapsedEventArgs>? _unitOfTimeElapsedInternal;

        /// <summary>
        /// Private constructor, available only through static factory method.
        /// </summary>
        /// <param name="cronometerTimeCounter">Injected counter param that defines intervals and keeps time.</param>
        private Cronometer(ITimeCounter cronometerTimeCounter):base(Guid.NewGuid())
        {
            _cronometerTimeCounter = cronometerTimeCounter;
            _timer = new System.Timers.Timer()
            {
                Interval = _cronometerTimeCounter.Interval,
                
            };
            _timer.Elapsed += Timer_Elapsed;
        }

        public event EventHandler<UnitOfTimeElapsedEventArgs>? UnitOfTimeElapsed
        {
            add
            {
                _unitOfTimeElapsedInternal += value;
            }
            remove
            {
                _unitOfTimeElapsedInternal -= value;
            }
        }

        #region Event Handlers

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _cronometerTimeCounter.AdvanceTime();
            var args = new UnitOfTimeElapsedEventArgs() { ElapsedTime = _cronometerTimeCounter.Time };
            _unitOfTimeElapsedInternal?.Invoke(this, args);
        }

        #endregion

        #region Public Methods

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
            _cronometerTimeCounter?.Reset();
            IsRunning = false;
            IsPaused = false;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Factory method creating new cronometer with counter as an injected parameter.
        /// </summary>
        /// <param name="timeCounter">Injected parameter of time counter.</param>
        /// <returns>New ICronometer instance.</returns>
        public static ICronometer CreateCronometer(ITimeCounter timeCounter)
        {
            return new Cronometer(timeCounter);
        }

        #endregion

        #region Properties

        public bool IsRunning { get; private set; }
        public bool IsPaused { get; private set; }

        #endregion
    }
}
