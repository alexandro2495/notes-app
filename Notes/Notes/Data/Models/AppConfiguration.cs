using System;
using SQLite;

namespace Notes.Data.Models
{
    [Table("AppConfiguration")]
    public class AppConfiguration
    {
        [PrimaryKey]
        [AutoIncrement]
        public long Id { get; set; }

        public string PrimaryColor { get; set; }

        public string DarkColor { get; set; }

        public string AccentColor { get; set; }

        public string PageBackgroundColor { get; set; }

        public string FontFamily { get; set; }

        public string Language { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Today;

        public DateTime ModifiedAt { get; set; } = DateTime.Today;

        public AppConfiguration()
        {
        }
    }
}

