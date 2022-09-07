using System;
using Notes.Data.Models;
using Notes.Services.implementations;

namespace Notes.Services.Implementations
{
    public class AppConfigSQLiteService : IAppConfigurationService
    {
        private readonly SQLiteConnectionService _connection;
        public AppConfigSQLiteService(SQLiteConnectionService connection)
        {
            _connection = connection;
        }

        public void Create(AppConfiguration config)
        {
            _connection.GetConnection().Insert(config);
        }

        public void Update(AppConfiguration note)
        {
            throw new NotImplementedException();
        }
    }
}

