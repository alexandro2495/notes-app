using System;
using System.Collections.Generic;
using Microsoft.AppCenter.Analytics;

namespace Notes.Services.Implementations
{
    public class AppCenterAnalyticService : IAnalyticService
    {
        public AppCenterAnalyticService()
        {
        }

        public void CreatedUserEvent(Dictionary<string, string> data)
        {
            Analytics.TrackEvent("CreatedUser", data);
        }

        public void NoteTypeAdded(Dictionary<string, string> data)
        {
            Analytics.TrackEvent("NoteTypeAdded", data);
        }

        public void ViewMap(Dictionary<string, string> data)
        {
            Analytics.TrackEvent("ViewMap", data);
        }
    }
}
