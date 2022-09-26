using System;
using System.Collections.Generic;

namespace Notes.Services
{
    public interface IAnalyticService
    {
        void CreatedUserEvent(Dictionary<string, string> data);

        void NoteTypeAdded(Dictionary<string, string> data);

        void ViewMap(Dictionary<string, string> data);
    }
}
