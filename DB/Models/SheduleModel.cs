using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class SheduleModel
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime StartWorking { get; set; }
        public DateTime EndWorking { get; set; }
    }
}
