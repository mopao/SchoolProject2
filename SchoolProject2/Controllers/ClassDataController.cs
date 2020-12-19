using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolProject2.Models;
using MySql.Data.MySqlClient;

namespace SchoolProject2.Controllers
{
    public class ClassDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext school = new SchoolDbContext();
        /// <summary>
        /// return a teacher's classes in the system 
        /// </summary>
        /// <example> GET: api/ListTeacherClasses/2       return the classes of teacher with id 2 in he system </example>
        /// <param name="id"></param>
        /// <returns> return the list of classes of the teacher with the given id</returns>
        [HttpGet]
        [Route("api/ClassData/ListTeacherClasses/{id}")]
        public List<Class> ListTeachersClasses(int? id)
        {
            //create an empty list for the classes
            List<Class> classes = new List<Class>();

            if (id != null)
            {
                //Create an instance of a connection
                MySqlConnection Conn = school.AccessDatabase();

                //Open the connection between the web server and database
                Conn.Open();

                //Establish a new command (query) for our database
                MySqlCommand cmd = Conn.CreateCommand();

                //SQL QUERY
                cmd.CommandText = "Select classes.* from classes left join teachers on  " +
                    "teachers.teacherid=classes.teacherid  where teachers.teacherid=" + id;

                //Gather Result Set of Query into a variable
                MySqlDataReader ResultSet = cmd.ExecuteReader();

                while (ResultSet.Read())
                {
                    // add the new class to the teacher
                    Class newClass = new Class();
                    newClass.className = ResultSet.GetString(5);
                    newClass.classCode = ResultSet.GetString(1);
                    newClass.startDate = ResultSet.GetDateTime(3);
                    newClass.finishDate = ResultSet.GetDateTime(4);
                    classes.Add(newClass);

                }

            }
            return classes;
        }
        /// <summary>
        /// add class to a teacher
        /// </summary>
        /// <param name="teacherId"> id of the teacher</param>
        /// <param name="newClass"> class to add to the teacher</param>
        /// <example>
        /// POST api/ClassData/AddClass 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"classcode":"http6778",
        ///	"classname":"php language",
        ///	"teacherid":"17",
        ///	"startdate":"2021-01-18", 
        ///	"finishdate":"2021-05-30"
        /// }
        /// </example>
        [HttpPost]
        public void AddClass( int teacherId, [FromBody] Class newClass)
        {
            // check if the finish date greater than the start date before inserting
            if(newClass.IsValid())         
            {
                //Create an instance of a connection
                MySqlConnection Conn = school.AccessDatabase();

                //Open the connection between the web server and database
                Conn.Open();

                //Establish a new command (query) for our database
                MySqlCommand cmd = Conn.CreateCommand();

                //SQL QUERY
                cmd.CommandText = "insert into classes (classcode, classname,teacherid ,startdate , finishdate) " +
                                   "values (@code,@name,@teacherid, @begin, @end)";
                cmd.Parameters.AddWithValue("@code", newClass.classCode);
                cmd.Parameters.AddWithValue("@name", newClass.className);
                cmd.Parameters.AddWithValue("@teacherid", teacherId);
                cmd.Parameters.AddWithValue("@begin", newClass.startDate.Date);
                cmd.Parameters.AddWithValue("@end", newClass.finishDate.Date);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

                Conn.Close();


            }
        }
    }
}
