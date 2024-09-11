using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Models
{
    internal class UserBooks
    {
        [Key]
        public int UserBookID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public UserBooks() { }
        public UserBooks(int UserID,int BookID,DateTime PurchaseDate)
        {
            this.UserID = UserID;
            this.BookID = BookID;
            this.PurchaseDate = PurchaseDate;
        }
    }
}
