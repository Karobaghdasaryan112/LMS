using LMS.Data;
using LMS.Models;
using LMS.Repositories;
using LMS.Repositories.Write;
using LMS.Services.Security;
using LMS.Services.UserService.Interfaces;
using System.Linq.Expressions;
namespace LMS.Services.UserService
{
    internal class UserService : IUserService
    {
        private LibraryContext _libraryContext { get; set; }
        private UserRepository _userRepository { get; set; }
        private PasswordService PasswordService { get; set; }
        public UserService(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
            _userRepository = new UserRepository(libraryContext);
            PasswordService = new PasswordService();
        }
        public User GetUserDetails(User user)
        {
            return user;
        }

        public void RegistrationUser(User? user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            User? ExistingUser = _userRepository.GetUserByUserName(user.UserName);
            if (ExistingUser == null)
            {
                string HashPassword = PasswordService.HashPassword(user.Password);
                user.Password = HashPassword;
                ((IWriteRepository<User>)_userRepository).Add(user);
                _libraryContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("this User is already exist");
            }
        }

        public void ResetLogin(User user, string NewUserName)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (NewUserName == null)
                throw new ArgumentNullException(nameof(NewUserName));
            string? OldUserName = _userRepository.GetUserByUserName(user.UserName)?.UserName;
            if (OldUserName != NewUserName)
            {
                user.UserName = NewUserName;
                ((IWriteRepository<User>)_userRepository).Add(user);
                _libraryContext.SaveChanges();
            }
        }

        public void ResetPassword(User user, string NewPassword)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (NewPassword == null)
                throw new ArgumentNullException(nameof(NewPassword));
            string? OldPassword = _userRepository.GetUserByUserName(user.UserName)?.Password;
            if (OldPassword != NewPassword)
            {
                string NewPasswordHash = PasswordService.HashPassword(NewPassword);
                user.Password = NewPasswordHash;
                ((IWriteRepository<User>)_userRepository).Add(user);
                _libraryContext.SaveChanges();
            }
        }

        public User UserAuthenticate(string username, string password)
        {
            if (username == null) throw new ArgumentNullException("username");
            if (password == null) throw new ArgumentNullException("password");
            User? AuthenticateUser = _userRepository.GetUserByUserName(username);
            if (AuthenticateUser != null)
            {
                if (PasswordService.VerifyPassword(password, AuthenticateUser.Password))
                {
                    if (AuthenticateUser.UserName == "Admin" && password == "Admin")
                    {
                        AuthenticateUser.Role = "Admin";
                    }
                    else
                    {
                        AuthenticateUser.Role = "User";
                    }
                    _libraryContext.Users.Update(AuthenticateUser);
                    _libraryContext.SaveChanges();
                    return AuthenticateUser;
                }
            }
            throw new UnauthorizedAccessException("Incorrect username or password.");
        }
        public void BuyBook(Book book, User user)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (user.Money < book.Price)
                throw new InvalidOperationException();
            UserBooks userBooks = new UserBooks()
            {
                BookID = book.BookID,
                UserID = user.UserID,
                PurchaseDate = DateTime.Now,
            };
            user.Money -= book.Price;
            _libraryContext.UserBooks.Add(userBooks);
            _libraryContext.Users.Update(user);
            _libraryContext.SaveChanges();
            Console.WriteLine($"{user.UserName}:you buy the book {book.Title} And your Balance is {user.Money}");
        }
    }
}