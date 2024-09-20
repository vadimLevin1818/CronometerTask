using CronometerTask.Domain.Cronometers;

namespace CronometerTask.Domain.Common
{
    public class UnitOfTimeElapsedEventArgs : EventArgs
    {
        public DateTime ElapsedTime { get; init; }
    }
}
