using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CumulativeP1.Models
{
    public class Course
    {
        // use this class to describe what classes' properties are for other components
        public int CourseId { get; set; }
        public string CourseName  { get; set; }
        public string CourseCode { get; set; }
        // change DATETIME type for StartDate to take out 12:00AM
        public string StartDate { get; set; }
        // change DATETIME type for EndDate to take out 12:00AM
        public string FinishDate { get; set; }


    }

    
    public class Student
    {
        // use this class to describe what students' properties are for other components

        public string StudentName { get; set; }
        public string StudentNumber { get; set; }
        // change DATETIME type for Entoldate to take out 12:00AM
        public string Enroldate { get; set; }
        public int CourseId { get; set;}


    }
    
}