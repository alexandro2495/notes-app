using System;
using Notes.Data.Models;

namespace Notes.Services.Implementations.SqliteImp
{
    public class AppConfigSQLiteService : IAppConfigurationService
    {

        public AppConfigSQLiteService()
        {
        }

        public void Create(AppConfiguration config)
        {
            SQLiteConnectionSingleton.Connection().Insert(config);
        }

        public void Update(AppConfiguration note)
        {
            throw new NotImplementedException();
        }
    }
}

