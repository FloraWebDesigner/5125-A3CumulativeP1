using CumulativeP1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CumulativeP1.Controllers
{
    public class CourseController : Controller
    {
        // POST: Course/show/{courseid}
        public ActionResult Show(int CourseId)
        {

            // work with the course data controller
            CourseDataController Controller = new CourseDataController();

            //call the find student method to get all the students in this class
            List<Student> NewStudent = Controller.FindStudent(CourseId);

            //pass along the FindStudent to the view
            //Views/Course/Show.cshtml
            return View(NewStudent);
        }

        // GET: course/list/{SearchKey?}
        public ActionResult List(string SearchKey=null)
        {
            // work with the course data controller
            CourseDataController controller = new CourseDataController();

            //call the list courses method
            List<Course> Courses = controller.ListCourse(SearchKey);

            //pass along the List<Course> to the view
            //Views/Course/List.cshtml
            ViewData["SearchKey"] = SearchKey;
            return View(Courses);
        }
    }
}