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
            

            Teacher newTeacher = new Teacher();
            newTeacher.firstname = TeacherFname;
            newTeacher.lastname = TeacherLname;
            newTeacher.employeeNumber = TeacherNumber;
            newTeacher.salary = decimal.Parse(TeacherSalary, new System.Globalization.CultureInfo("en-US"));

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(newTeacher);

            return RedirectToAction("List");
        }
        /// <summary>
        /// Routes to a dynamically generated "teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the teacher</param>
        /// <returns>A dynamic "Update teacher" webpage which provides the current information of the teacher and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher/Update/5</example>

        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher teacher = controller.GetTeacherById(id);
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            teacher.salary= decimal.Parse(teacher.salary.ToString());

            return View(teacher);
        }

        /// <summary>
        /// Routes to a dynamically generated "teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the teacher</param>
        /// <returns>A dynamic "Update teacher" webpage which provides the current information of the teacher and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher/Ajax_Update/5</example>

        public ActionResult Ajax_Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher teacher = controller.GetTeacherById(id);
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            teacher.salary = decimal.Parse(teacher.salary.ToString());

            return View(teacher);
        }

        /// <summary>
        ///Receives a POST request containing information about an existing teacher in the system, with new values.
        ///Conveys this information to the API, and redirects to the "Teacher Show" page of our updated author.
        /// </summary>
        /// <param name="TeacherId">id the teacher to update</param>
        /// <param name="TeacherFname"> the updated firstname of the teacher</param>
        /// <param name="TeacherLname">the updated lastname of the teacher</param>
        /// <param name="TeacherNumber">the updated employee number of the teacher</param>
        /// <param name="TeacherSalary">the updated salary of the teacher</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// {
        /// "TeacherId":"5",
        ///	"TeacherFname":"franck",
        ///	"TeacherLname":"Cheuzem",
        ///	"TeacherNumber":"L9990",
        ///	"TeacherSalary":"95.9"      
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(string TeacherId, string TeacherFname, string TeacherLname, string TeacherNumber, string TeacherSalary)
        {
            Teacher newTeacher = new Teacher();
            newTeacher.firstname = TeacherFname;
            newTeacher.lastname = TeacherLname;
            newTeacher.employeeNumber = TeacherNumber;
            newTeacher.salary = decimal.Parse(TeacherSalary, new System.Globalization.CultureInfo("en-US"));
            newTeacher.id = int.Parse(TeacherId);

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(newTeacher);

            return RedirectToAction("Show/"+TeacherId);

        }
        /// <summary>
        /// assigned a class to a teacher in the database
        /// </summary>
        /// <param name="id"> id of the teacher</param>
        /// <returns> dynamic webpage which allows to assigned a class to a teacher</returns>
        /// <example>GET: /Teacher/AddTeacherClass/5</example>
        public ActionResult AddTeacherClass(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher teacher = controller.GetTeacherById(id);
            return View(teacher);

        }
        [HttpPost]
        public ActionResult AddTeacherClass(int id, string ClassCode, string ClassName, DateTime StartDate, DateTime EndDate)
        {
            ClassDataController controller = new ClassDataController();
            Class newClass = new Class();
            newClass.classCode = ClassCode;
            newClass.className = ClassName;
            newClass.finishDate = EndDate;
            newClass.startDate = StartDate;
            controller.AddClass(id,newClass);
            return RedirectToAction("Show/"+id);
        }



    }
}