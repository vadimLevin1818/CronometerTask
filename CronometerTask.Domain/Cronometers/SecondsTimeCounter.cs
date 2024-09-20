namespace CronometerTask.Domain.Cronometers
{
    public class SecondsTimeCounter : ITimeCounter
    {
        private const int SecondsInterval = 1000;

        private DateTime _currentTime;

        public int Interval => SecondsInterval;
        public DateTime Time => _currentTime;

        public void AdvanceTime()
        {
            _currentTime = _currentTime.AddSeconds(1);
        }

        public void Reset()
        {
            _currentTime = new DateTime();
        }
    }
}
