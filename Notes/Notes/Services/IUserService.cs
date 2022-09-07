using System;
using Notes.Data.Models;

namespace Notes.Services
{
    public interface IUserService
    {
        void Save(User user);
        void Update(User user);
        void Delete(User user);
        User GetLoggedUser();
    }
}

