using System;
using Notes.Data.Models;

namespace Notes.Services.Implementations.SqliteImp
{
    public class UserSQLiteService : IUserService
    {
        public UserSQLiteService()
        {
        }

        public void Save(User user)
        {
            SQLiteConnectionSingleton.Connection().Insert(user);
        }

        public void Delete(User user)
        {
            SQLiteConnectionSingleton.Connection().Delete(user);
        }

        public User GetLoggedUser()
        {
            return SQLiteConnectionSingleton
                .Connection()
                .Table<User>()
                .FirstOrDefault(user => user.IsLoggedIn == true);
        }

        public void Update(User user)
        {
            SQLiteConnectionSingleton.Connection().Update(user);
        }
    }
}

