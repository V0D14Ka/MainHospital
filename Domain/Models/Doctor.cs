using Domain.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSpecialization { get; set; }

        public Doctor() : this(0, "a", "a") { }

        public Doctor(int id, string name, string specialization)
        {
            DoctorId = id;
            DoctorName = name;
            DoctorSpecialization = specialization;
        }


        public Result IsValid()
        {
            if (DoctorId < 0)
                return Result.Fail("Invalid id");

            if (string.IsNullOrEmpty(DoctorName))
                return Result.Fail("Invalid doctor name");

            if (string.IsNullOrEmpty(DoctorSpecialization))
                return Result.Fail("Invalid specialization");

            return Result.Ok();
        }
    }
}
