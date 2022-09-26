using System;
using System.Collections.Generic;
using Microsoft.AppCenter.Crashes;

namespace Notes.Services.Implementations
{
    public class AppCenterCrashReportService : ICrashReposrtService
    {
        public AppCenterCrashReportService()
        {
        }

        public void TrackError(Exception exception)
        {
            Crashes.TrackError(exception);
        }

        public void TrackError(Exception exception, Dictionary<string, string> data)
        {
            Crashes.TrackError(exception, data);
        }
    }
}
