
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulativeP1.Models;
using System.Web.Http.Cors;

namespace CumulativeP1.Controllers
{
    public class TeacherController : Controller
    { 

        //We can instantiate the authorcontroller outside of each method
        private TeacherDataController TeacherDataController = new TeacherDataController();

    // GET: teacher/show/{id}
    public ActionResult Show(int id)
        {
            try { 
            // work with the teacher data controller
            TeacherDataController controller = new TeacherDataController();

            //call the find teacher method
            Teacher NewTeacher = controller.FindTeacher(id);
            
            //pass along the FindTeacher to the view
            //Views/Teacher/Show.cshtml
            return View(NewTeacher);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: teacher/list/{SearchKey}
        public ActionResult List(string SearchKey)
        {
            try { 
            // work with the teacher data controller
            TeacherDataController controller = new TeacherDataController();

            //call the list teachers method
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);

            //pass along the List<Teacher> to the view
            //Views/Teacher/List.cshtml
            ViewData["SearchKey"] = SearchKey;
          
           return View(Teachers);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                //Debug.WriteLine(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        // POST:Teacher/Create
        // receive information about the teacher
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime TeacherHireDate, decimal TeacherSalary)
        {
            Debug.WriteLine("The teacher's first name is " + TeacherFname);
            Debug.WriteLine("The teacher's last name is " + TeacherLname);
            Debug.WriteLine("The teacher's hiredate is " + TeacherHireDate);
            Debug.WriteLine("The teacher's employeenumber is " + EmployeeNumber);
            Debug.WriteLine("The teacher's salary is " + TeacherSalary);

            try
            {

                //todo: add the teacher to the database
                TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.TeacherHireDate = TeacherHireDate;
            NewTeacher.TeacherSalary = TeacherSalary;

            controller.AddTeacher(NewTeacher);

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }

            // will execute the action of list view
            return RedirectToAction("List");

        }
        public ActionResult New()
        {
            return View();

        }

        //GET : /Teacher/Ajax_New
        // updated on 0729: Ajax does not work
        public ActionResult Ajax_New()
        {
            return View();

        }

        //GET : /Teacher/Ajax_Show
        public ActionResult Ajax_Show()
        {
            return View();

        }

        [HttpGet]
        [Route("/teacher/ConfirmDelete/{TeacherId}")]
        // GET: /teacher/ConfirmDelete/{TeacherId}
        public ActionResult DeleteConfirm(int id)
        {
            try { 
            // work with the teacher data controller
            TeacherDataController controller = new TeacherDataController();

            //call the previous find teacher method as we need that specific teacher information to confirm with the user
            Teacher NewTeacher = controller.FindTeacher(id);

            //pass along the FindTeacher to the view
            //Views/Teacher/DeleteConfirm.cshtml
            return View(NewTeacher);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        ///POST: Teacher/Delete/
        ///
        [HttpPost]
        // POST: Teacher/Delete/{TeacherId} 
        public ActionResult Delete(int id)
        {
            try { 
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);

            return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: teacher/update/{id}
        public ActionResult Update(int id)
        {
            try { 
            // work with the teacher data controller
            TeacherDataController controller = new TeacherDataController();

            //call the find teacher method
            Teacher SelectedTeacher = controller.FindTeacher(id);

            //pass along the FindTeacher to the view
            //Views/Teacher/Update.cshtml
            return View(SelectedTeacher);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }


        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="TeacherHireDate">The updated hire date of the teacher.</param>
        /// <param name="EmployeeNumber">The updated employee number of the teacher.</param>
        /// <param name="TeacherSalary">The updated salary of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/3
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Linda",
        ///	"TeacherLname":"Chan",
        ///	"TeacherHireDate":"2015=08=12"
        ///	"EmployeeNumber":"T382",
        ///	"TeacherSalary":"60.22"
        /// }
        /// </example>
        
        // POST : /Teacher/Edit/{id}
        [HttpPost]
        public ActionResult Edit(int id,string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime TeacherHireDate, decimal TeacherSalary)
        {
            Debug.WriteLine("The teacher's first name is " + TeacherFname);
            Debug.WriteLine("The teacher's last name is " + TeacherLname);
            Debug.WriteLine("The teacher's hiredate is " + TeacherHireDate);
            Debug.WriteLine("The teacher's employeenumber is " + EmployeeNumber);
            Debug.WriteLine("The teacher's salary is " + TeacherSalary);

            try { 
            //todo: add the teacher to the database
            TeacherDataController controller = new TeacherDataController();
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.EmployeeNumber = EmployeeNumber;
            TeacherInfo.TeacherHireDate = TeacherHireDate;
            TeacherInfo.TeacherSalary = TeacherSalary;

            controller.EditTeacher(id,TeacherInfo);
            // will execute the action of list view
            return RedirectToAction("Show/"+id);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }

        }


        //GET : /Teacher/Error
        /// <summary>
        /// This window is for showing Teacher Specific Errors!
        /// </summary>
        public ActionResult Error()
        {
            return View();
        }


        //GET : /Teacher/Ajax_List
        public ActionResult Ajax_List()
        {
            return View();
        }


        /// <summary>
        /// Routes to a dynamically rendered "Ajax Update" Page. The "Ajax Update" page will utilize JavaScript to send an HTTP Request to the data access layer (/api/TeacherData/UpdateTeacher)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Ajax_Update(int id)
        {
            try { 
            // work with the teacher data controller
            TeacherDataController controller = new TeacherDataController();

            //call the find teacher method
            Teacher SelectedTeacher = controller.FindTeacher(id);

            //pass along the FindTeacher to the view
            //Views/Teacher/Ahax_Update.cshtml
            return View(SelectedTeacher);

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

    }
}