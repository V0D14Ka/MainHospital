using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Converters.ToDomain
{
    public static class SheduleModelToDomainConverter
    {
        public static Shedule? ToDomain(this SheduleModel model)
        {
            return new Shedule
            {
                DoctorId = model.DoctorId,
                StartWorking = model.StartWorking,
                EndWorking = model.EndWorking
            };
        }
    }
}
