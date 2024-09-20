namespace CronometerTask.Domain.Cronometers
{
    public interface ITimeCounter
    {
        void AdvanceTime();
        void Reset();
        int Interval { get; }
        DateTime Time { get; }
    }
}
