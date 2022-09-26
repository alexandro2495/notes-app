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
            try
            {
                SQLiteConnectionSingleton.Connection().Insert(config);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            
        }

        public void Update(AppConfiguration note)
        {
            throw new NotImplementedException();
        }
    }
}

