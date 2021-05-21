
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using UserService.Models;
using UserService.Utility;

namespace UserService.Data.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUserForLogin(string email, string password)
        {
            var user = _context.Users
                .SingleOrDefault(u => (u.Email == email));
            var passwordHasher = new PasswordHasher();
            bool resultVerifyPassword = passwordHasher.VerifyPassword(user.Password, password);
            if (resultVerifyPassword==false)
            {
                
                return null;
            }

            return user;

        }

        public bool IsExistByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool IsExistByPhone(string phone)
        {
            return _context.Users.Any(u => u.PhoneNumber == phone);
        }

        public void ResetPassword(string Email,string pass)
        {
            var user = _context.Users.First(u=>u.Email==Email);
            var passwordHasher = new PasswordHasher();
            var hashedPassword = passwordHasher.HashPassword(pass);
            user.Password = hashedPassword;
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
