using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        bool IsDoctorExists(string doctorName);
        bool CreateDoctor(Doctor doctor);
        bool DeleteDoctor(Doctor doctor);
        Doctor GetDoctorById(int id);
        IEnumerable<Doctor> GetAllDoctors();
    }
}
