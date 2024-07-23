﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CumulativeP1.Models;
using MySql.Data.MySqlClient;

namespace CumulativeP1.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        // This Controller will use a search function to access the teacher table of our school database.

        ///<summary>
        /// Returns a list of teachers' names in the system according to the search key
        /// </summary>
        /// <param name="SearchKey">search key for teacher's names.</param>
        /// <returns>
        /// GET api/TeacherData/ListTeachers/{SearchKey}
        /// One or a list of teachers' names.
        /// </returns>
        /// <example>
        /// SearchKey="AL"or "ale";case insensitive and key words in the name.
        /// return Alexander Bennett;
        /// </example>


        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey}")]
        public List<Teacher> ListTeachers(string SearchKey)
        {
            // Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            // Open the connection between the web server and database
            Connection.Open();

            // Establish a new command (query) for our database
            MySqlCommand Command = Connection.CreateCommand();

            //SQL QUERY
            Command.CommandText = "Select * from teachers where lower (teacherfname) like lower (@key) or lower (teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower (@key)";

            // Use @key to avoid error outcome of special symbol
            Command.Parameters.AddWithValue("@key", "%"+SearchKey+"%");
            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = Command.ExecuteReader();

            // Create an empty list of Teacher Names
            List<Teacher> Teachers = new List<Teacher>();

            // Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column innformation by the DB column name as an index
                string TeacherName = ResultSet["teacherfname"] + " " + ResultSet["teacherlname"];
                string EmployeeNumber = Convert.ToString(ResultSet["employeenumber"]);
                decimal TeacherSalary = Convert.ToDecimal(ResultSet["salary"]);
                DateTime TeacherHireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                int TeacherId = (int)ResultSet["teacherid"];


                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherName= TeacherName;
                NewTeacher.TeacherSalary= TeacherSalary;
                NewTeacher.TeacherHireDate= TeacherHireDate;
                NewTeacher.EmployeeNumber= EmployeeNumber;
                NewTeacher.TeacherId = TeacherId;

                // Add the Teacher elements to the List
                Teachers.Add(NewTeacher);
            }

            // Close the connection between the MySQL Database and the WebServer
            Connection.Close();

            // Return the final list of author names
            return Teachers;

        }

        /// <summary>
        /// This controller will access to a teacher's name, employee number, hire date, salary, classid and classname.
        /// </summary>
        /// <param name="TeacherId">the primary key in the database</param>
        /// <returns>one teacher's properties.</returns>
        /// <example>
        /// api/TeacherData/FindTeacher/7
        /// TeacherId=7
        /// Name:Shannon Barton;Employee ID:T397;Hire Date:2013-08-04 12:00:00 AM;Salary:64.70;Class ID:http5104;Course:Digital Design
        /// </example>


        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{TeacherId}")]
        public Teacher FindTeacher(int TeacherId)
        {
            Teacher NewTeacher = new Teacher();
            
            // Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            // Open the connection between the web server and database
            Connection.Open();

            // Establish a new command (query) for our database
            MySqlCommand Command = Connection.CreateCommand();

            //SQL QUERY
            Command.CommandText = "SELECT * FROM teachers LEFT JOIN classes ON classes.teacherid = teachers.teacherid WHERE teachers.teacherid = @id";

            // Use @key to avoid error outcome of special symbol
            Command.Parameters.AddWithValue("@id",TeacherId);

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = Command.ExecuteReader();

            while (ResultSet.Read())
            {
                // use Loop to get data from database teacher table
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string TeacherName = ResultSet["teacherfname"] + " " + ResultSet["teacherlname"];
                string EmployeeNumber = Convert.ToString(ResultSet["employeenumber"]);
                decimal TeacherSalary = Convert.ToDecimal(ResultSet["salary"]);
                DateTime TeacherHireDate = (DateTime)ResultSet["hiredate"];
                TeacherId = (int)ResultSet["teacherid"];

                // use Loop to get data from database class table, seems one-to-many relationship...
                // Class NewClass = new Class();
                String ClassName = ResultSet["classname"].ToString();
                String ClassCode = ResultSet["classcode"].ToString();


                //NewClass.ClassCode = ClassCode;
                //NewClass.ClassName = ClassName;
                //NewClasses.Add(NewClass);

      
                // Add to New Teacher object

                NewTeacher.TeacherName = TeacherName;
                NewTeacher.TeacherSalary = TeacherSalary;
                NewTeacher.TeacherHireDate = TeacherHireDate;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.TeacherId = TeacherId;
                // added on 7/22 in convenience to get new teacher
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;

                if (ClassCode == null || ClassCode=="") { NewTeacher.ClassCode = "Not Assigned"; }
                else { NewTeacher.ClassCode = ClassCode; }
                if (ClassName == null || ClassName == "") { NewTeacher.ClassName = "Not Assigned"; }
                else { NewTeacher.ClassName = ClassName; }

                // Unable to get multiple classes...

            }
            // Close the connection between the MySQL Database and the WebServer
            Connection.Close();

            return NewTeacher;
        }


        /// <summary>
        /// receive teacher information and add it to the database
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// POST: api/TeacherData/AddTeacher ->{Teacher Object}
        /// POST: CONTENT /REQUEST BODY:
        /// {
        /// Name:Shannon Barton;
        /// Employee ID:T397;
        /// Hire Date:2013-08-04 12:00:00 AM;
        /// Salary:64.70;
        /// Class ID:http5104;
        /// Course:Digital Design
        /// </example>
        [HttpPost]
        [Route("api/TeacherData/AddTeacher")]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            string query = "INSERT INTO teachers(teacherfname,teacherlname,employeenumber,hiredate,salary) VALUES (@teacherfname, @teacherlname,@employeenumber,@hiredate,@salary)";
            MySqlConnection Connection = School.AccessDatabase();
            Connection.Open();
            // creating a command
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@teacherfname", NewTeacher.TeacherFname);
            command.Parameters.AddWithValue("@teacherlname", NewTeacher.TeacherLname);
            command.Parameters.AddWithValue("@employeenumber", NewTeacher.EmployeeNumber);
            command.Parameters.AddWithValue("@hiredate", NewTeacher.TeacherHireDate);
            command.Parameters.AddWithValue("@salary", NewTeacher.TeacherSalary);
            command.Prepare();
            // execute the INSERT
            command.ExecuteNonQuery();
            // add the article into the database
            Connection.Close();
            // we can return something here
        }


        /// <summary>
        /// This method receives teacher information and deletes it from the database.
        /// </summary>
        /// <param name="TeacherId">
        /// the primary key of the teachers</param>
        /// <returns></returns>
        /// <example>
        /// POST api/TeacherData/DeleteTeacher
        /// </example>

        [HttpGet]
        [Route("api/TeacherData/DeleteTeacher/{TeacherId}")]
        public void DeleteTeacher(int TeacherId)
        {
            string query = "DELETE FROM teachers WHERE teacherid=@id";

            MySqlConnection Connection = School.AccessDatabase();
            Connection.Open();
            // creating a command
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", TeacherId);

            command.Prepare();
            // execute the INSERT
            command.ExecuteNonQuery();
            // add the article into the database
            Connection.Close();

            // return "I want to delete this teacher";
        }
    }
}
