using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.CodeDom;
using System.IO;
using Dapper;

namespace Kinect_Photos
{
    internal class DatabaseHelper
    {
        private const String connectionString = "Data Source=kinectPhotos.db;Version=3;";

        public static IDbConnection GetDbConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public static void initializeDatabase()
        {
            using (IDbConnection dbConnection = GetDbConnection()) //surround with using block so the connection closes when we're done with it
            {
                dbConnection.Open(); //these lines will always start it
                string sql = File.ReadAllText("scripts/createTables.sql");
                dbConnection.Execute(sql);
            }

        }


    }


}
