using CronometerTask.Domain.Common;
using CronometerTask.Domain.Cronometers;

namespace CronometerTask.UI.Services
{
    public class CronometerService
    {
        private ICronometer _cronometer;

        public CronometerService(ICronometer cronometer)
        {
            _cronometer = cronometer;
        }

        public void Start()
        {
            _cronometer.Start();
        }

        public void Pause()
        {
            _cronometer.Pause();
        }

        public void Stop()
        {
            _cronometer.Stop();
        }

        public void SubscribeToUnitOfTimeElapsed(EventHandler<UnitOfTimeElapsedEventArgs>? eventHandler)
        {
            _cronometer.UnitOfTimeElapsed += eventHandler;
        }

        public bool IsRunning => _cronometer.IsRunning;
        public bool IsPaused => _cronometer.IsPaused;
    }
}
