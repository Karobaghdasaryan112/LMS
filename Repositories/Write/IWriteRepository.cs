using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repositories.Write
{
    internal interface IWriteRepository<T> where T : class
    {
        void Delete(T entity);
        void Add(T entity);
    }
}