using LMS.Data;
using LMS.Models;
using LMS.Repositories.Read;
using LMS.Repositories.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repositories
{
    internal class UserRepository : IUserReadRepository, IUserWriteRepository
    {
        private readonly LibraryContext _libraryContext;
        public UserRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        void IUserWriteRepository.DeleteUserByID(int Id)
        {
            if (_libraryContext != null)
            {
                User? user = _libraryContext.Users.FirstOrDefault(user => user.UserID == Id);
                if (user != null)
                {
                    _libraryContext.Users.Remove(user);
                    _libraryContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("user is null");
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(_libraryContext));
            }
        }

        string IUserReadRepository.GetUserFullName(int Id)
        {
            if (_libraryContext != null)
            {
                User? user = _libraryContext.Users.FirstOrDefault(user => user.UserID == Id);
                if (user != null)
                {
                    return user.FirstName + " " + user.LastName;
                }
            }
            throw new ArgumentNullException(nameof(_libraryContext));
        }

        decimal IUserReadRepository.GetUserMoney(int Id)
        {
            if (_libraryContext != null)
            {
                return _libraryContext.Users.FirstOrDefault(user => user.UserID == Id).Money;
            }
            throw new ArgumentNullException(nameof(_libraryContext));
        }

        void IUserWriteRepository.UpdateUserMoney(int Id, decimal Money)
        {
            if (_libraryContext != null)
            {
                User? user = _libraryContext.Users.FirstOrDefault(user => user.UserID == Id);
                if (user != null)
                {
                    user.Money = Money;
                    _libraryContext.Users.Update(user);
                    _libraryContext.SaveChanges();
                }
            }
        }

        IEnumerable<User> IUserReadRepository.GetUsers()
        {
            if (_libraryContext != null)
            {
                return _libraryContext.Users.ToList();
            }
            throw new ArgumentNullException(nameof(_libraryContext));
        }

        IEnumerable<User> IReadRepository<User>.GetAll() => _libraryContext.Users.ToList();

        void IWriteRepository<User>.Delete(User entity)
        {
            User? user = _libraryContext.Users.FirstOrDefault(user => user == entity);
            if (user != null)
            {
                _libraryContext.Users.Remove(user);
                _libraryContext.SaveChanges();
            }
        }
        void IWriteRepository<User>.Add(User entity)
        {
            User? user = _libraryContext.Users.FirstOrDefault(user => user.UserName == entity.UserName);
            if (user == null)
            {
                _libraryContext.Users.Add(entity);
                _libraryContext.SaveChanges();
            }
        }

        public User GetUserByUserName(string userName)
        {
            User? user = _libraryContext.Users.FirstOrDefault(user => user.UserName == userName);
            if (user != null)
                return user;
            return null;
        }
    }
}
