using CronometerTask.Domain.Logger;

namespace CronometerTask.Infrastructure.JsonLogger
{
    public class JsonLoggerFactory : ILoggerFactory
    {
        public ILogger CreateLogger()
        {
            return new JsonLogger();
        }
    }
}
