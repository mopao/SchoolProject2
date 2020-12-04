using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolProject2.Models;
using System.Diagnostics;

namespace SchoolProject2.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Teacher/List
        // acquire the list of teachers and send it to List.cshtml
        [HttpGet]

        public ActionResult List()
        {
            //we will to gather the list of teachers 
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> teachers = controller.ListTeachers();
            return View(teachers);
        }



        // acquire a teacher's data  and send it to show.cshtml       
        [HttpGet]
        public ActionResult Show(int? id)
        {
            //we will to gather the list of teachers 
            TeacherDataController teacherController = new TeacherDataController();
            Teacher teacher = teacherController.GetTeacherById(id);
            // gather teacher classes
            ClassDataController classesController = new ClassDataController();
            teacher.classes = classesController.ListTeachersClasses(id);

            return View(teacher); ;
        }

        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/Add
        public ActionResult Add()
        {
            return View();
        }

        //GET : /Teacher/Ajax_Add
        public ActionResult Ajax_Add()
        {
            return View();

        }

        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string TeacherNumber, string TeacherSalary)
        {
            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(TeacherNumber);
            Debug.WriteLine(TeacherSalary);

            Teacher newTeacher = new Teacher();
            newTeacher.firstname = TeacherFname;
            newTeacher.lastname = TeacherLname;
            newTeacher.employeeNumber = TeacherNumber;
            newTeacher.salary = decimal.Parse(TeacherSalary, System.Globalization.CultureInfo.InvariantCulture);

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(newTeacher);

            return RedirectToAction("List");
        }

    }
}