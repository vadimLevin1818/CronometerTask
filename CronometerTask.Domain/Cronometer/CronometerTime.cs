namespace CronometerTask.Domain.Cronometer
{
    public class CronometerTime : ICronometerTime
    {
        private const int MaxSeconds = 8;
        private const int MaxMinutes = 5;

        public int Seconds { get; private set; }
        public int Minutes { get; private set; }
        public int Hours { get; private set; }

        public void AdvanceTime()
        {
            if (++Seconds < MaxSeconds) return;

            Seconds = 0;

            if (++Minutes < MaxMinutes) return;
            
            Minutes = 0;
            Hours++; 
        }

        public void Reset()
        {
            Seconds = 0;
            Minutes = 0;
            Hours = 0;
        }
    }
}
