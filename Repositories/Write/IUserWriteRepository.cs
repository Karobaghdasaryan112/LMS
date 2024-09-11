using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Models;
namespace LMS.Repositories.Write
{
    internal interface IUserWriteRepository : IWriteRepository<User>
    {
        void DeleteUserByID(int Id);
        void UpdateUserMoney(int Id,decimal Money);
    }
}
