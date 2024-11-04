using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public string BookNum { get; set; }           // e.g., "00001-2023"
        public string Title { get; set; }              // Title of the book
        public string Author { get; set; }             // Author's name
        public string Publisher { get; set; }          // Publisher's name
        public string ISBN { get; set; }               // ISBN, fixed-length 13
        public string PublicationPlace { get; set; }   // Place of publication
        public DateTime PublicationDate { get; set; }  // Date the book was published
    }
}
