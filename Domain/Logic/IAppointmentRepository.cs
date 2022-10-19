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
        IEnumerable<Appointment> GetActualDates(Specialization spec);
        IEnumerable<Appointment> GetActualDates(Doctor doctor);
        bool IsAppointmentExist(Appointment appointment);
    }
}
