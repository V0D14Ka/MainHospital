using Domain.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Appointment
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public Appointment() : this(new DateTime(), new DateTime(), 1, 1) { }

        public Appointment(DateTime start, DateTime end, int patientId, int doctorId)
        {
            StartTime = start;
            EndTime = end;
            PatientId = patientId;
            DoctorId = doctorId;
        }


        public Result IsValid()
        {
            if (DoctorId < 0)
                return Result.Fail("Invalid doctor id");

            if (PatientId < 0)
                return Result.Fail("Invalid patient id");

            if(StartTime >= EndTime)
                return Result.Fail("Invalid time");

            return Result.Ok();
        }
    }
}
