using LMS.Data;
using LMS.Models;
using LMS.Repositories;
using LMS.Repositories.Read;
using LMS.Repositories.Repository;
using LMS.Repositories.Write;
using LMS.Services.Security;
using LMS.Services.UserService;
namespace LMS
{
    class Program
    {
        private static User? CurrentUser { get; set; }
        private static RepositoryFactory? repositoryFactory {  get; set; }
        private static IUserReadRepository? UserRepo { get; set; }
        private static IUserWriteRepository? AdminRepo { get; set; }
        private static IBookReadRepository? BookRepo { get; set; }
        public static void Main(string[] args)
        {
            LibraryContext context = new LibraryContext();
            UserService userService = new UserService(context);

            Console.WriteLine("Login: 1 |  Registration: 2");

            int.TryParse(Console.ReadLine(), out int RoleInput);
            if (RoleInput == 1)
            {
                CurrentUser = UserInterfaceLogin(userService);
                repositoryFactory = new RepositoryFactory(CurrentUser,context);
                UserRepo = repositoryFactory?.GetReadRepository<User>() as IUserReadRepository;
                AdminRepo = repositoryFactory?.GetWriteRepository<User>() as IUserWriteRepository;
                BookRepo = repositoryFactory?.GetReadRepository<Book>() as IBookReadRepository;

                if (AdminRepo != null)
                    Console.WriteLine($"Welcome Admin ");
                else if (UserRepo != null)
                {
                    Console.WriteLine($"Welcome {UserRepo?.GetUserFullName(CurrentUser.UserID)}");
                    if (BookRepo != null)
                    {
                        userService.BuyBook(BookRepo?.GetBookByID(2), CurrentUser);
                    }
                }
            }
            else if (RoleInput == 2)
            {
                CurrentUser = UserInterfaceRegistration(userService);
                repositoryFactory = new RepositoryFactory(CurrentUser, context);
                UserRepo = repositoryFactory?.GetReadRepository<User>() as IUserReadRepository;
                AdminRepo = repositoryFactory?.GetWriteRepository<User>() as IUserWriteRepository;
            }
            else
            {
                throw new Exception("Invalid InputRole");
            }

            Console.ReadLine();
        }

        public static User UserInterfaceRegistration(UserService userService)
        {
            Console.Write("Login:   ");
            string? UserName = Console.ReadLine()?.Trim();
            Console.Write("Password:   ");
            string? Password = Console.ReadLine()?.Trim();
            Console.Write("Email:   ");
            string? UserEmail = Console.ReadLine()?.Trim();
            Console.Write("FirstName:   ");
            string? FirstName = Console.ReadLine()?.Trim();
            Console.Write("LastName:   ");
            string? LastName = Console.ReadLine()?.Trim();
            DateTime DateOfBirth = DateTime.Now;
            if 
            (
                   UserEmail != null
                && UserName != null
                && FirstName != null
                && LastName != null
                && Password != null
            )
            {
                User user = new User(UserName, FirstName, LastName, DateOfBirth, Password, UserEmail);
                userService.RegistrationUser(user);
                return user;
            }
            throw new Exception("Invalid Operation");
        }
        public static User UserInterfaceLogin(UserService userService)
        {
            Console.Write("Login:  ");
            string? UserName = Console.ReadLine()?.Trim();
            Console.Write("Password:  ");
            string? Password = Console.ReadLine()?.Trim();

            if (UserName != null && Password != null)
            {
                return userService.UserAuthenticate(UserName, Password);
            }
            throw new ArgumentNullException();
        }
    }
}