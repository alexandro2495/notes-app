using System;
using System.Collections.Generic;
using Notes.Data.Models;

namespace Notes.Services
{
    public interface INoteService
    {
        void Create(Note note);
        void Update(Note note);
        void Delete(Note note);
        List<Note> GetNotes(long userId);
    }
}

