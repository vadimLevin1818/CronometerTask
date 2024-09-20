using CronometerTask.Domain.Common;
using CronometerTask.Domain.Cronometers;

namespace CronometerTask.UI.Services
{
    /// <summary>
    /// Dependency injection service for Cronometer with construction injection.
    /// </summary>
    /// <param name="cronometer">Injected ICronometer object</param>
    public class CronometerService(ICronometer cronometer)
    {
        private readonly ICronometer _cronometer = cronometer;

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
