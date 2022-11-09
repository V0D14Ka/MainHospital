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
    public class SheduleRepository : ISheduleRepository
    {
        private readonly ApplicationContext _context;

        public SheduleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Shedule? GetSheduleByDoctorAndDate(Doctor doctor, DateTime date)
        {
            // FirstOrDefault вернет либо одну запись, либо нуль
            var shedule = _context.Shedules.FirstOrDefault(u => u.DoctorId == doctor.Id && u.StartWorking == date);
            return shedule?.ToDomain();
        }

        public bool Create(Shedule shedule)
        {
            SheduleModel? newShedule = shedule.ToSheduleModel();
            try { _context.Shedules.Add(newShedule); }
            catch { return false; }
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var shedule = _context.Shedules.SingleOrDefault(u => u.DoctorId == id);
            if (shedule != null)
            {
                var flag = _context.Shedules.Remove(shedule);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(Shedule shedule)
        {
            var newShedule = shedule.ToSheduleModel();
            var _shedule = _context.Shedules.SingleOrDefault(u => u.DoctorId == shedule.DoctorId);
            if (_shedule != null)
            {
                _shedule = newShedule;
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

        public IEnumerable<Shedule?> GetAll()
        {
            var _shedules = _context.Shedules.ToList();
            var shedules = _shedules.Select(x => x.ToDomain()).ToList();
            return shedules;
        }
    }
}
