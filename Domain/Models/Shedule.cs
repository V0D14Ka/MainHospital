using Domain.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Shedule
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime StartWorking;
        public DateTime EndWorking;

        public Shedule() : this(0, new DateTime(), new DateTime()) { }

        public Shedule(int id, DateTime start, DateTime end)
        {
            DoctorId = id;
            StartWorking = start;
            EndWorking = end;
        }


        public Result IsValid()
        {
            if (DoctorId < 0)
                return Result.Fail("Invalid id");

            if (StartWorking >= EndWorking)
                return Result.Fail("Invalid time");

            return Result.Ok();
        }
    }
}
