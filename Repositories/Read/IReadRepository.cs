using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repositories.Read
{
    internal interface IReadRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
    }
}