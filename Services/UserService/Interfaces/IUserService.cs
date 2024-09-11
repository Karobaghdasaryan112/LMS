using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Models;
namespace LMS.Services.UserService.Interfaces
{
    internal interface IUserService
    {
        void RegistrationUser(User user);
        User UserAuthenticate(string username, string password);
        void ResetLogin(User user,string NewUserName);
        void ResetPassword(User user,string NewPassword);
        User GetUserDetails(User user);
        void BuyBook(Book book, User user);
    }
}
