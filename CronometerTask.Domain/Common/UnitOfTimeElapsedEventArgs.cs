using CronometerTask.Domain.Cronometers;

namespace CronometerTask.Domain.Common
{
    public class UnitOfTimeElapsedEventArgs : EventArgs
    {
        public ICronometerTimeMeasure? CronometerTimeMeasure { get; set; }
    }
}
