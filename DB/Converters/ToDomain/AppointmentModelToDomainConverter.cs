using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Converters.ToDomain
{
    public static class AppointmentModelToDomainConverter
    {
        public static Appointment? ToDomain(this AppointmentModel model)
        {
            return new Appointment
            {
                DoctorId = model.DoctorId,
                PatientId = model.PatientId,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
            };
        }
    }
}
