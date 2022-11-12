using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class AppointmentModel
    {
        public DateTime StartTime;
        public DateTime EndTime;
        public int PatientId;
        public int DoctorId;
    }
}
