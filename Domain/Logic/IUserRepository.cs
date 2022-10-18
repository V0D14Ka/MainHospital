using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsUserExist(string login);
        bool IsUserExist(User user);
        User? GetUserByLogin(string login);
        User? GetUserByID(int id);
        bool IsUserExistByID(int id);
        bool CreateUser(User user);
    }
}
