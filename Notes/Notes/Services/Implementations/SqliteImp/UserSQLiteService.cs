using System;
using Notes.Data.Models;
using SQLite;

namespace Notes.Services.Implementations.SqliteImp
{
    public class UserSQLiteService : IUserService
    {
        public UserSQLiteService()
        {
        }

        public void Save(User user)
        {
            try
            {
                SQLiteConnectionSingleton.Connection().Insert(user);
            }
            catch(SQLiteException e)
            {
                if (e.Message == "UNIQUE constraint failed: User.UserName")
                {
                    throw new Exception("This user already exists, please use another");
                }
            }
            
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

