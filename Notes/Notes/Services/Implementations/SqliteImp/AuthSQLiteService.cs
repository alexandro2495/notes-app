using System;
using Notes.Data.Models;

namespace Notes.Services.Implementations.SqliteImp
{
    public class AuthSQLiteService : IAuthenticationService
    {
        public AuthSQLiteService() { }

        public bool SignIn(string user, string password)
        {
            var result = SQLiteConnectionSingleton.Connection()
                .Table<User>()
                .FirstOrDefault(
                    _user => _user.UserName == user && _user.Password == password
                );

            if (result != default && result != null)
            {
                SQLiteConnectionSingleton
                    .Connection()
                    .Execute("UPDATE User SET IsLoggedIn = ? WHERE Id=? ", true, result.Id);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SignOut(string id)
        {
            SQLiteConnectionSingleton.Connection()
                .Execute("UPDATE User SET IsLoggedIn = ? WHERE Id=? ", false, id);
            return true;
        }
    }
}

