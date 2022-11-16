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
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationContext _context;

        public AppointmentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Appointment?> GetActualDates(Specialization spec)
        {
            var alldates = new List<AppointmentModel>();
            var doctors = _context.Doctors.Where(u => u.Specialization.Id == spec.Id).ToList();
            foreach (var doctor in doctors)
            {
                var appointment = _context.Appointments.Where(u => u.DoctorId == doctor.Id && u.PatientId == 0).ToList();
                appointment.ForEach(p => alldates.Add(p));
            }
            IEnumerable<Appointment?> date = alldates.Select(x => x.ToDomain()).ToList();
            return date;
        }

        public IEnumerable<Appointment?> GetActualDates(Doctor doctor)
        {
            var appoints = _context.Appointments.Where(u => u.DoctorId == doctor.Id && u.PatientId == 0).ToList();
            var date = appoints.Select(x => x.ToDomain()).ToList();
            return date;
        }

        public bool IsAppointmentExist(Appointment appoint)
        {
            var flag = _context.Appointments.FirstOrDefault(u => u.DoctorId == appoint.DoctorId
            && u.PatientId == appoint.PatientId);
            return flag != null;
        }

        public bool Create( Appointment appointment)
        {
            AppointmentModel? newappointment = appointment.ToAppointmentModel();
            try { _context.Appointments.Add(newappointment); }
            catch { return false; }
            return true;
        }

        public bool Delete(int id)
        {
            var appoint = _context.Appointments.SingleOrDefault(u => u.PatientId == id);
            if (appoint != null)
            {
                var flag = _context.Appointments.Remove(appoint);
                return true;
            }
            return false;
        }

        public bool Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment.ToAppointmentModel());
            return true;
        }

        public void Save()
        {
            _context.SaveChanges();
            return;
        }

        public IEnumerable<Appointment?> GetAll()
        {
            var _appointments = _context.Appointments.ToList();
            var appointments = _appointments.Select(x => x.ToDomain()).ToList();
            return appointments;
        }
    }
}
