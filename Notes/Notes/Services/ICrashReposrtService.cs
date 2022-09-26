using System;
using System.Collections.Generic;

namespace Notes.Services
{
    public interface ICrashReposrtService
    {
        void TrackError(Exception exception);

        void TrackError(Exception exception, Dictionary<string, string> data);
    }
}
