using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronometerTask.Domain.Cronometer
{
    public interface ICronometerTime
    {
        int Seconds { get; }
        int Minutes { get; }
        int Hours { get; }
        void AdvanceTime();
        void Reset();
    }
}
