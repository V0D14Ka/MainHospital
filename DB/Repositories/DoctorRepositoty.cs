using DB.Converters;
using DB.Converters.FromDomain;
using DB.Converters.ToDomain;
using DB.Models;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationContext _context;

        public DoctorRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Doctor?> GetDoctorsBySpec(Specialization spec)
        {
            // FirstOrDefault вернет либо одну запись, либо нуль
            var doctor = _context.Doctors.Where(u => u.Name == spec.Name);
            var doctors = doctor.Select(x => x.ToDomain()).ToList();
            return doctors;
        }

        public Doctor? GetDoctorById(int id)
        {
            var doctor = _context.Doctors.FirstOrDefault(u => u.Id == id);
            return doctor?.ToDomain();
        }

        public bool IsDoctorExist(int id)
        {
            var flag = _context.Doctors.FirstOrDefault(u => u.Id == id);
            return flag != null;
        }

        public bool IsDoctorExist(Doctor doctor)
        {
            var flag = _context.Doctors.FirstOrDefault(u => u.Id == doctor.Id);
            return flag != null;
        }

        public bool Create(Doctor doctor)
        {
            DoctorModel? newdoctor = doctor.ToDoctorModel();
            try { _context.Doctors.Add(newdoctor); }
            catch { return false; }
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var doctor = _context.Doctors.SingleOrDefault(u => u.Id == id);
            if (doctor != null)
            {
                var flag = _context.Doctors.Remove(doctor);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(Doctor doctor)
        {
            var newdoctor = doctor.ToDoctorModel();
            var _doctor = _context.Doctors.SingleOrDefault(u => u.Id == doctor.Id);
            if (_doctor != null)
            {
                _doctor = newdoctor;
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

        public IEnumerable<Doctor?> GetAll()
        {
            var _doctors = _context.Doctors.ToList();
            var doctors = _doctors.Select(x => x.ToDomain()).ToList();
            return doctors;
        }
    }
}
