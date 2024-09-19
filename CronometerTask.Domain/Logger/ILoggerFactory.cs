using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronometerTask.Domain.Logger
{
    public interface ILoggerFactory
    {
        ILogger CreateLogger();
    }
}
