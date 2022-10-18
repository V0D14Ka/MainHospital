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
        public int Id { get; set; }
        public string Name { get; set; }
        public Specialization Specialization { get; set; }

        public Doctor() : this(0, "", new Specialization()) { }

        public Doctor(int id, string name, Specialization specialization)
        {
            Id = id;
            Name = name;
            Specialization = specialization;
        }


        public Result IsValid()
        {
            if (Id < 0)
                return Result.Fail("Invalid id");

            if (string.IsNullOrEmpty(Name))
                return Result.Fail("Invalid doctor name");

            return Result.Ok();
        }
    }
}
