using System;
using System.Timers;

namespace CronometerTask.Domain.Cronometer
{
    public class SecondsTimeMeasure : ICronometerTimeMeasure
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
