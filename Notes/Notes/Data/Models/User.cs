using System;
using SQLite;

namespace Notes.Data.Models
{
    [Table("User")]
    public class User
    {
        [PrimaryKey]
        [AutoIncrement]
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public long IdAppConfiguration { get; set; }

        public bool IsLoggedIn { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Today;

        public DateTime ModifiedAt { get; set; } = DateTime.Today;

        public User()
        {
        }
    }
}

