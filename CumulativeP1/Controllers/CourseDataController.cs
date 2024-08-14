using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CumulativeP1.Models;
using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;

namespace CumulativeP1.Controllers
{
    public class CourseDataController : ApiController
    {
        // The database context class which allows us to access to our MySQL Database.
        // private SchoolDbContext School = new SchoolDbContext();

        //Create an instance of a connection
        MySqlConnection Connection = SchoolDbContext.AccessDatabase();

        // This controller will access the class table of our school database.
        ///<summary>
        ///GET api/CourseData/ListCourse/{SearchKey}
        ///To access a list of courses and their properties, including Course Code,Course Name,Start Date,Finish Date and Student Enrolment link
        ///</summary>
        ///<param name="SearchKey">search course name</param>
        ///<example>
        ///SearchKey = Pro
        ///Show two courses: Project Management and Web Programming and their Course Codes,Start Dates,Finish Dates and Student Enrolment links
        /// </example>
        ///<returns>
        ///Return a list of courses in the system with Course Code,Course Name,Start Date,Finish Date and Student Enrolment link
        ///</returns>

        [HttpGet]
        [Route("api/CourseData/ListCourse/{SearchKey?}")]
        public List<Course> ListCourse(string SearchKey = null)
        {
            

            //Open the connection between the web server and database
            Connection.Open();

            // Establish a new command (query) for out database
            MySqlCommand Command = Connection.CreateCommand();

            //SQL QUERY
            Command.CommandText = "SELECT * FROM classes WHERE classname like lower(@key)";
            Command.Parameters.AddWithValue("@key", "%"+SearchKey+"%");
            Command.Prepare();


            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = Command.ExecuteReader();

            // Create an empty list of Course Names

            List<Course> Courses = new List<Course>();

            // Loop Through Each Row the ResultSet

            while (ResultSet.Read())
            {
                // Access Column information by the DB column name as an index
                string CourseName = ResultSet["classname"].ToString();
                string CourseCode = ResultSet["classcode"].ToString();
                int CourseId = Convert.ToInt32(ResultSet["classid"]);
                String StartDate = ((DateTime)ResultSet["startdate"]).ToShortDateString();
                String FinishDate = ((DateTime)ResultSet["finishdate"]).ToShortDateString();


            Course NewCourse = new Course();
                NewCourse.CourseCode = CourseCode;
                NewCourse.CourseId = CourseId;
                NewCourse.StartDate = StartDate;
                NewCourse.FinishDate = FinishDate;
                NewCourse.CourseName= CourseName;

                // Add course name to the list
                Courses.Add(NewCourse);
            }
            //Close the connection between MySQL Database and the webserver
            Connection.Close();

            // return the final list of course names
            return Courses;

            }


        ///<summary>
        ///POST api/CourseData/FindStudent/{courseid}
        ///To access a list of students and their properties, including StudentNumber,StudentName,Enroldate.
        ///</summary>
        ///<param name="CourseId">use courseid to find students information</param>
        ///<example>
        ///CourseId = 2
        ///curl -d "" "http://localhost:61978/api/StudentData/FindStudent/2"
        ///Show 27 students information(StudentNumber,StudentName,Enroldate)
        ///</example>
        ///<returns>
        ///Return a list of students who enrolled in this course.
        ///</returns>
        [HttpPost]
        // I'd like to change to post, seems the url refers to the one I defined in the view, still show up the courseid on the browser... 
        [Route("api/CourseData/FindStudent/{CourseId?}")]
        public List<Student> FindStudent(int CourseId)
        {
            
            //Create an instance of a connection
            //MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            // Establish a new command (query) for out database
            MySqlCommand Command = Connection.CreateCommand();

            //SQL QUERY
            Command.CommandText = "SELECT * FROM studentsxclasses LEFT JOIN classes ON classes.classid = studentsxclasses.classid LEFT JOIN students ON students.studentid = studentsxclasses.studentid WHERE classes.classid = @id";
            Command.Parameters.AddWithValue("@id",CourseId);
            Command.Prepare();


            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = Command.ExecuteReader();

            // Create an empty list of student properties

            List<Student> Students = new List<Student>();

            // Loop Through Each Row the ResultSet

            while (ResultSet.Read())
            {
                // Access Column information by the DB column name as an index
                string StudentName = ResultSet["studentfname"] + " " + ResultSet["studentlname"];
                string StudentNumber = ResultSet["studentnumber"].ToString();
                string Enroldate = ((DateTime)ResultSet["enroldate"]).ToShortDateString();
                CourseId = Convert.ToInt32(ResultSet["classid"]);


                Student NewStudent = new Student();
                NewStudent.StudentName = StudentName;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.Enroldate = Enroldate;
                NewStudent.CourseId = CourseId;

                // Add student information to the list
                Students.Add(NewStudent);
            }
            //Close the connection between MySQL Database and the webserver
            Connection.Close();

            // return the final list of student names
            return Students;

        }
    }
    }

