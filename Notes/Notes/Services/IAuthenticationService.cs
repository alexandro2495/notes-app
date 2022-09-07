using System;
namespace Notes.Services
{
    public interface IAuthenticationService
    {
        bool SignIn(string user, string password);
        bool SignOut(string id);
    }
}

