using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LMS.Data;
using LMS.Models;
using LMS.Repositories.Read;
using LMS.Repositories.Write;
namespace LMS.Repositories.Repository
{
    internal class RepositoryFactory
    {
        private User CurrentUser { get; set; }
        private LibraryContext _LibraryContext { get; set; }
        public RepositoryFactory(User user, LibraryContext libraryContext)
        {
            CurrentUser = user;
            _LibraryContext = libraryContext;
        }
        public IWriteRepository<T> GetWriteRepository<T>() where T : class
        {
            if (CurrentUser.Role == "Admin")
            {
                return CreateWriteRepository<T>();
            }
            Console.WriteLine("you haven't success for Write");
            return default;
        }
        public IReadRepository<T> GetReadRepository<T>() where T : class
        {
            if (CurrentUser.Role == "Admin" || CurrentUser.Role == "User")
            {
                return CreateReadRepository<T>();
            }
            throw new Exception("invalid Role");
        }
        private IWriteRepository<T> CreateWriteRepository<T>() where T : class
        {
            if (typeof(T) == typeof(Book))
            {
                return (IWriteRepository<T>)new BookRepository(_LibraryContext);
            }
            if (typeof(T) == typeof(User))
            {
                return (IWriteRepository<T>)new UserRepository(_LibraryContext);
            }
            throw new Exception("invalid Type");
        }
        private IReadRepository<T> CreateReadRepository<T>() where T : class
        {
            if (typeof(T) == typeof(Book))
            {
                return (IReadRepository<T>)new BookRepository(_LibraryContext);
            }
            if (typeof(T) == typeof(User))
            {
                return (IReadRepository<T>)new UserRepository(_LibraryContext);
            }
            throw new Exception("invalid Type");
        }
    }
}
