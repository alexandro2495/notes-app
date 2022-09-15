using System;
using SQLite;

namespace Notes.Data.Models
{
    [Table("Note")]
    public class Note
    {
        [PrimaryKey]
        [AutoIncrement]
        public long Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public long IdUsuario { get; set; }

        public NoteType Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Today;

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public Note()
        {
        }
    }

    public enum NoteType
    {
        None = 0,
        Picture = 1,
        Music = 2,
        Text = 3
    }
}

