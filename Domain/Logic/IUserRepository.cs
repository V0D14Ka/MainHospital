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
        bool IsUserExists(string login);
        User? GetUserByLogin(string login);
        bool CreateUser(User user);
    }
}
