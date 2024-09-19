using CronometerTask.Domain.Cronometers;

namespace CronometerTask.Domain.Common
{
    public class TimerElapsedEventArgs : EventArgs
    {
        public ICronometerTimeMeasure? CronometerTime { get; set; }
    }
}
