using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Student
    {
        public int LibraryCardNum { get; set; }    // Unique 6-digit library card number
        public string FirstName { get; set; }      // First name of the student
        public string LastName { get; set; }       // Last name of the student
    }
}
