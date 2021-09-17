using Aidan.Common.Core.Interfaces.Contract;
using Microsoft.Extensions.Logging;

namespace Aidan.Common.Utils.Utils
{
    public class LoggerAdapter<T> : ILoggerAdapter<T> where T : class
    {
        private readonly ILogger _logger;

        public LoggerAdapter( ILogger<T> logger ) { _logger = logger; }

        public void LogInfo( string message ) { _logger.LogInformation( message ); }

        public void LogError( string message ) { _logger.LogError( message ); }
    }
}