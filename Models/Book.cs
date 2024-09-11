using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LMS.Models
{
    internal class Book
    {
        public int BookID { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Author { get; set; }
        public int Count {  get; set; }
        public string? Category {  get; set; }
        public string? Title { get; set; }
        public Book() { }
    }
}
