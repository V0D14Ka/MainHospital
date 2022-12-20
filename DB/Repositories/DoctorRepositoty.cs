using DB.Converters;
using DB.Converters.FromDomain;
using DB.Converters.ToDomain;
using DB.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Doctor?> GetDoctorsBySpec(int specid)
        {
            var doctor = _context.Doctors.Include(u => u.Specialization).Where(u => u.Specialization.Id == specid);
            var doctors = doctor.Select(x => x.ToDomain()).ToList();
            return doctors;
        }

        public Doctor? GetDoctorById(int id)
        {
            var doctor = _context.Doctors.Include(u => u.Specialization).FirstOrDefault(u => u.Id == id);
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
            SpecializationModel spec = _context.Specializations.FirstOrDefault(u => u.Id == doctor.Specialization.Id);
            DoctorModel? newdoctor = doctor.ToDoctorModel();
            newdoctor.Specialization = spec;
            try { _context.Doctors.Add(newdoctor); }
            catch { return false; }
            return true;
        }

        public bool Delete(int id)
        {
            var doctor = _context.Doctors.SingleOrDefault(u => u.Id == id);
            if (doctor != null)
            {
                var flag = _context.Doctors.Remove(doctor);
                return true;
            }
            return false;
        }

        public bool Update(Doctor doctor)
        {
            _context.Doctors.Update(doctor.ToDoctorModel());
            return true;
        }

        public void Save()
        {
            _context.SaveChanges();
            return;
        }

        public IEnumerable<Doctor?> GetAll()
        {
            var _doctors = _context.Doctors.Include(u => u.Specialization).ToList();
            var doctors = _doctors.Select(x => x.ToDomain()).ToList();
            return doctors;
        }
    }
}
