using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Data.Repositories.Users
{

    public interface IUserRepository
    {
        void AddUser(User user);
        bool IsExistByEmail(string email);
        bool IsExistByPhone(string phone);

        User GetUserForLogin(string email, string password);
        void ResetPassword(string Email, string pass);
    }
}
