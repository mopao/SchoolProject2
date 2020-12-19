using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolProject2.Models
{
    public class Teacher
    {
        // a Teacher description
        public int id;
        public string firstname;
        public string lastname;
        public string employeeNumber;
        public decimal salary;
        public DateTime hireDate;
        public List<Class> classes = new List<Class>();

        /// <summary>
        /// server side validation
        /// </summary>
        /// <returns> true if the fields are valid</returns>
        public bool IsValid()
        {
            bool valid = true;
            if(String.IsNullOrEmpty(firstname)||String.IsNullOrEmpty(lastname)|| String.IsNullOrEmpty(employeeNumber)||
                salary <= 0)
            {
                valid=false;
            }
            else
            {
                //Validation for fields to make sure they meet server constraints
                if (firstname.Length < 2 || firstname.Length > 255)
                {
                    valid = false;
                }

                if (lastname.Length < 2 || lastname.Length > 255)
                {
                    valid = false;
                }

                if (employeeNumber.Length < 2 || employeeNumber.Length > 255)
                {
                    valid = false;
                }


            }


            return valid;
        }
    }
}