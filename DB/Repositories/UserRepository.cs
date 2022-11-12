using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Converters.FromDomain;
using DB.Converters.ToDomain;
using DB.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DB.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public User? GetUserByLogin(string username)
        {
            // FirstOrDefault вернет либо одну запись, либо нуль
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);
            return user?.ToDomain();
        }

        public User? GetUserByID(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            return user?.ToDomain();
        }

        public bool IsUserExist(string username)
        {
            var flag = _context.Users.FirstOrDefault(u => u.UserName == username);
            return flag != null;
        }

        public bool IsUserExist(User user)
        {
            var flag = _context.Users.FirstOrDefault(u => u.UserName == user.UserName);
            return flag != null;
        }

        public bool IsUserExistByID(int id)
        {
            var flag = _context.Users.FirstOrDefault(u => u.Id == id);
            return flag != null;
        }

        public bool Create(User user)
        {
            try { _context.Users.Add(user.ToUserModel());}
            catch { return false; }
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user != null)
            {
                var flag = _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(User user)
        {
            var _user = _context.Users.SingleOrDefault(u => u.Id == user.Id);
            if (_user != null)
            {
                _user = user.ToUserModel();
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Save()
        {
            _context.SaveChanges();
            return;
        }

        public IEnumerable<User?> GetAll()
        {
            var _users = _context.Users.ToList();
            var users = _users.Select(x => x.ToDomain()).ToList();
            return users;
        }
    }
}
