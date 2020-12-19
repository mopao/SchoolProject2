using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolProject2.Models
{
    public class Class
    {
        public string classCode;
        public DateTime startDate;
        public DateTime finishDate;
        public string className;


        /// <summary>
        /// server side validation
        /// </summary>
        /// <returns> true if the fields are valid</returns>
        public bool IsValid()
        {
            bool valid = true;
            if (String.IsNullOrEmpty(className) || String.IsNullOrEmpty(classCode))
            {
                valid = false;
            }
            else
            {
                //Validation for fields to make sure they meet server constraints
                if (classCode.Length < 2 || classCode.Length > 255)
                {
                    valid = false;
                }

                if (className.Length < 2 || className.Length > 255)
                {
                    valid = false;
                }

                if (startDate >= finishDate)
                {
                    valid = false;
                }


            }


            return valid;
        }
    }
}