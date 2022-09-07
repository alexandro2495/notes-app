using System;
using System.Collections.Generic;
using Notes.Data.Models;
using Notes.Services.implementations;

namespace Notes.Services.Implementations
{
    public class NoteSQLiteService : INoteService
    {
        private readonly SQLiteConnectionService _connection;
        public NoteSQLiteService(SQLiteConnectionService connection)
        {
            _connection = connection;
        }

        public void Create(Note note)
        {
            _connection.GetConnection().Insert(note);
        }

        public void Delete(Note note)
        {
            _connection.GetConnection().Delete(note);
        }

        public List<Note> GetNotes(long userId)
        {
            return _connection.GetConnection().Table<Note>().Where(note => note.IdUsuario == userId).ToList();
        }

        public void Update(Note note)
        {
            _connection.GetConnection().Update(note);
        }
    }
}

