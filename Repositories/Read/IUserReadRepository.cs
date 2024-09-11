using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Models;
namespace LMS.Repositories.Read
{
    internal interface IUserReadRepository : IReadRepository<User>
    {
        decimal GetUserMoney(int Id);
        string GetUserFullName(int Id);
        User GetUserByUserName(string userName);
        IEnumerable<User> GetUsers();

    }
}
