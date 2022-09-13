using System;
using System.Collections.Generic;
using System.IO;
using Notes.Data.Models;
using SQLite;

namespace Notes.Services.Implementations.SqliteImp
{
    internal class SQLiteConnectionSingleton
    {
        //singleton object
        private readonly static SQLiteConnectionSingleton singleton = new SQLiteConnectionSingleton();

        //database connection object
        private SQLiteConnection _dbConnectionInstance;

        //list to declare our database tables
        private List<Type> Tables = new List<Type>();

        private string DB_NAME = "noteapp.db3";
        private string AND_PATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private string IOS_PATH = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        private SQLiteConnectionSingleton()
        {
            if (_dbConnectionInstance == null)
            {
                CreateConnection();
                CreateTables();
            }
        }

        private void CreateTables()
        {
            //Tables
            Tables.Add(typeof(User));
            Tables.Add(typeof(AppConfiguration));
            Tables.Add(typeof(Note));

            //Creating tabless
            _dbConnectionInstance.CreateTables(CreateFlags.None, Tables.ToArray());

        }

        private void CreateConnection()
        {
#if IOS
            string path = Path.Combine(IOS_PATH, DB_NAME);
#else
            string path = Path.Combine(AND_PATH, DB_NAME);
#endif
            _dbConnectionInstance = new SQLiteConnection(
                path,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite
            );
        }

        public static SQLiteConnection Connection()
        {
            return singleton._dbConnectionInstance;
        }
    }
}