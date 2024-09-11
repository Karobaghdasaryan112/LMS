using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repositories.Write
{
    internal interface IBookWriteRepository : IWriteRepository<Book>
    {
        void DeleteBookById(int Id);
        void UpdatePrice(int Id, decimal Price);
        void ChangeBookCount(int Id, int count);
    }
}
