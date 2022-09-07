using System;
using Notes.Data.Models;
using Notes.Services.implementations;

namespace Notes.Services.Implementations
{
    public class UserSQLiteService : IUserService, IAuthenticationService
    {
        private readonly SQLiteConnectionService _connection;

        public UserSQLiteService(SQLiteConnectionService connection)
        {
            _connection = connection;
        }

        public void Save(User user)
        {
            _connection.GetConnection().Insert(user);
        }

        public void Delete(User user)
        {
            _connection.GetConnection().Delete(user);
        }

        public User GetLoggedUser()
        {
            return _connection.GetConnection().Table<User>().FirstOrDefault(user => user.IsLoggedIn == true);
        }

        public void Update(User user)
        {
            _connection.GetConnection().Update(user);
        }

        public bool SignIn(string user, string password)
        {
            var result = _connection.GetConnection()
                .Table<User>()
                .FirstOrDefault(
                    _user => _user.UserName == user && _user.Password == password
                );

            if (result != default && result != null)
            {
                _connection.GetConnection().Execute("UPDATE User SET IsLoggedIn = ? WHERE Id=? ", true, result.Id);

                return true;
            }
            else
            {
                throw new Exception("Authentication Error Username/Password not equal");
            }
        }

        public bool SignOut(string id)
        {
            _connection.GetConnection().Execute("UPDATE User SET IsLoggedIn = ? WHERE Id=? ", false, id);
            return true;
        }
    }
}

