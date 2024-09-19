namespace CronometerTask.Domain.Cronometers
{
    public interface ICronometerTimeMeasure
    {
        int Interval { get; }
        DateTime Time { get; }
        void AdvanceTime();
        void Reset();
    }
}
