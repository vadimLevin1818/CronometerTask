namespace CronometerTask.Domain.Cronometer
{
    public interface ICronometerTimeMeasure
    {
        int Interval { get; }
        DateTime Time { get; }
        void AdvanceTime();
        void Reset();
    }
}
