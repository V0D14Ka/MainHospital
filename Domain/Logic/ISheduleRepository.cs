using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic
{
    public interface ISheduleRepository : IRepository<Shedule>
    {
        Shedule? GetSheduleByDoctorAndDate(Doctor doctor,DateTime date);
    }
}
