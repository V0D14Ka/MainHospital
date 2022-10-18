﻿using Domain.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Specialization
    {
        public int SpecID { get; set; }
        public string Name { get; set; }

        public Specialization() : this(0, "") { }

        public Specialization(int id, string name) 
        { 
            SpecID = id; 
            Name = name; 
        }


        public Result IsValid()
        {
            if (SpecID < 0)
                return Result.Fail("Invalid id");

            if (string.IsNullOrEmpty(Name))
                return Result.Fail("Invalid specialization name");

            return Result.Ok();
        }
    }
}
