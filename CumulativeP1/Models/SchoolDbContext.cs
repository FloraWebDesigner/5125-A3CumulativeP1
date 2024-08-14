using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Install via Tools > NuGet package manager > manage nuget packages for solution
//"Browse" tab
//search for mysql.data and install to project
using MySql.Data.MySqlClient;

namespace CumulativeP1.Models
{
    public class SchoolDbContext
    {
        //Update these to match your own local school database!

        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        // ConnectionString is created to connect to the database
        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                 + "; user = " + User
                 + "; database = " + Database
                 + "; port = " + Port
                 + "; password = " + Password
                 + "; convert zero datetime = True";

            }
        }

        //This is the method we actually use to get the database!
        ///<summary>
        ///Returns a connection to the school database.
        ///</summary>
        ///<example>
        ///Private SchoolDbContext = new SchoolDbContext();
        ///MySqlConnection Conn=School.AccessDatabase();
        /// </example>
        /// <returns>
        /// A MySqlConnection Object
        /// </returns>
        public static MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
        