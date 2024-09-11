using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Models;

namespace LMS.Repositories.Read
{
    internal interface IBookReadRepository : IReadRepository<Book>
    {
        Book GetBookByAuthor(string author);
        Book GetBookByID(int id);   
       IEnumerable<Book> GetAllBooksByPrice();
    }
}
