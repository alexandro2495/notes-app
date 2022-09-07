using System;
using System.Collections.Generic;
using System.IO;
using Notes.Data.Models;
using SQLite;

namespace Notes.Services.implementations
{
    public class SQLiteConnectionService
    {
        //database connection object
        private SQLiteConnection DbConnection { get; set; }
        //list to declare our database tables
        private List<Type> Tables = new List<Type>();

        private string DB_NAME = "noteapp.db3";
        private string AND_PATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private string IOS_PATH = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public SQLiteConnectionService()
        {
            CreateConnection();
            CreateTables();
        }

        private void CreateTables()
        {
            //Tables
            Tables.Add(typeof(User));
            Tables.Add(typeof(AppConfiguration));
            Tables.Add(typeof(Note));

            //Creating tables
            DbConnection.CreateTables(CreateFlags.None, Tables.ToArray());

        }

        protected void CreateConnection()
        {
#if IOS
            string path = Path.Combine(IOS_PATH, DB_NAME);
#else
            string path = Path.Combine(AND_PATH, DB_NAME);
#endif
            DbConnection = new SQLiteConnection(
                path,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite
            );
        }

        public SQLiteConnection GetConnection()
        {
            return DbConnection;
        }
    }
}

