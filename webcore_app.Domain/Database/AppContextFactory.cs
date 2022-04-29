using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using webcore_app.Core.Interfaces;

namespace webcore_app.Core.Database
{
    public class AppContextFactory
    {
        public IConnectionStringProvider ConnectionStringProvider { get; set; }
        public AppContextFactory(IConnectionStringProvider connectionStringProvider)
        {
            ConnectionStringProvider = connectionStringProvider;
        }
        public AppContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppContext>();
            optionsBuilder.UseSqlServer(ConnectionStringProvider.ConnectionString);

            return new AppContext(optionsBuilder.Options);
        }
    }
}
