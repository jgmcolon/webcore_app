using Microsoft.Extensions.Configuration;
using webcore_app.Core.Interfaces;

namespace AppService
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private IConfiguration _configuration { get; }

        private string _connectionSource;

        public ConnectionStringProvider(IConfiguration Configuration, string ConnectionSource)
        {
            _configuration = Configuration;
            _connectionSource = ConnectionSource;
        }

        public string ConnectionString => _configuration.GetConnectionString(_connectionSource);
    }


}
