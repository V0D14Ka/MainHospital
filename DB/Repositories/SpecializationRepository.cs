using DB.Converters.FromDomain;
using DB.Converters.ToDomain;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Repositories
{
    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly ApplicationContext _context;

        public SpecializationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Specialization? GetSpecialization(string name)
        {
            // FirstOrDefault вернет либо одну запись, либо нуль
            var spec = _context.Specializations.FirstOrDefault(u => u.Name == name);
            return spec?.ToDomain();
        }

        public bool IsSpecializationExistById(int id)
        {
            var flag = _context.Specializations.FirstOrDefault(u => u.Id == id);
            return flag != null;
        }

        public bool IsSpecializationExist(string name)
        {
            var flag = _context.Specializations.FirstOrDefault(u => u.Name == name);
            return flag != null;
        }

        public bool IsSpecializationExist(Specialization spec)
        {
            var flag = _context.Specializations.FirstOrDefault(u => u.Id == spec.Id);
            return flag != null;
        }

        public bool Create(Specialization spec)
        {
            SpecializationModel? newspec = spec.ToSpecModel();
            try { _context.Specializations.Add(newspec); }
            catch { return false; }
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var spec = _context.Specializations.SingleOrDefault(u => u.Id == id);
            if (spec != null)
            {
                var flag = _context.Specializations.Remove(spec);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(Specialization spec)
        {
            var newspec = spec.ToSpecModel();
            var _spec = _context.Specializations.SingleOrDefault(u => u.Id == spec.Id);
            if (_spec != null)
            {
                _spec = newspec;
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

        public IEnumerable<Specialization?> GetAll()
        {
            var _users = _context.Specializations.ToList();
            var users = _users.Select(x => x.ToDomain()).ToList();
            return users;
        }
    }
}
