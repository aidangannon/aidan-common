using Aidan.Common.Core.Interfaces.Contract;
using Microsoft.Extensions.Configuration;

namespace Aidan.Common.Utils
{
    public class ConfigurationAdapter : IConfigurationAdapter
    {
        private readonly IConfiguration _configuration;

        public ConfigurationAdapter( IConfiguration configuration ) { _configuration = configuration; }

        public T Get<T>( ) => _configuration.Get<T>( );
    }
}