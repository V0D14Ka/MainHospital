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
        bool CreateShedule(Shedule shedule);
        bool UpdateShedule(Shedule shedule, Shedule updatedshedule);
        Shedule? GetSheduleByDoctorAndDate(Doctor doctor,DateTime date);
    }
}
