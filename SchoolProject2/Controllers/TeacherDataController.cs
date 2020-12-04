using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolProject2.Models;
using MySql.Data.MySqlClient;
using System.Web.Http.Cors;


namespace SchoolProject2.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext school = new SchoolDbContext();
        //This Controller Will access the teachers table of our schooldb database.
        /// <summary>
        /// Returns a list of teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers(first names and last names)
        /// </returns>
        [HttpGet]
        public List<Teacher> ListTeachers()
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers order by teacherfname asc";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers Names
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {

                //Access Column information by the DB column name as an index
                Teacher newTeacher = new Teacher();
                newTeacher.id = ResultSet.GetInt32(0);
                newTeacher.firstname = ResultSet.GetString(1);
                newTeacher.lastname = ResultSet.GetString(2);
                newTeacher.employeeNumber = ResultSet.GetString(3);
                newTeacher.hireDate = ResultSet.GetDateTime(4);
                newTeacher.salary = ResultSet.GetDecimal(5);

                //Add the teacher Name to the List
                Teachers.Add(newTeacher);

            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teachers names
            return Teachers;

        }
        /// <summary>
        /// get information on a teacher in the system
        /// </summary>
        /// <example> GET: api/GetTeacherData/Show/1   display the teacher's profile with the id 1</example>
        /// <param name="id"> teacher's id</param>
        /// <returns> the teacher with the given id</returns>
        [HttpGet]
        [Route("api/TeacherData/GetTeacherById/{id}")]
        public Teacher GetTeacherById(int? id)
        {
            Teacher newTeacher = new Teacher();
            if (id != null)
            {
                //Create an instance of a connection
                MySqlConnection Conn = school.AccessDatabase();

                //Open the connection between the web server and database
                Conn.Open();

                //Establish a new command (query) for our database
                MySqlCommand cmd = Conn.CreateCommand();

                //SQL QUERY
                cmd.CommandText = "Select * from teachers where teacherid = @id" ;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();


                //Gather Result Set of Query into a variable
                MySqlDataReader ResultSet = cmd.ExecuteReader();

                //Loop Through Each Row the Result Set
                while (ResultSet.Read())
                {
                    //Access Column information by the DB column name as an index                
                    newTeacher.id = ResultSet.GetInt32(0);
                    newTeacher.firstname = ResultSet.GetString(1);
                    newTeacher.lastname = ResultSet.GetString(2);
                    newTeacher.employeeNumber = ResultSet.GetString(3);
                    newTeacher.hireDate = ResultSet.GetDateTime(4);
                    newTeacher.salary = ResultSet.GetDecimal(5);
                }

                //Close the connection between the MySQL Database and the WebServer
                Conn.Close();

            }
            //Return the final list of teachers names
            return newTeacher;

        }
        // <summary>
        /// Adds a teacher to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the teacher's table. Non-Deterministic.</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"franck",
        ///	"TeacherLname":"Cheuzem",
        ///	"TeacherNumber":"Likes Coding!",
        ///	"TeacherSalary":"97.9"      
        /// }
        /// </example>

        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody] Teacher newTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) " +
                               "values (@TeacherFname,@TeacherLname,@TeacherNumber, Now(), @TeacherSalary)";
            cmd.Parameters.AddWithValue("@TeacherFname", newTeacher.firstname);
            cmd.Parameters.AddWithValue("@TeacherLname", newTeacher.lastname);
            cmd.Parameters.AddWithValue("@TeacherNumber", newTeacher.employeeNumber);
            cmd.Parameters.AddWithValue("@TeacherSalary", newTeacher.salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }
        /// <summary>
        /// Deletes a teacher from the connected MySQL Database if the ID of that teacher exists. Does NOT maintain relational integrity. Non-Deterministic.

        /// </summary>
        /// <param name="id"> the ID of the teacher</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/1</example>


        [HttpPost]
        public void DeleteTeacher(int id)
        {

            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }
    }
}
