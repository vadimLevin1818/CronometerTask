using CronometerTask.Domain.Common;

namespace CronometerTask.Domain.Cronometer
{
    public interface ICronometer
    {
        void Start();
        void Pause();
        void Stop();
        bool IsRunning {  get; }
        bool IsPaused { get; }
        event EventHandler<TimerElapsedEventArgs> UnitOfTimeElapsed;
    }
}
