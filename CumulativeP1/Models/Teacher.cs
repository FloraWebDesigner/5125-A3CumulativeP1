using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CumulativeP1.Models
{
    public class Teacher
    {

        // we use this class to describe what teachers' fields are for other components

        public string TeacherName { get; set; }
        public string EmployeeNumber { get; set; }
        public decimal TeacherSalary { get; set; }
        public DateTime TeacherHireDate { get; set; }
        public int TeacherId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode {  get; set; }

        // added on 7/22 to get new teacher
        public string TeacherFname { get; set; }
        public string TeacherLname { get; set; }

        // Fail to figure it out, only have 1 on 1 relationship now, and missing some class data if one teacher has multiple classes.
        // public List<Class>ClassName { get; set; }
        // public List<Class> ClassCode { get; set; }



        // update on 0812
        public bool IsValid()
        {
            bool valid = true;

            if (TeacherFname == null || TeacherLname == null || EmployeeNumber == null || TeacherSalary == 0)
            {
                Debug.WriteLine("TeacherFname:"+TeacherFname);
                Debug.WriteLine("TeacherLname:" + TeacherLname);
                Debug.WriteLine("EmployeeNumber:" + EmployeeNumber);
                Debug.WriteLine("TeacherSalary:" + TeacherSalary);
                Debug.WriteLine("TeacherHireDate:" + TeacherHireDate);
                //Base validation to check if the fields are entered.
                valid = false;
            }
            else
            {
                //Validation for fields to make sure they meet server constraints
                if (TeacherFname.Length < 2 || TeacherFname.Length > 255) { valid = false; }
                if (TeacherLname.Length < 2 || TeacherLname.Length > 255) { valid = false; }

                // Validation for hire date
                DateTime minDate = new DateTime(1900, 1, 1);
                DateTime maxDate = DateTime.Now;
                if (TeacherHireDate < minDate || TeacherHireDate > maxDate)
                {
                    valid = false;
                }

                //Validation for salary
                Regex salary = new Regex(@"/ ^\d{ 2} (\.\d{ 1,2})?$/");
                if (!salary.IsMatch(TeacherSalary.ToString("F2")))
                {
                    valid = false;
                }

                //Validation for employee number
                Regex employeeNum = new Regex(@"/ ^T\d{ 3}$/");
                if (!employeeNum.IsMatch(EmployeeNumber))
                {
                    valid = false;
                }
            }

                Debug.WriteLine("The model validity is : " + valid);

                return valid;
            
        }


        //parameter-less constructor function
        public Teacher() { }

    }
}