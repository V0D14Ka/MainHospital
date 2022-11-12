using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Converters.FromDomain
{
    public static class DomainToAppointmentModelConverter
    {
        public static AppointmentModel? ToAppointmentModel(this Appointment model)
        {
            return new AppointmentModel
            {
                DoctorId = model.DoctorId,
                PatientId = model.PatientId,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
            };
        }
    }
}
