using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Borrow
    {
        public int BorrowID { get; set; }               // Unique ID for each borrow transaction
        public int StudentLibraryCardNum { get; set; }  // Foreign key referencing Student.LibraryCardNum
        public string BookBookNum { get; set; }         // Foreign key referencing Book.BookNum
        public DateTime BorrowDate { get; set; }        // Date the book was borrowed
        public DateTime ReturnDate { get; set; }        // Date the book was returned
        public DateTime DueDate { get; set; }           // Due date for returning the book
    }
}
