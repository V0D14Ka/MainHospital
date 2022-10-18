using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        bool MakeNote(Appointment appoint);
        IEnumerable<Appointment> GetActualDates(string specialization);
        IEnumerable<Appointment> GetActualDatesByDoctorID(int doctorId);
        bool IsAppointmentExist(Appointment appointment);
    }
}
