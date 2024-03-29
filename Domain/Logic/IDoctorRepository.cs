﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        bool IsDoctorExist(int id);
        bool IsDoctorExist(Doctor doctor);
        Doctor? GetDoctorById(int id);
        IEnumerable<Doctor?> GetDoctorsBySpec(int specid);
    }
}
