using System;
using System.Collections.Generic;
using Notes.Data.Models;

namespace Notes.Services.Implementations.SqliteImp
{
    public class NoteSQLiteService : INoteService
    {
        public NoteSQLiteService()
        {
        }

        public void Create(Note note)
        {
            SQLiteConnectionSingleton.Connection().Insert(note);
        }

        public void Delete(Note note)
        {
            SQLiteConnectionSingleton.Connection().Delete(note);
        }

        public List<Note> GetNotes(long userId)
        {
            return SQLiteConnectionSingleton
                    .Connection()
                    .Table<Note>()
                    .Where(note => note.IdUsuario == userId)
                    .ToList();
        }

        public void Update(Note note)
        {
            SQLiteConnectionSingleton.Connection().Update(note);
        }
    }
}

