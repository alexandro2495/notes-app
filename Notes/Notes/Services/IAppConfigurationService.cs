using System;
using Notes.Data.Models;

namespace Notes.Services
{
    public interface IAppConfigurationService
    {
        void Create(AppConfiguration config);
        void Update(AppConfiguration note);
    }
}

